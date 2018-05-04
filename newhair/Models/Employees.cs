using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairList;
using System;

namespace HairList.Models
{
    public class Employee
    {
      private int _id;
      private string _name;

    public Employee (string name, int id = 0){
      _id = id;
      _name = name;
    }

    public int GetId()
    {
      return _id;
    }
    public string Getname()
    {
      return _name;
    }

      public static List<Employee> GetAll()
      {
          List<Employee> allItems = new List<Employee> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM Employee;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int itemId = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            Employee newItem = new Employee(name, itemId);
            allItems.Add(newItem);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allItems;
      }
       public void save(){
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO `employee` (`name`) VALUES (@Itemname);";

          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@Itemname";
          name.Value = this._name;
          cmd.Parameters.Add(name);

          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
        }
        public static List<customer> Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `customer` WHERE stylistid = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int testid = 0;
            string name = "";
            List<customer>foundItemlist = new List<customer>();
            while (rdr.Read())
            {
                testid = rdr.GetInt32(0);
                name = rdr.GetString(1);
                int stylistid = rdr.GetInt32(2);
                customer foundItem= new customer(name, stylistid, testid);
                foundItemlist.Add(foundItem);
            }

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }

            return foundItemlist; 

        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM employee;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public override bool Equals(System.Object otheremployee)
        {
          if (!(otheremployee is Employee))
          {
            return false;
          }
          else
          {
            Employee newemployee = (Employee) otheremployee;
            bool idEquality = (this.GetId() == newemployee.GetId());
            bool nameEquality = (this.Getname() == newemployee.Getname());
            return (idEquality && nameEquality);
          }
        }
}
}
