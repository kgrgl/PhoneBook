using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Persons
    {
        [Key]
        public int ID { get; set; }
        public Guid UUID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Kişi Adı 50 karakterden fazla olamaz!")]
        public int Ad { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Kişi Soyadı 50 karakterden fazla olamaz!")]
        public int Soyad { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Kişi Firma Bilgisi 50 karakterden fazla olamaz!")]
        public int Firma { get; set; }
        public ICollection<Contacts> contacts { get; set; }
    }
}
