using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    // This is an automatically created class based on
    // an existing database schema, with this being the
    // State table from the MMABooks database, with
    // each property here representing a column from
    // said table. This is used to interact with database
    // data using C# objects rather than SQL queries to be
    // able to access, create, delete, and update records
    // or information from database tables.
    public partial class State
    {
        // A parameterless constructor called when a new
        // instance of the State class is created.
        // Initializes an empty HashSet collection for
        // Customer objects so Customers can be added to
        // this State. HashSet is used to ensure each
        // Customer is unique within the collection. 
        // When loaded from the database, this collection
        // will include any existing Customers linked to 
        // the State via a foreign key.
        public State()
        {
            Customers = new HashSet<Customer>();
        }

        public string StateCode { get; set; } = null!;
        public string StateName { get; set; } = null!;

        // This is a navigation property used by Entity
        // Framework to manage a relationship between
        // tables. They allow EF to handle relational
        // data through object-oriented properties,
        // eliminating the need for SQL joins or direct
        // foreign key management.
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
