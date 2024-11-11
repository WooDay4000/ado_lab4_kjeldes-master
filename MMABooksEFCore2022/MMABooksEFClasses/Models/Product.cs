using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    // This is an automatically created class based on
    // an existing database schema, with this being the
    // Product table from the MMABooks database, with
    // each property here representing a column from
    // said table. This is used to interact with database
    // data using C# objects rather than SQL queries to be
    // able to access, create, delete, and update records
    // or information from database tables.
    public partial class Product
    {
        // A parameterless constructor called when a new
        // instance of the Product class is created.
        // Initializes an empty HashSet collection for
        // Invoicelineitem objects so Invoicelineitems
        // can be added to this Product. HashSet is used to
        // ensure each Invoicelineitem is unique within the
        // collection. When loaded from the database, this
        // collection ill include any existing Invoicelineitems
        // linked to the Product via a foreign key.
        public Product()
        {
            Invoicelineitems = new HashSet<Invoicelineitem>();
        }

        public string ProductCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int OnHandQuantity { get; set; }

        // This is a navigation property used by Entity
        // Framework to manage a relationship between
        // tables. They allow EF to handle relational
        // data through object-oriented properties,
        // eliminating the need for SQL joins or direct
        // foreign key management.
        public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; }
    }
}
