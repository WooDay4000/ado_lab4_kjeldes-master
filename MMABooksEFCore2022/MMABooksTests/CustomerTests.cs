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
    // Customer table.
    public class CustomerTests
    {
        // A instance of the MMABooksContext class,
        // used in Entity Framework interactions
        // with the MMABooks.
        MMABooksContext dbContext;
        // A Customer object used in tests.
        // The ? marks it as nullable,
        // allowing its fields to be null.
        Customer? c;
        // A list that will be used to store 
        // Customers for tests, with ? making
        // this nullable allowing to be null.
        List<Customer>? customers;

        [SetUp]
        // This will run before each test, running
        // the usp_testingResetCustomer1Data,
        // usp_testingResetCusomer2Data, and
        // usp_testingResetCustomer3Data stored
        // procedures to have the Customer table
        // in the database restart back to how it
        // was before a test.
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer1Data()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCusomer2Data()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer3Data()");
        }

        [Test]
        // The GetAllTest method verifies the "read"
        // functionality of CRUD operations for the
        // Customer table. It attempts to retrieve a
        // full list of Customer records from the
        // Customers database table using ToList. The
        // initial Assert.AreEqual checks that the
        // number of retrieved records matches the
        // expected count. The following Assert.AreEqual
        // statements validate that the fields of the
        // first Customer record in the list match the
        // expected values, ensuring the data was retrieved
        // accurately.
        public void GetAllTest()
        {
            customers = dbContext.Customers.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(157, customers[0].CustomerId);
            Assert.AreEqual("Abeyatunge, Derek", customers[0].Name);
            Assert.AreEqual("1414 S. Dairy Ashford", customers[0].Address);
            Assert.AreEqual("North Chili", customers[0].City);
            Assert.AreEqual("NY", customers[0].State);
            Assert.AreEqual("14514", customers[0].ZipCode);
            PrintAll(customers);
        }

        [Test]
        // The GetByPrimaryKeyTest method verifies the "read" 
        // functionality of CRUD operations for the Customer
        // table. It attempts to retrieve a specific Customer
        // record from the Customers database table using its
        // primary key with Find. The Assert.IsNotNull checks
        // if the Find operation successfully retrieved the
        // customer record. The following Assert.AreEqual
        // statements validate that the fields of the retrieved
        // Customer record match the expected values.
        public void GetByPrimaryKeyTest()
        {
            c = dbContext.Customers.Find(157);
            Assert.IsNotNull(c);
            Assert.AreEqual(157, c.CustomerId);
            Assert.AreEqual("Abeyatunge, Derek", c.Name);
            Assert.AreEqual("1414 S. Dairy Ashford", c.Address);
            Assert.AreEqual("North Chili", c.City);
            Assert.AreEqual("NY", c.State);
            Assert.AreEqual("14514", c.ZipCode);
            Console.WriteLine(c);
        }

        [Test]
        // The GetUsingWhere method verifies the "read"
        // functionality of CRUD operations by testing
        // the ability to filter Customer records. It 
        // retrieves specific Customer records from the
        // Customers table using the Where clause to select
        // only those with "OR" in the State field. The initial 
        // Assert.AreEqual checks that the correct number
        // of records were returned based on the filter.
        // The following Assert.AreEqual statements confirm
        // that the properties of the first Customer in the
        // list match the expected values.
        public void GetUsingWhere()
        {
            // get a list of all of the customers who live in OR
            customers = dbContext.Customers.Where(c => c.State.Equals("OR")).ToList();
            Assert.AreEqual(5, customers.Count);
            Assert.AreEqual(12, customers[0].CustomerId);
            Assert.AreEqual("Swenson, Vi", customers[0].Name);
            Assert.AreEqual("102 Forest Drive", customers[0].Address);
            Assert.AreEqual("Albany", customers[0].City);
            Assert.AreEqual("OR", customers[0].State);
            Assert.AreEqual("97321", customers[0].ZipCode);
        }

        [Test]
        // The GetWithInvoicesTest method verifies the "read"
        // functionality of CRUD operations by testing the
        // ability to retrieve a specific Customer record along
        // with all related Invoice records using the Include
        // method. This filters to retrieve only the Customer
        // with a CustomerId of 20, alongside any invoices related
        // to this customer. Using Assert.AreEqual to confirm that
        // the correct Customer record was received, including
        // expected number of related Invoices too.
        public void GetWithInvoicesTest()
        {
            // get the customer whose id is 20 and all of the invoices for that customer
            c = dbContext.Customers.Include("Invoices").Where(c => c.CustomerId == 20).SingleOrDefault();
            Assert.AreEqual(20, c.CustomerId);
            Assert.AreEqual(3, c.Invoices.Count);
            Console.WriteLine(c);
        }

        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the customer id, name, statecode and statename
            var customers = dbContext.Customers.Join(
               dbContext.States,
               c => c.State,
               s => s.StateCode,
               (c, s) => new { c.CustomerId, c.Name, c.State, s.StateName }).OrderBy(r => r.StateName).ToList();
            Assert.AreEqual(696, customers.Count);
            // I wouldn't normally print here but this lets you see what each object looks like
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }
        }

        [Test]
        // The DeleteTest method verifies the "delete" 
        // functionality of CRUD operations by testing 
        // the ability to delete a specific Customer 
        // record in the database Customers table. It
        // uses Find to retrieve the customer record
        // with the given CustomerId, then applies the
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
            c = dbContext.Customers.Find(26);
            dbContext.Customers.Remove(c);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Customers.Find(26));
        }

        [Test]
        // The CreateTest method verifies the "create"
        // functionality of CRUD operations by testing
        // the ability to create a Customer record and
        // add it to the database Customers table. Where
        // the created customer object is called with the
        // Add method to have it marked as created in the
        // database context. With SaveChanges being called
        // to commit the creation, having the created
        // Customer record be added to the database. Using
        // Assert.IsNotNull with Where to try and grab the
        // newly created Customer record, where if the
        // result isn't null then it was successfully
        // created.
        public void CreateTest()
        {
            c = new Customer();
            c.Name = "Ryan Qwerty";
            c.Address = "123 Walkaway Lane";
            c.City = "Youngstown";
            c.State = "OH";
            c.ZipCode = "44501";
            dbContext.Customers.Add(c);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Customers.Where(c => c.Name.Equals("Ryan Qwerty")));
        }

        [Test]
        // The UpdateTest method verifies the "update"
        // functionality of CRUD operations by testing
        // the ability to update a Customer record in
        // the database Customers table. We use Find
        // with a CustomerId to retrieve the Customer
        // record that will be updated, modify specific
        // fields of the record with new values, and then
        // call the Update method to mark this record as updated.
        // SaveChanges is called to commit this change, 
        // updating the customer record in the database with
        // the new data.
        // Using Assert.AreEqual to compare the values of the
        // updated record with the expected values, where 
        // if they match, then the update was successful.
        public void UpdateTest()
        {
            c = dbContext.Customers.Find(30);
            c.Name = "Andrew, Susan";
            c.Address = "123 New Sheet Way";
            dbContext.Customers.Update(c);
            dbContext.SaveChanges();
            Assert.AreEqual("Andrew, Susan", c.Name);
            Assert.AreEqual("123 New Sheet Way", c.Address);
        }

        public void PrintAll(List<Customer> customers)
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine(c);
            }
        }
    }
}