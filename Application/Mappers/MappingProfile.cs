using MotorRent.DeliveryManagement.Application.Dtos;
using MotorRent.DeliveryManagement.Core.Entities;
using AutoMapper;


namespace MotorRent.DeliveryManagement.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DeliverymanDto, Deliveryman>();
        CreateMap<Deliveryman, DeliverymanDto>();
    }
}
