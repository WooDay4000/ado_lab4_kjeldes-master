using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    // This is an automatically created class based on
    // an existing database schema, with this being the
    // Customer table from the MMABooks database, with
    // each property here representing a column from
    // said table. This is used to interact with database
    // data using C# objects rather than SQL queries to be
    // able to access, create, delete, and update records
    // or information from database tables.
    public partial class Customer
    {
        // A parameterless constructor called when a new
        // instance of the Customer class is created.
        // Initializes an empty HashSet collection for
        // Invoice objects so invoices can be added to
        // this Customer. HashSet is used to ensure each
        // invoice is unique within the collection. 
        // When loaded from the database, this collection
        // will include any existing invoices linked to 
        // the Customer via a foreign key.
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;

        // These are navigation properties used by
        // Entity Framework to manage relationships
        // between tables. They allow EF to handle
        // relational data through object-oriented
        // properties, eliminating the need for SQL
        // joins or direct foreign key management.
        public virtual State StateNavigation { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
