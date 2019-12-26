using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Farm.Suppliers.WebAPI.Models;
using Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Farm.Suppliers.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SupplierController : ControllerBase
    {

        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierRepository repository;
        private readonly IMapper mapper;

        public SupplierController(ILogger<SupplierController> logger, ISupplierRepository repository, IMapper mapper)
        {
            _logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierModel>> Get()
        {
            var suppliers = await repository.GetAllAsync();
            return suppliers.Select(s => mapper.Map<SupplierModel>(s));
        }
        
        [HttpDelete]
        public async void Delete(int id)
        {
            repository.Delete(id);
        }

        [HttpPost]
        public async Task<SupplierModel> AddOrUpdate(SupplierModel supplierModel)
        {
            var supplierInDb = await repository.GetAsync(supplierModel.Id);
            Supplier @new;
            if (supplierInDb == null)
            {
                var supplier = mapper.Map<Supplier>(supplierModel);
                @new = repository.Add(supplier);
            }
            else
            {
                @new= mapper.Map<SupplierModel, Supplier>(supplierModel, supplierInDb);
                repository.Update(supplierInDb);
            }
            repository.UnitOfWork.SaveChangesAsync();
            return mapper.Map<SupplierModel>(@new);
        }
    }
}
