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
    //TODO make unique email.
    public class User : IEntity
    {
        [Key]
        public int ID { get; set; }  
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsEmailConfirmed { get; set; }


        public virtual ICollection<Role> Roles { get; set; }
    }
}
