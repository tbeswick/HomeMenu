using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeMenu.Models
{
    public class MenuIngredientRepository : IMenuIngredientRepository
    {

        private string connectionString;


        public MenuIngredientRepository(string conn)
        {
            connectionString = conn;
        }


        public async Task<bool> CreateItem(MenuIngredientModel model)
        {
            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "INSERT INTO MenuIngredients ([Name],[Type],[Added],[Modifed]) " +
                                  "VALUES (" +
                                  "@Name,@Catergory,@Added@Modifed)";
                    await db.QueryAsync(qry, model);

                    return true;
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateMachine exception " + ex.Message);

            }


            return false;
        }

        public async Task<bool> UpdateItem(MenuIngredientModel model)
        {
            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "UPDATE MenuIngredients SET [Name]=@Name, [Type]=@Type,[Modifed]=@Modifed " +
                                  "WHERE Id=@Id";
                    await db.QueryAsync(qry, model);

                    return true;
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateMachine exception " + ex.Message);

            }

            return false;
        }


        public async Task<IList<MenuIngredientModel>> GetItems(string UserId)
        {

            try
            {
                using (SqlConnection db =
                    new SqlConnection(connectionString))
                {

                    await db.OpenAsync();

                    string qry = "SELECT * FROM MenuIngredients WHERE [UserId]=@userid";
                    var result = await db.QueryAsync<MenuIngredientModel>(qry, new {userid = UserId });

                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateMachine exception " + ex.Message);

            }

            return null;

        }


    }
}