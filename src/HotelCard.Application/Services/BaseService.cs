using AutoMapper;
using HotelCard.Application.Notification;

namespace HotelCard.Application.Services;

public abstract class BaseService
{
    protected readonly INotificator Notificator;
    protected readonly IMapper Mapper;

    protected BaseService(INotificator notificator, IMapper mapper)
    {
        Notificator = notificator;
        Mapper = mapper;
    }
}