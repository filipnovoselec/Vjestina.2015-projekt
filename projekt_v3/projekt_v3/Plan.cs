//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace projekt_v3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plan
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public double WantedAvg { get; set; }
        public Nullable<double> CurrentAvg { get; set; }
        public int nGrades { get; set; }
        public string NeededGrades { get; set; }
        public int Pcolumn { get; set; }
        public string ColName { get; set; }
        public int SubjectId { get; set; }
        public int Mode { get; set; }
    
        public virtual Column Column { get; set; }
    }
}
