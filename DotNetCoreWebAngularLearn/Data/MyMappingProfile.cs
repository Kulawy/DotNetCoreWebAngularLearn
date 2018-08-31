using AutoMapper;
using DotNetCoreWebAngularLearn.Data.Entities;
using DotNetCoreWebAngularLearn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebAngularLearn.Data
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember( o => o.OrderId, ex => ex.MapFrom( o => o.Id))
                .ReverseMap(); // żeby działało w dwie strony 


            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();

        }
        
    }
}
