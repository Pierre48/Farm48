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

        public Supplier Add(Supplier supplier)
        {
            var existingSupplier = _context.Suppliers.Find(supplier.Id);
            if (existingSupplier != null)
                return existingSupplier;
            _context.Suppliers.Add(supplier);
            return supplier;
        }

        public async Task<Supplier> GetAsync(int SupplierId)
        {
            var Supplier = await _context
                                .Suppliers
                                .FirstOrDefaultAsync(o => o.Id == SupplierId);
            if (Supplier == null)
            {
                Supplier = _context
                            .Suppliers
                            .Local
                            .FirstOrDefault(o => o.Id == SupplierId);
            }

            return Supplier;
        }

        public void Update(Supplier Supplier)
        {
            _context.Entry(Supplier).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public void Delete(int id)
        {
            var entity = _context.FindAsync<Supplier>(id);
            _context.Remove(entity);
        }
    }
}
