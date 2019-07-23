using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealsBackend.Models
{
    public class Receipt : IComparable
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int CompareTo(object obj)
        {
            if(obj is Receipt receipt)
            {
                return ID.CompareTo(receipt.ID);
            }
            return ID.CompareTo(obj);
        }
    }

    public class ReceiptModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Portions { get; set; }
        public List<IngredientAmountModel> Ingedients { get; set; }
    }

    public class IngredientModel
    {
        public string Name { get; set; }
        public string Measure { get; set; }
        public int Сalories { get; set; }
        public int Fats { get; set; }
        public int Сarbohydrates { get; set; }
        public int Proteins { get; set; }
    }

    public class IngredientAmountModel
    {
        public IngredientModel Ingredient { get; set; }
        public double Amount { get; set; }
    }
}