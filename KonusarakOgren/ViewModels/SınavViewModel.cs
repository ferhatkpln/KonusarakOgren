using KonusarakOgren.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgren.ViewModels
{
    public class SınavViewModel
    {
        public List<TextViewModel> TextViewModels { get; set; }
        public Sınav Sınav { get; set; }
    }
}
