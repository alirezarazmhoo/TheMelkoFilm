//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalMelkBin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class melkAcceptUser
    {
        public int id { get; set; }
        public int melkId { get; set; }
        public string userName { get; set; }
        public string statusSelected { get; set; }
        public string RegisterDate { get; set; }
    
        public virtual melk melk { get; set; }
    }
}
