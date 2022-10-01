using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classADO_12
{
    public class test
    {
        private string sqlConStr;
        private SqlConnection sqlCon;

        public void sqlconnection()
        {
            sqlConStr = @"Data Source = DESKTOP-TA8BLLL\SQLEXPRESS; Initial Catalog = product; Integrated Security = true;";
            sqlCon = new SqlConnection(sqlConStr);
        }

        //public void insertDataPurchase(string pur_no, DateTime date, double total_amount)
        //{
        //    sqlCon.Open();

        //    SqlDataAdapter adp = new SqlDataAdapter("insert into purchase (pur_no, date, total_amount) values('" + pur_no + "', '" + date + "', '" + total_amount + "')", sqlCon);

        //    adp.SelectCommand.ExecuteNonQuery();

        //    sqlCon.Close();
        //}

        //public void insertDataPurchaseProduct(int pur_id, string item, int qty, double amount)
        //{
        //    sqlCon.Open();

        //    SqlDataAdapter adp = new SqlDataAdapter("insert into purchase_product (pur_id, item, qty, amount) values('" + pur_id + "', '" + item + "', '" + qty + "', '" + amount + "')", sqlCon);

        //    adp.SelectCommand.ExecuteNonQuery();

        //    sqlCon.Close();
        //}

        //public void purchaseDelete(int id)
        //{
        //    sqlCon.Open();

        //    SqlDataAdapter adp = new SqlDataAdapter($"delete from purchase where pur_id = {id}", sqlCon);

        //    adp.SelectCommand.ExecuteNonQuery();

        //    sqlCon.Close();
        //}

        public string getUniqueNumber()
        {
            string lastId, subLastId, date, uniqueId;
            int intSubLastId;

            sqlconnection();

            sqlCon.Open();

            SqlCommand cmd = new SqlCommand("spGetLastId",sqlCon);

            var temp = cmd.ExecuteScalar();

            sqlCon.Close();

            if(temp == null)
            {
                subLastId = "0001";
            }
            else
            {
                lastId = temp.ToString();

                subLastId = lastId.Substring((lastId.Length - 4), 4);

                intSubLastId = Convert.ToInt32(subLastId);

                intSubLastId++;

                subLastId = intSubLastId.ToString();

                subLastId = subLastId.PadLeft(4, '0');
            }

            date = DateTime.Now.ToString("yyMMdd");

            uniqueId = string.Concat(date, subLastId);

            return uniqueId;
        }
    }
}
