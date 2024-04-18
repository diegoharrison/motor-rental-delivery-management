namespace MotorRent.DeliveryManagement.Application.Services.Interfaces;

public interface IRentalService
{
    decimal RentMoto(int motoId, int deliverymanId, DateTime startDate, DateTime endDate, DateTime expectedEndDate);
}