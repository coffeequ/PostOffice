//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PostOffice
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subscribe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subscribe()
        {
            this.Correspondence = new HashSet<Correspondence>();
        }
    
        public int id_Subscribe { get; set; }
        public int id_Subscriber { get; set; }
        public int id_Publication { get; set; }
        public byte StatusActive { get; set; }
        public System.DateTime EntryTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public System.DateTime DateRegistration { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Correspondence> Correspondence { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual SubscriberOfThePostOffice SubscriberOfThePostOffice { get; set; }
    }
}
