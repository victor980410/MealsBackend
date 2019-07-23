using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealsBackend.DataBase;
using Newtonsoft;
using Newtonsoft.Json;

namespace MealsBackend.Controllers
{
    public class MealsAPIController : Controller
    {
        dscorers_victorEntities database = new dscorers_victorEntities();

        public JsonResult GetAllReceipts()
        {
            var routes = database.Receipts.Select(r =>
                new 
                {
                    r.ID,
                    r.Title,
                    r.Description
                }).ToList();

            return Json(routes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReceiptDetails(int? id)
        {
            if (database.Receipts.Find(id) is Receipt receipt) {
                var ingredients = receipt.Recepient_Ingredient.Select(ri => new Models.IngredientAmountModel
                {
                    Amount = ri.Volume ?? 0,
                    Ingredient = new Models.IngredientModel {
                        Name = ri.Ingredient.Name,
                        Measure = ri.Ingredient.MeasureUnit.Name,
                        Сalories = ri.Ingredient.Сalories,
                        Fats = ri.Ingredient.Fats,
                        Сarbohydrates = ri.Ingredient.Сarbohydrates,
                        Proteins = ri.Ingredient.Proteins
                    }
                }).ToList();
                var receiptModel = new Models.ReceiptModel
                {
                    ID = id.Value,
                    Title = receipt.Title,
                    Description = receipt.Description,
                    Ingedients = ingredients,
                    Duration = receipt.Duration ?? 0,
                    Portions = receipt.Portions ?? 0
                };
                return Json(receiptModel, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllIngredients()
        {
            var ingredients = database.Ingredients.Select(r =>
               new
               {
                   r.ID,
                   r.Name
               }).ToList();
            return Json(ingredients, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchReceipts(int[] ids)
        {
            if (ids == null)
                return null;

            SortedSet<int> set = null;
            foreach(int id in ids)
            {
                if (database.Ingredients.Find(id) is Ingredient ingredient)
                {
                    var tempSet = new SortedSet<int>(ingredient.Recepient_Ingredient.Select(r => r.ReceiptID));
                    if(set == null)
                    {
                        set = tempSet;
                    } else
                    {
                        set = new SortedSet<int>(set.Intersect(tempSet));
                    }
                }
            }

            if (set == null)
                return null;

            var result = new List<Models.Receipt>();
            foreach(int id in set)
            {
                if (database.Receipts.Find(id) is Receipt receipt)
                {
                    result.Add(new Models.Receipt
                    {
                        ID = receipt.ID,
                        Title = receipt.Title,
                        Description = receipt.Description
                    });
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}