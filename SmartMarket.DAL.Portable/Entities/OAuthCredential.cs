using SmartMarket.DAL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.DAL.Entities
{
    public class OAuthCredential :  IEntity
    {
        [Key, ForeignKey("User")]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public int Identifier { get; set; }

        public User User { get; set; }
    }
}