using AutoMapper;
using DotNetCoreWebAngularLearn.Data;
using DotNetCoreWebAngularLearn.Data.Entities;
using DotNetCoreWebAngularLearn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebAngularLearn.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemController : Controller
    {
        private readonly IMyRepository _repository;
        private readonly ILogger<OrderItemController> _logger;
        private readonly IMapper _mapper;

        public OrderItemController(IMyRepository repository, ILogger<OrderItemController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null)
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{orderItemId}")]
        public IActionResult Get(int orderId, int orderItemId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == orderItemId).FirstOrDefault();
                if (item != null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }
            return NotFound();
        }

    }
}
