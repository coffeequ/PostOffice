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
    
    public partial class SubscriberOfThePostOffice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriberOfThePostOffice()
        {
            this.Subscribe = new HashSet<Subscribe>();
        }
    
        public int id_Subscriber { get; set; }
        public int id_Operator { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public System.DateTime Birthday { get; set; }
        public string NumberPhone { get; set; }
    
        public virtual OperatorPostOffice OperatorPostOffice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscribe> Subscribe { get; set; }
        public virtual User User { get; set; }

        public int CountSubscribe
        {
            get
            {
                return Subscribe.Count;
            }
        }
    }
}
