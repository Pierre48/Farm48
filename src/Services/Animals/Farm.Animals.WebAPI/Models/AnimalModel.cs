using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate;

namespace Farm.Animals.WebAPI.Models
{
    public class AnimalModel
    {
        public  int Id { get; set; }
        public AnimalType AnimalType { get; set; }
        public String ShortName { get; set; }
        public String LongName { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public Gender Gender { get; set; }
    }
}
