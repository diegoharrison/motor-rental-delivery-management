using Microsoft.EntityFrameworkCore;
using MotorRent.DeliveryManagement.Core.Entities;
using MotorRent.DeliveryManagement.Core.Repositories;
using MotorRent.DeliveryManagement.Infrastructure.Data;

namespace MotorRent.DeliveryManagement.Infrastructure.Repositories;

public class DeliverymanRepository : IDeliverymanRepository
{
    private readonly AppDbContext _context;

    public DeliverymanRepository(AppDbContext context)
    {
        _context = context;
    }

    public Deliveryman GetById(int id)
    {
        return _context.Deliverymen.FirstOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<Deliveryman>> GetAllAsync()
    {
        return await _context.Deliverymen.ToListAsync();
    }

    public Deliveryman GetByCNPJ(string cnpj)
    {
        return _context.Deliverymen.FirstOrDefault(x => x.CNPJ == cnpj);
    }

    public Deliveryman GetByDriverLicenseNumber(string driverLicenseNumber)
    {
        return _context.Deliverymen.FirstOrDefault(x => x.DriverLicenseNumber == driverLicenseNumber);
    }

    public void Add(Deliveryman deliveryman)
    {
        _context.Deliverymen.Add(deliveryman);
        _context.SaveChanges();
    }

    public void Update(Deliveryman deliveryman)
    {
        _context.Deliverymen.Update(deliveryman);
        _context.SaveChanges();
    }

    public void Remove(Deliveryman deliveryman)
    {
        _context.Deliverymen.Remove(deliveryman);
        _context.SaveChanges();
    }
}
