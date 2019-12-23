using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Supplier.WebAPI.Models
{
    public class SupplierModel
    {
        public  int Id { get; set; }
        public String ShortName { get; set; }
        public String LongName { get; set; }
        public String AddressLine1 { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
    }
}
