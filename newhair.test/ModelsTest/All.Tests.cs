using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairList;
using System;
using HairList.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairListTest.Models
{
    [TestClass]
    public class customerTests : IDisposable
    {
      public void Dispose()
        {
          customer.DeleteAll();
          Employee.DeleteAll();
        }
      public customerTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=eric_nicolas_test;";
        }

    [TestMethod]
      public void Save_SavesToDatabase_customerList()
      {
        customer testItem = new customer("hello",1,1);
        testItem.save();
        List<customer> result = customer.GetAll();
        List<customer> testList = new List<customer>{testItem};

        CollectionAssert.AreEqual(testList, result);
      }
    [TestMethod]
      public void Save_SavesToDatabase_employeeList()
      {
        Employee testItem = new Employee("hello",1);
        testItem.save();
        List<Employee> result = Employee.GetAll();
        List<Employee> testList = new List<Employee>{testItem};

        CollectionAssert.AreEqual(testList, result);
      }

      [TestMethod]
      public void Find_FindsItemInDatabase_Item()
      {
        customer testItem = new customer("hello",1,1);
        testItem.save();
        List<customer> testList = customer.GetAll();
        CollectionAssert.AreEqual(testList, Employee.Find(1));
      }
    }
    }
