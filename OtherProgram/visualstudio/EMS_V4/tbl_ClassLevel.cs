//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS_V4
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ClassLevel
    {
        public tbl_ClassLevel()
        {
            this.tbl_YearAcademy1 = new HashSet<tbl_YearAcademy>();
        }
    
        public int Id_ClassLevel { get; set; }
        public Nullable<int> Id_YearAcademy { get; set; }
        public string ClassLevel { get; set; }
    
        public virtual tbl_YearAcademy tbl_YearAcademy { get; set; }
        public virtual ICollection<tbl_YearAcademy> tbl_YearAcademy1 { get; set; }
    }
}