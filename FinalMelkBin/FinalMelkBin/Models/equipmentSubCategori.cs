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
    
    public partial class equipmentSubCategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public equipmentSubCategori()
        {
            this.childSubEquipmentCategoris = new HashSet<childSubEquipmentCategori>();
            this.equipments = new HashSet<equipment>();
        }
    
        public int id { get; set; }
        public string namee { get; set; }
        public int equipmentCategoriId { get; set; }
    
        public virtual equipmentCategori equipmentCategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<childSubEquipmentCategori> childSubEquipmentCategoris { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipment> equipments { get; set; }
    }
}
