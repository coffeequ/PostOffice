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
    
    public partial class Publication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publication()
        {
            this.Subscribe = new HashSet<Subscribe>();
        }
    
        public int id_Publication { get; set; }
        public int id_TypePublication { get; set; }
        public int id_TypeViewPublication { get; set; }
        public string Name { get; set; }
        public decimal PricePerMonth { get; set; }
        public string Cover { get; set; }
        public string Feedback { get; set; }
        public int NumberIssuesPerMonth { get; set; }
    
        public virtual TypePublication TypePublication { get; set; }
        public virtual TypeViewPublication TypeViewPublication { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscribe> Subscribe { get; set; }

        public string NewNumberIssuesPerMonth
        {
            get
            {
                return NumberIssuesPerMonth > 1 ? $"{TypePublication.Name}, {NumberIssuesPerMonth.ToString()} раза в месяц" : $" {TypePublication.Name}, {NumberIssuesPerMonth.ToString()} раз в месяц";
            }
        }

        public string PricePerMonthRounded
        {
            get
            {
                return PricePerMonth.ToString("F2");
            }
        }

        public string CoverImage
        {
            get
            {
                return $"/Pic/" + Cover;
            }
        }
    }
}
