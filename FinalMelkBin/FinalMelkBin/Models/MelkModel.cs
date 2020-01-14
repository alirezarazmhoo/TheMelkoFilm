using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMelkBin
{
    public class MelkModel
    {
        public long lon { get; set; }
        public long lat { get; set; }
        public int page { get; set; }
        public int? subCategoriId { get; set; }
    }
}