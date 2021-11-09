using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgren.ViewModels
{
    public class CevapViewModel
    {
        public int Id { get; set; }
        public string Cevap { get; set; }
        public string DoğruCevap { get; set; }
        public bool? DoğruCevapmı { get; set; }
    }
}
