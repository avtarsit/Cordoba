using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class CategoryService : ICategoryServices
    {
       
        public List<CategoryEntity> GetCategoryList(int CategoryId = 0)
        {
            List<CategoryEntity> Categories = new List<CategoryEntity>();
            Categories.Add(new CategoryEntity() { CategoryId = 1, CategoryName = "Home & Garden", });
            Categories.Add(new CategoryEntity() { CategoryId = 2, CategoryName = "Home Appliances", });
            Categories.Add(new CategoryEntity() { CategoryId = 3, CategoryName = "Health & Safety", });
            Categories.Add(new CategoryEntity() { CategoryId = 4, CategoryName = "Fashion", });
            Categories.Add(new CategoryEntity() { CategoryId = 5, CategoryName = "Flowers", });
            Categories.Add(new CategoryEntity() { CategoryId = 6, CategoryName = "Gaming", });
            Categories.Add(new CategoryEntity() { CategoryId = 7, CategoryName = "Gardening", });
            Categories.Add(new CategoryEntity() { CategoryId = 8, CategoryName = "Gift Cards", });
            Categories.Add(new CategoryEntity() { CategoryId = 9, CategoryName = "Giftware", });
            Categories.Add(new CategoryEntity() { CategoryId = 10, CategoryName = "Home & Garden", });
            Categories.Add(new CategoryEntity() { CategoryId = 11, CategoryName = "Home Appliances", });
            Categories.Add(new CategoryEntity() { CategoryId = 12, CategoryName = "Home and Garden", });
            Categories.Add(new CategoryEntity() { CategoryId = 13, CategoryName = "Nursery", });
            Categories.Add(new CategoryEntity() { CategoryId = 14, CategoryName = "Personal Care", });
            Categories.Add(new CategoryEntity() { CategoryId = 15, CategoryName = "Personal Items", });
            return Categories;
        }
    }
}
