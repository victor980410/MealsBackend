//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MealsBackend.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recepient_Ingredient
    {
        public int ReceiptID { get; set; }
        public int IngredientID { get; set; }
        public Nullable<double> Volume { get; set; }
    
        public virtual Ingredient Ingredient { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
