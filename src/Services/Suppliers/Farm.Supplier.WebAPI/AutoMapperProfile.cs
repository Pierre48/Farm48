using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate;
using Farm.Suppliers.WebAPI.Models;

namespace Farm.Suppliers.WebAPI
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Supplier, SupplierModel>()
                .ReverseMap();
        }
    }
}
