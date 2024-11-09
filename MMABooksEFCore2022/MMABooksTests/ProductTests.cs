using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        MMABooksContext dbContext;
        Product? p;
        List<Product>? products;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetProductData()");
        }

        [Test]
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
        public void DeleteTest()
        {
            p = dbContext.Products.Find("ADC4");
            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.States.Find("ADC4"));
        }

        [Test]
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

        public void PrintAll(List<Product> products)
        {
            foreach (Product p in products)
            {
                Console.WriteLine(p);
            }
        }
    }
}