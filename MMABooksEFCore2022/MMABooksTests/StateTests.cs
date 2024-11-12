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
    // States table.
    public class StateTests
    {
        // ignore this warning about making dbContext nullable.
        // if you add the ?, you'll get a warning wherever you use dbContext
        // ---
        // A instance of the MMABooksContext class,
        // used in Entity Framework interactions
        // with the MMABooks.
        MMABooksContext dbContext;
        // A State object used in tests.
        // The ? marks it as nullable,
        // allowing its fields to be null.
        State? s;
        // A list that will be used to store 
        // State objects for tests, with ? making
        // this nullable allowing this to be null.
        List<State>? states;

        [SetUp]
        // Changed which stored procedure is used, because it wasn't restarting the table
        // which caused some tests to fail.
        // ---
        // This will run before each test, running
        // the usp_testingResetStateData stored
        // procedure to have the States table
        // in the database restart back to how it
        // was before a test.
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetStateData()");
        }

        [Test]
        // The GetAllTest method verifies the "read"
        // functionality of CRUD operations for the
        // States table. It attempts to retrieve a
        // full list of State records from the
        // States database table using ToList. The
        // initial Assert.AreEqual checks that the
        // number of retrieved records matches the
        // expected count. The following Assert.AreEqual
        // statement validates that the fields of the
        // first State record in the list match the
        // expected values, ensuring the data was retrieved
        // accurately.
        public void GetAllTest()
        {
            states = dbContext.States.OrderBy(s => s.StateName).ToList();
            Assert.AreEqual(53, states.Count);
            Assert.AreEqual("Alabama", states[0].StateName);
            PrintAll(states);
        }

        [Test]
        // The GetByPrimaryKeyTest method verifies the "read" 
        // functionality of CRUD operations for the States
        // table. It attempts to retrieve a specific State
        // record from the States database table using its
        // StateCode primary key with Find. The Assert.IsNotNull
        // checks if the Find operation successfully retrieved the
        // State record. The following Assert.AreEqual
        // statement validates that the fields of the retrieved
        // State record match the expected values.
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.States.Find("OR");
            Assert.IsNotNull(s);
            Assert.AreEqual("Ore", s.StateName);
            Console.WriteLine(s);
        }

        [Test]
        // The GetUsingWhere method verifies the "read"
        // functionality of CRUD operations by testing
        // the ability to filter State records. It 
        // retrieves specific State records from the
        // States table using the Where clause to select
        // only those whose StateName starts with a "A".
        // The initial Assert.AreEqual checks that the
        // correct number of records were returned based
        // on the filter. The following Assert.AreEqual
        // statement confirms that the properties of the
        // first State in the list match the expected
        // values.
        public void GetUsingWhere()
        {
            states = dbContext.States.Where(s => s.StateName.StartsWith("A")).OrderBy(s => s.StateName).ToList();
            Assert.AreEqual(4, states.Count);
            Assert.AreEqual("Alabama", states[0].StateName);
            PrintAll(states);
        }

        [Test]
        // The GetWithInvoicesTest method verifies the "read"
        // functionality of CRUD operations by testing the
        // ability to retrieve a specific State record along
        // with all related Customer records using the Include
        // method. This filters to retrieve only the State
        // with a StateCode of "OR", alongside any Customers related
        // to this State. Using Assert.AreEqual to confirm that
        // the correct State record was received, including
        // the expected number of related Customers too.
        public void GetWithCustomersTest()
        {
            s = dbContext.States.Include("Customers").Where(s => s.StateCode == "OR").SingleOrDefault();
            Assert.IsNotNull(s);
            Assert.AreEqual("Ore", s.StateName);
            Assert.AreEqual(5, s.Customers.Count);
            Console.WriteLine(s);
        }

        [Test]
        // The DeleteTest method verifies the "delete" 
        // functionality of CRUD operations by testing 
        // the ability to delete a specific State 
        // record in the database States table. It
        // uses Find to retrieve the State record
        // with the given StateCode, then applies the
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
            s = dbContext.States.Find("HI");
            dbContext.States.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.States.Find("HI"));
        }


        [Test]
        // The CreateTest method verifies the "create"
        // functionality of CRUD operations by testing
        // the ability to create a State record and
        // add it to the database States table. Where
        // the created State object is called with the
        // Add method to have it marked as created in the
        // database context. With SaveChanges being called
        // to commit the creation, having the created
        // State record be added to the database. Using
        // Assert.IsNotNull with Where to try and grab the
        // newly created State record, where if the
        // result isn't null then it was successfully
        // created.
        public void CreateTest()
        {
            s = new State();
            s.StateCode = "W2";
            s.StateName = "Wyoming2";
            dbContext.States.Add(s);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.States.Find("W2"));
        }

        [Test]
        // The UpdateTest method verifies the "update"
        // functionality of CRUD operations by testing
        // the ability to update a State record in
        // the database States table. We use Find
        // with a StateCode to retrieve the State
        // record that will be updated, modify specific
        // fields of the record with new values, and then
        // call the Update method to mark this record as updated.
        // SaveChanges is called to commit this change, 
        // updating the state record in the database with
        // the new data.
        // Using Assert.AreEqual to compare the values of the
        // updated record with the expected values, where 
        // if they match, then the update was successful.
        public void UpdateTest()
        {
            s = dbContext.States.Find("OR");
            s.StateName = "Oregon";
            dbContext.States.Update(s);
            dbContext.SaveChanges();
            s = dbContext.States.Find("OR");
            Assert.AreEqual("Oregon", s.StateName);
        }

        // The PrintAll method is used for debugging purposes.
        // It loops through a list of State objects and prints
        // each State object to the console.
        public void PrintAll(List<State> states)
        {
            foreach (State s in states)
            {
                Console.WriteLine(s);
            }
        }
    }
}