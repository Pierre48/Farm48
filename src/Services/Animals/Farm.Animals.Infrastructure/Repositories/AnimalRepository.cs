using Farm.Infrastructure.Seedwork;
using Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Animals.Infrastructure.Repositories
{
    public class AnimalRepository
        : IAnimalRepository
    {
        private readonly AnimalContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AnimalRepository(AnimalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Animal Add(Animal entity)
        {
            var existingEntity = _context.Animals.Find(entity.Id);
            if (existingEntity != null)
                return existingEntity;
            _context.Animals.Add(entity);
            return entity;
        }

        public async Task<Animal> GetAsync(int id)
        {
            var entity = await _context
                                .Animals
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (entity == null)
            {
                entity = _context
                            .Animals
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return entity;
        }

        public void Update(Animal entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals.ToListAsync();
        }

        public void Delete(int id)
        {
            var entity = _context.Animals.Find(id);
            if (entity != null)
            _context.Remove(entity);
        }
    }
}
