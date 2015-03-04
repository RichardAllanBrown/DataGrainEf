using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf.Example.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Total { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
