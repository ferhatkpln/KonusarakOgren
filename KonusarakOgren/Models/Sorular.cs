using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgren.Models
{
    public class Sorular
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SınavCevapID { get; set; }
        public int SoruNo { get; set; }

        [Required(ErrorMessage = "Soru yazmalısınız...")]
        public string Soru { get; set; }

        [Required(ErrorMessage = "Cevap giriniz...")]
        public string CevapA { get; set; }

        [Required(ErrorMessage = "Cevap giriniz...")]
        public string CevapB { get; set; }

        [Required(ErrorMessage = "Cevap giriniz...")]
        public string CevapC { get; set; }

        [Required(ErrorMessage = "Cevap giriniz...")]
        public string CevapD { get; set; }

        [Required(ErrorMessage = "Doğru cevabı seçiniz...")]
        public string DoğruCevap { get; set; }
        public int SınavID { get; set; }
        public Sınav Sınav { get; set; }
    }
}
