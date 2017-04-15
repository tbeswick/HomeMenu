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

                    string qry = "INSERT INTO MenuItems ([UserId],[Name],[Catergory],[Added],[Modified]) " +
                                  "VALUES (" +
                                  "@UserId,@Name,@Catergory,@Added,@Modified); SELECT CAST(SCOPE_IDENTITY() as int)";
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

        public async Task<bool> UpdateItem(MenuItemModel model)
        {
            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "UPDATE MenuItems SET [Name]=@Name, [Catergory]=@Catergory,[Modified]=@Modified " +
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