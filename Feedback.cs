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
    
    public partial class Feedback
    {
        public int id_Feedback { get; set; }
        public int id_Publication { get; set; }
        public string Feedback1 { get; set; }
    
        public virtual Publication Publication { get; set; }
    }
}