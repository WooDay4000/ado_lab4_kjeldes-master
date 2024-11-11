using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    // This is an automatically created class based on
    // an existing database schema, with this being the
    // Invoice table from the MMABooks database, with
    // each property here representing a column from
    // said table. This is used to interact with database
    // data using C# objects rather than SQL queries to be
    // able to access, create, delete, and update records
    // or information from database tables.
    public partial class Invoice
    {
        // A parameterless constructor called when a new
        // instance of the Invoice class is created.
        // Initializes an empty HashSet collection for
        // Invoicelineitem objects so Invoicelineitems
        // can be added to this Invoice. HashSet is used
        // to ensure each Invoicelineitem is unique within the
        // collection. When loaded from the database, this
        // collection will include any existing Invoicelineitems
        // linked to the Invoice via a foreign key.
        public Invoice()
        {
            Invoicelineitems = new HashSet<Invoicelineitem>();
        }

        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Shipping { get; set; }
        public decimal InvoiceTotal { get; set; }

        // These are navigation properties used by
        // Entity Framework to manage relationships
        // between tables. They allow EF to handle
        // relational data through object-oriented
        // properties, eliminating the need for SQL
        // joins or direct foreign key management.
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; }
    }
}
