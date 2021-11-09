using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgren.Models
{
    public class Sınav
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SınavID { get; set; }
        public string TextID { get; set; }

        [Required(ErrorMessage = "Başlık alanı boş olamaz!")]
        public string Baslık { get; set; }

        [Required(ErrorMessage = "Yazı alanı boş olamaz!")]
        public string İcerik { get; set; }
        public DateTime OlusturmaTarihi { get; set; }

        public List<Sorular> Sorulars { get; set; }
    }
}
