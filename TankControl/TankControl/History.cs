//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TankControl
{
    public partial class History
    {
        public int id { get; set; }
        public int recipe_id { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<double> el1 { get; set; }
        public Nullable<double> el2 { get; set; }
        public Nullable<double> el3 { get; set; }
        public Nullable<double> el4 { get; set; }
        public Nullable<double> el6 { get; set; }
        public Nullable<double> el7 { get; set; }
        public Nullable<int> duration_el1 { get; set; }
        public Nullable<int> duration_el2 { get; set; }
        public Nullable<int> duration_el3 { get; set; }
        public Nullable<int> duration_el4 { get; set; }
        public Nullable<int> duration_el5 { get; set; }
        public Nullable<int> duration_el6 { get; set; }
        public Nullable<int> duration_el7 { get; set; }
    
        public virtual Recipe Recipe { get; set; }
    }
    
}
