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
    
    public partial class ChildSubCategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChildSubCategori()
        {
            this.melks = new HashSet<melk>();
        }
    
        public int id { get; set; }
        public string childSubCategoriName { get; set; }
        public int subCategoriId { get; set; }
    
        public virtual SubCategori SubCategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<melk> melks { get; set; }
    }
}
