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
    
    public partial class SubCategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubCategori()
        {
            this.ChildSubCategoris = new HashSet<ChildSubCategori>();
            this.melks = new HashSet<melk>();
        }
    
        public int id { get; set; }
        public string subCategoriName { get; set; }
        public int categoriId { get; set; }
    
        public virtual Categori Categori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChildSubCategori> ChildSubCategoris { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<melk> melks { get; set; }
    }
}
