using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        MMABooksContext dbContext;
        Customer? c;
        List<Customer>? customers;
        // Goal of this is to make these tests.
        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer1Data()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCusomer2Data()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer3Data()");
        }

        [Test]
        public void GetAllTest()
        {
            customers = dbContext.Customers.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(157, customers[0].CustomerId);
            Assert.AreEqual("Abeyatunge, Derek", customers[0].Name);
            Assert.AreEqual("North Chili", customers[0].City);
            Assert.AreEqual("NY", customers[0].State);
            Assert.AreEqual("14514", customers[0].ZipCode);
            PrintAll(customers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            c = dbContext.Customers.Find(157);
            Assert.IsNotNull(c);
            Assert.AreEqual(157, c.CustomerId);
            Assert.AreEqual("Abeyatunge, Derek", c.Name);
            Assert.AreEqual("North Chili", c.City);
            Assert.AreEqual("NY", c.State);
            Assert.AreEqual("14514", c.ZipCode);
            Console.WriteLine(c);
        }

        [Test]
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
        public void GetWithInvoicesTest()
        {
           // get the customer whose id is 20 and all of the invoices for that customer
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
        public void DeleteTest()
        {

        }

        [Test]
        public void CreateTest()
        {
            
        }

        [Test]
        public void UpdateTest()
        {

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