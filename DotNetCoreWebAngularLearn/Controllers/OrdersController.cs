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
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IMyRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IMyRepository repository, ILogger<OrdersController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true )
        {


            try
            {
                var results = _repository.GetAllOrders(includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {

            try
            {
                var order = _repository.GetOrderById(id);

                if (order != null)
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)  // jak już mamy OrderViewModel to możemy użyć zamiast Order w parametrze konstruktora bo w OrderViewModel dodatkowa validacja
        {

            try
            {
                if(ModelState.IsValid)
                {
                    //var newOrder = new Order()
                    //{
                    //    OrderDate = model.OrderDate,
                    //    OrderNumber = model.OrderNumber,
                    //    Id = model.OrderId
                    //};

                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);


                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        //var vm = new OrderViewModel()
                        //{
                        //    OrderId = newOrder.Id,
                        //    OrderDate = newOrder.OrderDate,
                        //    OrderNumber = newOrder.OrderNumber
                        //};

                        //var vm = _mapper.Map<Order, OrderViewModel>(newOrder);

                        //return Created($"/api/orders/{vm.OrderId}", model);
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                        

                    }
                    
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                //return BadRequest("Failed to get orders");
            }

            return BadRequest("Failed to add new order");

        }

        private IActionResult Created(Order model)
        {
            throw new NotImplementedException();
        }
    }
}
