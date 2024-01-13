using Souq.Core.DataBase;
using Souq.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Souq.Repositorey.DataBase.DataSeed
{
    public static class StoreContextSeed
    {


        public static async Task SeedData(StoreContext Context)
        {
            #region Check If Have Data Or Not 
            #region Types 

            if (!Context.ProductTypes.Any())
            {
                await SeedType(Context);
            }
            #endregion


            #region Brands 
            if (!Context.ProductBrands.Any())
            {
                await SeedBrand(Context);
            }
            #endregion


            #region Products 

            if (!Context.Products.Any())
            {
                await SeedProduct(Context);
            }
            #endregion


            #endregion


            await SeedDeliveryMethod(Context);
           await Context.SaveChangesAsync();

        }



        #region Seed Data ( Brands)
        public static async Task SeedBrand(StoreContext Context)
        {
            //Read File As String 
            var BrandData = File.ReadAllText("../Souq.Repositorey/DataBase/DataSeed/brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
            if ((Brands is not null) && (Brands.Count > 0))
            {
                foreach (var item in Brands)
                {
                    await Context.ProductBrands.AddAsync(item);
                }
            }
        }
        #endregion




        #region Seed Data Of (Types)
        public static async Task SeedType(StoreContext Context)
        {
            var TypeData = File.ReadAllText("../Souq.Repositorey/Database/DataSeed/types.json");
            var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
            #region Check If Not Null and not Empty
            if (Types is not null && (Types.Count > 0))
            {
                foreach (var item in Types)
                {
                    await Context.ProductTypes.AddAsync(item);
                }
            }
            // Save Changes
            #endregion
        }
        #endregion













        #region Seed Data Of Products
        public static async Task SeedProduct(StoreContext Context)
        {
            var ProductData = File.ReadAllText("../Souq.Repositorey/Database/DataSeed/Products.Json"); //As String 
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductData); //As Json

            if (Products is not null && (Products.Count > 0))
            {
                foreach (var item in Products)
                {
                    await Context.AddAsync(item);
                }
            }


        } 
        #endregion




        public async static Task SeedDeliveryMethod(StoreContext Context)
        {

            var FilePath = File.ReadAllText("../Souq.Repositorey/DataBase/DataSeed/delivery.json");

            var Data = JsonSerializer.Deserialize<List<DeliveryMethod>>(FilePath);
            if(Data is not null && Data.Count()>0)
            {
                foreach (var item in Data)
                {
                    await Context.Set<DeliveryMethod>().AddAsync(item);
                }
            }

        }







    }
}
