using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL.Entities
{
    public class Balance : IEntity
    {
        [Key]
        public int ID { get; set; }

        public int MerchantID { get; set; }

        public string MerchantPassword { get; set; }

        public string CardNumber { get; set; }

        public decimal Cash { get; set; }

        public Currency Currency { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
