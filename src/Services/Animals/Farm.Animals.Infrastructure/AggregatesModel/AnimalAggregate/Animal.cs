using Farm.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate
{
    public class Animal
        : Entity, IAggregateRoot
    {
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