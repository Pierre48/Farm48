using Farm.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Animal Add(Animal animal);

        void Update(Animal animal);

        Task<Animal> GetAsync(int id);
        Task<IEnumerable<Animal>> GetAllAsync();
        void Delete(int id);
    }
}
