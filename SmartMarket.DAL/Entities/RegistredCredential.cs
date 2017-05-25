using SmartMarket.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.DAL.Entities
{
    public class RegistredCredential : IEntity
    {
        [Key, ForeignKey("User")]
        public int ID { get; set; }

        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        public bool IsEmailConfirmed { get; set; }

        public User User { get; set; }
    }
}