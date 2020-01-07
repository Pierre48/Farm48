using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Farm.Animals.WebAPI.Models;
using Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Farm.Animals.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnimalController : ControllerBase
    {

        private readonly ILogger<AnimalController> _logger;
        private readonly IAnimalRepository repository;
        private readonly IMapper mapper;

        public AnimalController(ILogger<AnimalController> logger, IAnimalRepository repository, IMapper mapper)
        {
            _logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AnimalModel>> Get()
        {
            var Animals = await repository.GetAllAsync();
            return Animals.Select(s => mapper.Map<AnimalModel>(s));
        }
        
        [HttpDelete]
        public async void Delete(int id)
        {
            repository.Delete(id);
            repository.UnitOfWork.SaveChangesAsync();
        }

        [HttpPost]
        public async Task<AnimalModel> AddOrUpdate(AnimalModel AnimalModel)
        {
            var AnimalInDb = await repository.GetAsync(AnimalModel.Id);
            Animal @new;
            if (AnimalInDb == null)
            {
                var Animal = mapper.Map<Animal>(AnimalModel);
                @new = repository.Add(Animal);
            }
            else
            {
                @new= mapper.Map<AnimalModel, Animal>(AnimalModel, AnimalInDb);
                repository.Update(AnimalInDb);
            }
            repository.UnitOfWork.SaveChangesAsync();
            return mapper.Map<AnimalModel>(@new);
        }
    }
}
