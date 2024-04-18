using MotorRent.DeliveryManagement.Application.Services.Interfaces;
using MotorRent.DeliveryManagement.Core.Repositories;

namespace MotorRent.DeliveryManagement.Application.Services;

public class RentalService : IRentalService
{
    private readonly IMotoRepository _motoRepository;
    private readonly IDeliverymanRepository _deliverymanRepository;

    public RentalService(IMotoRepository motoRepository, IDeliverymanRepository deliverymanRepository)
    {
        _motoRepository = motoRepository;
        _deliverymanRepository = deliverymanRepository;
    }

    public decimal RentMoto(int motoId, int deliverymanId, DateTime startDate, DateTime endDate, DateTime expectedEndDate)
    {
        var moto = _motoRepository.GetById(motoId);
        
        if (moto == null)
        {
            throw new Exception("The specified motorcycle was not found.");
        }

        var deliveryman = _deliverymanRepository.GetById(deliverymanId);
        if (deliveryman == null)
        {
            throw new Exception("The specified deliveryman was not found.");
        }

        if (deliveryman.DriverLicenseType != "A" && deliveryman.DriverLicenseType != "A+B")
        {
            throw new Exception("The deliveryman is not authorized to rent a motorcycle.");
        }

        // Calcula o número de dias de locação
        var rentalDays = (int)(endDate - startDate).TotalDays;

        // Verifica o plano de locação e seu custo por dia
        decimal dailyCost;
        switch (rentalDays)
        {
            case 7:
                dailyCost = 30.00m;
                break;
            case 15:
                dailyCost = 28.00m;
                break;
            case 30:
                dailyCost = 22.00m;
                break;
            case 45:
                dailyCost = 20.00m;
                break;
            case 50:
                dailyCost = 18.00m;
                break;
            default:
                throw new Exception("Invalid rental duration.");
        }

        // Calcula o custo total da locação
        decimal totalCost = rentalDays * dailyCost;

        // Verifica se a data de retorno é anterior à data prevista
        if (endDate < expectedEndDate)
        {
            // Calcula a multa adicional
            decimal additionalCost = 0;
            if (rentalDays == 7)
            {
                additionalCost = totalCost * 0.20m;
            }
            else if (rentalDays == 15)
            {
                additionalCost = totalCost * 0.40m;
            }

            // Adiciona a multa ao custo total
            totalCost += additionalCost;
        }
        else if (endDate > expectedEndDate)
        {
            // Calcula o número de dias adicionais
            var extraDays = (int)(endDate - expectedEndDate).TotalDays;

            // Adiciona o custo adicional por dia extra
            totalCost += extraDays * 50.00m;
        }
        
        return totalCost;
    }
}