using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate;
using Farm.Animals.WebAPI.Models;

namespace Farm.Animals.WebAPI
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Animal, AnimalModel>();
            CreateMap<AnimalModel, Animal>().ForMember(x => x.Id, opt => opt.Ignore());

        }
    }
}
