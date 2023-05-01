using System.Security.Claims;
using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.Cars.Views;

internal class CarsView : BaseView
{
    public List<Car> GetAllCars(HttpContext httpContext, HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.Cars.Skip(page * pageSize).Take(pageSize).ToList();
    }

    public Car GetCar(int id, HajurKoDbContext context)
    {
        var car = context.Cars.FirstOrDefault(c => c.Id == id);
        if (car is null) throw new KeyNotFoundException($"Car with ID {id} not found");
        return car;
    }

    public void CreateCar(Car car, HajurKoDbContext context)
    {
        // if (car.Photo is { Length: > 0 })
        // {
        //     var fileName = Guid.NewGuid() + Path.GetExtension(car.Photo.FileName);
        //     var filePath = Path.Combine(Config.FileUploadPath, fileName);
        //     using var stream = new FileStream(filePath, FileMode.Create);
        //     car.Photo.CopyTo(stream);
        //     car.PhotoUrl = Config.FileBaseUrl + fileName;
        // }

        context.Cars.Add(car);
        context.SaveChanges();
    }

    public void UpdateCar(Car car, HajurKoDbContext context)
    {
        context.Entry(car).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void DeleteCar(int id, HajurKoDbContext context)
    {
        var car = context.Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) throw new KeyNotFoundException($"Car with ID {id} not found");

        if (car.PhotoUrl is null)
        {
            var fileName = Path.GetFileName(car.PhotoUrl);
            var filePath = Path.Combine(Config.FileUploadPath, fileName!);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        context.Cars.Remove(car);
        context.SaveChanges();
    }
}