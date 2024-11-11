using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    // This is an automatically created class based on
    // an existing database schema, with this being the
    // Invoicelineitem table from the MMABooks database, with
    // each property here representing a column from
    // said table. This is used to interact with database
    // data using C# objects rather than SQL queries to be
    // able to access, create, delete, and update records
    // or information from database tables.
    public partial class Invoicelineitem
    {
        public int InvoiceId { get; set; }
        public string ProductCode { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }

        // These are navigation properties used by
        // Entity Framework to manage relationships
        // between tables. They allow EF to handle
        // relational data through object-oriented
        // properties, eliminating the need for SQL
        // joins or direct foreign key management.
        public virtual Invoice Invoice { get; set; } = null!;
        public virtual Product ProductCodeNavigation { get; set; } = null!;
    }
}
