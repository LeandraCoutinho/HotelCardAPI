using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Auth;
using HotelCard.Application.Notification;
using HotelCard.Core.Enums;
using HotelCard.Core.Settings;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Domain.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;

namespace HotelCard.Application.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordHasher<Employee> _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;
    
    public AuthService(INotificator notificator, IMapper mapper, IEmployeeRepository employeeRepository, IPasswordHasher<Employee> passwordHasher, IJwtService jwtService, IOptions<JwtSettings> jwtSettings, IEmailService emailService) : base(notificator, mapper)
    {
        _employeeRepository = employeeRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _jwtSettings = jwtSettings.Value;
        _emailService = emailService;
    }

    public async Task<TokenDto?> Login(LoginDto loginDto)
    {
        var employee = await _employeeRepository.Get(loginDto.Email);

        if (employee is null)
        {
            Notificator.Handle("Usuario inexistente!");
            return null;
        }
        
        var validation = _passwordHasher.VerifyHashedPassword(employee, employee.Password, loginDto.Password)
                         != PasswordVerificationResult.Failed;
        
        if(validation)
        {
            return new TokenDto()
            {
                Name = employee.Name,
                Email = employee.Email,
                Token = await GenerateToken(employee)
            };
        }

        return null;
    }

    public async Task<bool> ForgotPassword(string? email)
    {
        var employee = await _employeeRepository.Get(email);
        
        if (employee is null)
        {
            Notificator.HandleNotFoundResource();
            return false;
        }
        
        var resetTokenExpirationDate = DateTime.Now.AddMinutes(10);
        employee.TokenResetExpiresAt = resetTokenExpirationDate;
        employee.TokenResetPassword = GenerateTokenForgotPassword(resetTokenExpirationDate);
        _employeeRepository.Update(employee);
        
        if (await _employeeRepository.UnitOfWork.Commit())
        {
            await _emailService.SendPasswordRecovery(employee);
            return true;
        }

        Notificator.Handle("Não foi possível solicitar a recuperação de senha.");
        return false;
    }
    
    public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var employee = await _employeeRepository.GetTokenResetPassword(resetPasswordDto.Token);
        
        if (employee == null)
        {
            Notificator.HandleNotFoundResource();
            return false;
        }
        
        if (employee.TokenResetExpiresAt.HasValue && employee.TokenResetExpiresAt.Value < DateTime.Now)
        {
            Notificator.Handle("O token de redefinição de senha expirou.");
            return false;
        }
        
        if (!string.IsNullOrEmpty(resetPasswordDto.Password) && resetPasswordDto.Password != resetPasswordDto.ConfirmPassword)
        {
            Notificator.Handle("As senhas informadas não coincidem.");
            return false;
        }

        if (!ValidarSenha(resetPasswordDto.Password))
        {
            return false;
        }
        
        employee.Password = _passwordHasher.HashPassword(employee, resetPasswordDto.Password);
        employee.TokenResetPassword = null;
        employee.TokenResetExpiresAt = null;
        await _employeeRepository.Update(employee);
        
        if (await CommitChanges())
        {
            return true;
        }
        
        Notificator.Handle("Não foi possível alterar a senha.");
        return false;
    }
    
    private async Task<string> GenerateToken(Employee employee)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key); 

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Role, ERole.Administrator.ToString()),
                new Claim(ClaimTypes.Role, ERole.Operator.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours((int)_jwtSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256) 
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    private string GenerateTokenForgotPassword(DateTime expirationDate)
    { 
        var tokenBytes = RandomNumberGenerator.GetBytes(8);
        var tokenRandom = Convert.ToHexString(tokenBytes);

        var stringExpirationDate = expirationDate.ToString("yyyyMMddHHmmss");

        var tokenWithDate = $"{tokenRandom}-{stringExpirationDate}";

        return tokenWithDate;
    }
    
    private bool ValidarSenha(string password)
    {
        var passwordValidator = new EmployeeValidation.PasswordValidator();
        var result = passwordValidator.Validate(password);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                Notificator.Handle(error.ErrorMessage);
            }
            return false;
        }
        
        return true;
    }
    private async Task<bool> CommitChanges() => await _employeeRepository.UnitOfWork.Commit();
}