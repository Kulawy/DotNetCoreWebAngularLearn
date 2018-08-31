using System.Collections.Generic;
using DotNetCoreWebAngularLearn.Data.Entities;

namespace DotNetCoreWebAngularLearn.Data
{
    public interface IMyRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}

// stworzyliśmy interface z MyRepository żeby można było tworzyć moki przy testowaniu a nie na rzeczywistych rzeczach testować.