using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Contacts
    {

        [Key]
        public int ID { get; set; }
        public enum ContactTypes
        {
            Phone,
            Mail,
            Location
        }
        [Required]
        [MaxLength(50, ErrorMessage = "İletişim Verisi 50 karakterden fazla olamaz!")]
        public string ContactText { get; set; }
        public Persons Match { get; set; }
        [Required]
        public ContactTypes ContactType { get; set; }
    }
}
