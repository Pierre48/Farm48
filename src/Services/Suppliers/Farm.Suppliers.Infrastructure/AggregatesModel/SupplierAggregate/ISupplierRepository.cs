using Farm.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Supplier Add(Supplier supplier);

        void Update(Supplier supplier);

        Task<Supplier> GetAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        void Delete(int id);
    }
}
