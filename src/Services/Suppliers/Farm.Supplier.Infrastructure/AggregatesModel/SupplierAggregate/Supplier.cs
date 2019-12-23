using Farm.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate
{
    public class Supplier
        : Entity, IAggregateRoot
    {
        public String ShortName { get; set; }
        public String LongName { get; set; }
        public String AddressLine1 { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
    }
}
