using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using System.Diagnostics;
using System.Data.SqlClient;

namespace HomeMenu.Models
{
    public class MenuItemRepository : IMenuItemRepository
    {

        private string connectionString;


        public MenuItemRepository(string conn)
        {
            connectionString = conn;
        }


        public async Task<int> CreateItem(MenuItemModel model)
        {
            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "INSERT INTO MenuItems ([UserId],[Name],[Catergory],[Image],[Added],[Modified]) " +
                                  "VALUES (" +
                                  "@UserId,@Name,@Catergory,@Image,@Added,@Modified); SELECT CAST(SCOPE_IDENTITY() as int)";
                    var result = await db.QueryAsync<int>(qry, model);
                    return result.Single();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateMachine exception " + ex.Message);

            }


            return -1;
        }


        public async Task<int> AddIngredientToMenuItem(AddIngredientToItemModel model)
        {


            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "INSERT INTO IngredientToItem ([MenuItem],[MenuIngredient]) " +
                                  "VALUES (" +
                                  "@ItemId,@IngredientId); SELECT CAST(SCOPE_IDENTITY() as int)";
                    var result = await db.QueryAsync<int>(qry, model);
                    return result.Single();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("AddIngredientToMenuItem exception " + ex.Message);

            }


            return -1;


        }


        public async Task<int> DeleteIngredientToMenuItem(AddIngredientToItemModel model)
        {


            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "DELETE FROM IngredientToItem WHERE [MenuItem]=@ItemId AND [MenuIngredient]=@IngredientId";
                        
                    await db.QueryAsync<int>(qry, model);
                    return 1;
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteIngredientToMenuItem exception " + ex.Message);

            }


            return 0;


        }



        public async Task<IList<MenuItemIngredientModel>>GetCurrentItemIngredients(long itemId)
        {


            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "SELECT * FROM MenuItemsWithIngredients WHERE [ItemId]=@itemid";
                    var result = await db.QueryAsync<MenuItemIngredientModel>(qry, new { itemid = itemId });

                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetCurrentItemIngredients exception " + ex.Message);

            }

            return null;



        } 



        public async Task<bool> UpdateItem(MenuItemModel model)
        {
            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "UPDATE MenuItems SET [Name]=@Name, [Catergory]=@Catergory,[Image]=@Image,[Modified]=@Modified " +
                                  "WHERE Id=@Id";
                    await db.QueryAsync<int>(qry, model);

                    return true;
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateMachine exception " + ex.Message);

            }

            return false;
        }


        public async Task<IList<MenuItemModel>> GetItems(string userId)
        {

            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "SELECT * FROM MenuItems WHERE [UserId]=@userid";
                    var result = await db.QueryAsync<MenuItemModel>(qry, new { userid = userId });

                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateMachine exception " + ex.Message);

            }

            return null;

        }


        public async Task<MenuItemModel> GetItemById(long id)
        {

            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "SELECT * FROM MenuItems WHERE [Id]=@id";
                    var result = await db.QueryAsync<MenuItemModel>(qry, new { Id = id });

                    return result.FirstOrDefault();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetItemById exception " + ex.Message);

            }

            return null;

        }


        public async Task<IList<ItemCatergoriesModel>> GetItemCatergories(string userid)
        {

            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "SELECT * FROM ItemCatergories WHERE [UserId]=@id OR [UserId]=@all";
                    var result = await db.QueryAsync<ItemCatergoriesModel>(qry, new { id = userid, all = "all" });

                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetItemCatergories exception " + ex.Message);

            }

            return null;

        }


    }
}