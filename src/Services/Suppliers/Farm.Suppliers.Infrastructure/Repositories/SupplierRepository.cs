using Farm.Infrastructure.Seedwork;
using Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Suppliers.Infrastructure.Repositories
{
    public class SupplierRepository
        : ISupplierRepository
    {
        private readonly SupplierContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public SupplierRepository(SupplierContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Supplier Add(Supplier entity)
        {
            var existingEntity = _context.Suppliers.Find(entity.Id);
            if (existingEntity != null)
                return existingEntity;
            _context.Suppliers.Add(entity);
            return entity;
        }

        public async Task<Supplier> GetAsync(int id)
        {
            var entity = await _context
                                .Suppliers
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (entity == null)
            {
                entity = _context
                            .Suppliers
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return entity;
        }

        public void Update(Supplier entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public void Delete(int id)
        {
            var entity = _context.Suppliers.Find(id);
            if (entity != null)
            _context.Remove(entity);
        }
    }
}
