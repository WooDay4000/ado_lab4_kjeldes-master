using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    // Tests for CRUD operations and Entity
    // Framework's ability to interact with
    // the database. This class specifically
    // tests these operations with the
    // Products table.
    public class ProductTests
    {
        // A instance of the MMABooksContext class,
        // used in Entity Framework interactions
        // with the MMABooks.
        MMABooksContext dbContext;
        // A Product object used in tests.
        // The ? marks it as nullable,
        // allowing its fields to be null.
        Product? p;
        // A list that will be used to store 
        // Product objects for tests, with ? making
        // this nullable allowing to be null.
        List<Product>? products;

        [SetUp]
        // This will run before each test, running
        // the usp_testingResetProductData stored
        // procedure to have the Product table
        // in the database restart back to how it
        // was before a test.
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetProductData()");
        }

        [Test]
        // The GetAllTest method verifies the "read"
        // functionality of CRUD operations for the
        // Products table. It attempts to retrieve a
        // full list of Product records from the
        // Products database table using ToList. The
        // initial Assert.AreEqual checks that the
        // number of retrieved records matches the
        // expected count. The following Assert.AreEqual
        // statements validate that the fields of the
        // first Product record in the list match the
        // expected values, ensuring the data was retrieved
        // accurately.
        public void GetAllTest()
        {
            products = dbContext.Products.OrderBy(p => p.OnHandQuantity).ToList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("DB2R", products[0].ProductCode);
            Assert.AreEqual("DB2 for the COBOL Programmer, Part 2 (2nd Edition)", products[0].Description);
            Assert.AreEqual(45, products[0].UnitPrice);
            Assert.AreEqual(621, products[0].OnHandQuantity);
            PrintAll(products);
        }

        [Test]
        // The GetByPrimaryKeyTest method verifies the "read" 
        // functionality of CRUD operations for the Products
        // table. It attempts to retrieve a specific Product
        // record from the Products database table using its
        // ProductCode primary key with Find. The Assert.IsNotNull
        // checks if the Find operation successfully retrieved the
        // Product record. The following Assert.AreEqual
        // statements validate that the fields of the retrieved
        // Product record match the expected values.
        public void GetByPrimaryKeyTest()
        {
            p = dbContext.Products.Find("ADV4");
            Assert.IsNotNull(p);
            Assert.AreEqual("ADV4", p.ProductCode);
            Assert.AreEqual("Murach's ADO.NET 4 with VB 2010", p.Description);
            Assert.AreEqual(56.50, p.UnitPrice);
            Assert.AreEqual(4538, p.OnHandQuantity);
            Console.WriteLine(p);
        }

        [Test]
        // The GetUsingWhere method verifies the "read"
        // functionality of CRUD operations by testing
        // the ability to filter Product records. It 
        // retrieves specific Product records from the
        // Products table using the Where clause to select
        // only those with "56.50" in the UnitPrice field.
        // The initial Assert.AreEqual checks that the correct
        // number of records were returned based on the filter.
        // The following Assert.AreEqual statements confirm
        // that the properties of the first Product in the
        // list match the expected values.
        public void GetUsingWhere()
        {
            // get a list of all of the products that have a unit price of 56.50
            products = dbContext.Products.Where(p => p.UnitPrice.Equals(56.50m)).OrderBy(p => p.OnHandQuantity).ToList();
            Assert.AreEqual(7, products.Count);
            Assert.AreEqual("VB10", products[0].ProductCode);
            Assert.AreEqual("Murach's Visual Basic 2010", products[0].Description);
            Assert.AreEqual(56.50, products[0].UnitPrice);
            Assert.AreEqual(2193, products[0].OnHandQuantity);
            PrintAll(products);
        }

        [Test]
        public void GetWithCalculatedFieldTest()
        {
            // get a list of objects that include the productcode, unitprice, quantity and inventoryvalue
            var products = dbContext.Products.Select(
            p => new { p.ProductCode, p.UnitPrice, p.OnHandQuantity, Value = p.UnitPrice * p.OnHandQuantity }).
            OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        [Test]
        // The DeleteTest method verifies the "delete" 
        // functionality of CRUD operations by testing 
        // the ability to delete a specific Product 
        // record in the database Products table. It
        // uses Find to retrieve the Product record
        // with the given ProductCode, then applies the
        // Remove method to mark it for deletion in the
        // database context. Finally, SaveChanges is
        // called to commit the deletion, permanently
        // removing the record from the database. 
        // Using Assert.IsNull, Find is called again 
        // to check if the record can still be retrieved. 
        // If the result is null, it confirms the record 
        // was successfully deleted.
        public void DeleteTest()
        {
            p = dbContext.Products.Find("ADC4");
            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.States.Find("ADC4"));
        }

        [Test]
        // The CreateTest method verifies the "create"
        // functionality of CRUD operations by testing
        // the ability to create a Product record and
        // add it to the database Products table. Where
        // the created Product object is called with the
        // Add method to have it marked as created in the
        // database context. With SaveChanges being called
        // to commit the creation, having the created
        // Product record be added to the database. Using
        // Assert.IsNotNull with Where to try and grab the
        // newly created Product record, where if the
        // result isn't null then it was successfully
        // created.
        public void CreateTest()
        {
            p = new Product();
            p.ProductCode = "WERT";
            p.Description = "Something About Coding Book";
            p.UnitPrice = 25.25m;
            p.OnHandQuantity = 100;
            dbContext.Products.Add(p);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Products.Find("WERT"));
        }

        [Test]
        // The UpdateTest method verifies the "update"
        // functionality of CRUD operations by testing
        // the ability to update a Product record in
        // the database Products table. We use Find
        // with a ProductCode to retrieve the Product
        // record that will be updated, modify specific
        // fields of the record with new values, and then
        // call the Update method to mark this record as updated.
        // SaveChanges is called to commit this change, 
        // updating the Product record in the database with
        // the new data.
        // Using Assert.AreEqual to compare the values of the
        // updated record with the expected values, where 
        // if they match, then the update was successful.
        public void UpdateTest()
        {
            p = dbContext.Products.Find("CS10");
            p.Description = "Murach's C# 2010 (OUTDATED)";
            p.UnitPrice = 28.25m;
            p.OnHandQuantity = 4136;
            dbContext.SaveChanges();
            p = dbContext.Products.Find("CS10");
            Assert.AreEqual("Murach's C# 2010 (OUTDATED)", p.Description);
            Assert.AreEqual(28.25m, p.UnitPrice);
            Assert.AreEqual(4136, p.OnHandQuantity);
        }

        // The PrintAll method is used for debugging purposes.
        // It loops through a list of Project objects and prints
        // each Project object to the console.
        public void PrintAll(List<Product> products)
        {
            foreach (Product p in products)
            {
                Console.WriteLine(p);
            }
        }
    }
}