using AutoMapper;
using Farm.Supplier.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Supplier.WebAPI
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Supplier, SupplierModel>();
        }
    }
}
