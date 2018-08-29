using System.Collections.Generic;
using DotNetCoreWebAngularLearn.Data.Entities;

namespace DotNetCoreWebAngularLearn.Data
{
    public interface IMyRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();

    }
}

// stworzyliśmy interface z MyRepository żeby można było tworzyć moki przy testowaniu a nie na rzeczywistych rzeczach testować.