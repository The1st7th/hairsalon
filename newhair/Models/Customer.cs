using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairList;
using System;

namespace HairList.Models
{
    public class customer
    {
        private string _name;
        private int _id;
        private int _stylistid;


        public customer(string name, int stylistid = 0, int Id=0)
        {
          _id = Id;
          _name = name;
          _stylistid = stylistid;
        }

        public void save(){
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO `customer` (`name`,`stylistid`) VALUES (@customername, @stylenumber);";

          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@customername";
          name.Value = this._name;
          cmd.Parameters.Add(name);

          MySqlParameter stylistid = new MySqlParameter();
          stylistid.ParameterName = "@stylenumber";
          stylistid.Value = this._stylistid;
          cmd.Parameters.Add(stylistid);

          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
        }

        public string Getname()
        {
            return _name;
        }
        public void Setname(string newname)
        {
            _name = newname;
        }
        public int GetId()
        {
          return _id;
        }
        public static List<customer> GetAll()
        {
            List<customer> allcustomers = new List<customer> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customer;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int customerId = rdr.GetInt32(0);
              string customername = rdr.GetString(1);
              int stylistidd = rdr.GetInt32(2);
              customer newcustomer = new customer(customername, stylistidd, customerId);
              allcustomers.Add(newcustomer);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allcustomers;
        }
 
        public override bool Equals(System.Object othercustomer)
        {
          if (!(othercustomer is customer))
          {
            return false;
          }
          else
          {
            customer newcustomer = (customer) othercustomer;
            bool idEquality = (this.GetId() == newcustomer.GetId());
            bool nameEquality = (this.Getname() == newcustomer.Getname());
            bool otherEquality = (this.Getstyleid() == newcustomer.Getstyleid());
            return (idEquality && nameEquality);
          }
        }

        public static void delete(int id){
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM `customer` Where id = @thisId;";

          MySqlParameter thisId = new MySqlParameter();
          thisId.ParameterName = "@thisId";
          thisId.Value = id;
          cmd.Parameters.Add(thisId);

          cmd.ExecuteNonQuery();

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
        }

        public string Getstylist(){
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `employee` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = _stylistid;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string stylist = "";

            while (rdr.Read())
            {
                stylist = rdr.GetString(1);
            } 

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }

            return stylist;  

        }
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM customer;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public int Getstyleid()
        {
            return _stylistid;
        }
    }
}
