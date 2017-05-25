using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL.Interfaces
{
    public interface IEntity
    {
        [Key]
        int ID { get; set; }
    }
}
