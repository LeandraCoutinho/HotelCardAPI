using HotelCard.Application.Notification;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers;

public class BaseController : ControllerBase
{
    private readonly INotificator _notificator;
    public BaseController(INotificator notificator)
    {
        _notificator = notificator;
    }
    
    protected ActionResult CustomResponse(object? reponse = null)
    {
        if (IsValidOperation)
        {
            return Ok(reponse);
        }

        if (_notificator.IsNotFoundResource)
        {
            return NotFound();
        }

        return BadRequest(_notificator.GetNotifications().ToList());
    }
    
    private bool IsValidOperation => !(_notificator.HasNotification || _notificator.IsNotFoundResource);
}