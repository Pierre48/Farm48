using Farm.Animals.Infrastructure.AggregatesModel.AnimalAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farm.Animals.Infrastructure.EntityConfigurations
{
    internal class AnimalEntityTypeConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> AnimalConfiguration)
        {
            AnimalConfiguration.ToTable("Animal", AnimalContext.DEFAULT_SCHEMA);

            AnimalConfiguration.HasKey(o => o.Id);

            AnimalConfiguration.Ignore(b => b.DomainEvents);

            AnimalConfiguration.Property(o => o.Id)
                .UseHiLo("Animalseq", AnimalContext.DEFAULT_SCHEMA);

            AnimalConfiguration.Property(b => b.ShortName)
                .HasMaxLength(20)
                .IsRequired();

            AnimalConfiguration.Property(b => b.LongName)
                .HasMaxLength(200)
                .IsRequired();

            AnimalConfiguration.Property(b => b.Gender)
                .IsRequired();
            
            AnimalConfiguration.Property(b => b.AnimalType)
                .IsRequired();

            AnimalConfiguration.Property(b => b.BirthDay);
            AnimalConfiguration.Property(b => b.DeathDate);
            AnimalConfiguration.Property(b => b.EntryDate);
            AnimalConfiguration.Property(b => b.LeaveDate);
        }
    }

}