using DotNetCoreWebAngularLearn.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebAngularLearn.Data
{
    public class MyRepository : IMyRepository
    {
        private readonly MyDataContext _ctx;
        private readonly ILogger<MyRepository> _logger;

        public MyRepository(MyDataContext ctx, ILogger<MyRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }


        public IEnumerable<Product> GetAllProducts()
        {            
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }

        } 

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }


    }
}
