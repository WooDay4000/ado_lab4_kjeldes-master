DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_testingResetProductData`()
BEGIN

-- disable foreign key constraints first
	set foreign_key_checks=0;
    set sql_safe_updates=0;
	delete from Products;
    
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('A4CS      ', 'Murach''s ASP.NET 4 Web Programming with C# 2010', 56.5000, 4637);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('A4VB      ', 'Murach''s ASP.NET 4 Web Programming with VB 2010', 56.5000, 3974);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('ADC4      ', 'Murach''s ADO.NET 4 with C# 2010', 56.5000, 3756);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('ADV4      ', 'Murach''s ADO.NET 4 with VB 2010', 56.5000, 4538);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('CRFC      ', 'Murach''s CICS Desk Reference', 50.0000, 1865);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('CS10      ', 'Murach''s C# 2010', 56.5000, 5136);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('DB1R      ', 'DB2 for the COBOL Programmer, Part 1 (2nd Edition)', 42.0000, 4825);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('DB2R      ', 'DB2 for the COBOL Programmer, Part 2 (2nd Edition)', 45.0000, 621);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('JAVP      ', 'Murach''s Java Programming', 56.5000, 3455);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('JSP2      ', 'Murach''s JAVA Servlets and JSP (2nd Edition)', 52.5000, 4999);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('MCBL      ', 'Murach''s Structured COBOL', 62.5000, 2386);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('MCCP      ', 'Murach''s CICS for the COBOL Programmer', 54.0000, 2368);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('MDOM      ', 'Murach''s JavaScript and DOM Scripting', 54.5000, 6937);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('SQ12      ', 'Murach''s SQL Server 2012', 57.5000, 2465);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('VB10      ', 'Murach''s Visual Basic 2010', 56.5000, 2193);
INSERT Products (ProductCode, Description, UnitPrice, OnHandQuantity) VALUES ('ZJLR      ', 'Murach''s OS/390 and z/os JCL', 62.5000, 677);

-- enable foreign key constraints
	set foreign_key_checks=1;
	set sql_safe_updates=1;
END$$
DELIMITER ;
