using FluentValidation.Results;

namespace HotelCard.Application.Notification;

public class Notificator : INotificator
{
    private readonly List<string> _notifications = new();
    private bool _isNotFoundResourse;
    public void Handle(string message)
    {
        if (_isNotFoundResourse)
            throw new InvalidOperationException(
                "Não é possível charmar um tipo Handle quando for do tipo NotFoundResource");
        
        _notifications.Add(message);
    }

    public void Handle(List<ValidationFailure> failures)
    {
        failures.ForEach(c => Handle(c.ErrorMessage));
    }

    public void HandleNotFoundResource()
    {
        if(HasNotification)
            throw new InvalidOperationException(
                "Não é possível charmar um tipo NotFoundResource quando for do tipo Handle");
        
        _isNotFoundResourse = true;
    }

    public IEnumerable<string> GetNotifications() => _notifications;

    public bool HasNotification => _notifications.Any();
    public bool IsNotFoundResource => _isNotFoundResourse;
}