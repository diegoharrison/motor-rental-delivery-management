using Microsoft.EntityFrameworkCore;
using MotorRent.DeliveryManagement.Core.Entities;
using MotorRent.DeliveryManagement.Core.Repositories;
using MotorRent.DeliveryManagement.Infrastructure.Data;

namespace MotorRent.DeliveryManagement.Infrastructure.Repositories;

public class MotoRepository : IMotoRepository
{
    private readonly AppDbContext _context;

    public MotoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Moto GetById(int id)
    {
        return _context.Motos.FirstOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<Moto>> GetAllAsync()
    {
        return await _context.Motos.ToListAsync();
    }

    public Moto GetByPlate(string plate)
    {
        return _context.Motos.FirstOrDefault(x => x.Plate == plate);
    }

    public void Add(Moto moto)
    {
        _context.Motos.Add(moto);
        _context.SaveChanges();
    }

    public void Update(Moto moto)
    {
        _context.Motos.Update(moto);
        _context.SaveChanges();
    }

    public void Remove(Moto moto)
    {
        _context.Motos.Remove(moto);
        _context.SaveChanges();
    }
}
