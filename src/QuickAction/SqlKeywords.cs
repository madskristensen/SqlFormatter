using System.Collections.Generic;

public class SqlKeyword(string keyword, string description, string example)
{
    public string Keyword { get; } = keyword;
    public string Description { get; } = description;
    public string Example { get; } = example;

    public static List<SqlKeyword> All =>
        [
            new("ABORT",
                "Rolls back and terminates the current transaction, undoing all changes. Useful for error recovery or canceling operations.",
                "BEGIN TRANSACTION; INSERT INTO Orders (OrderID, CustomerID) VALUES (1001, 5); ABORT TRANSACTION;"
            ),
            new("ABS",
                "Returns the absolute (positive) value of a number. Ideal for normalizing negative values or calculating distances.",
                "SELECT ABS(Balance) AS PositiveBalance FROM Accounts WHERE Balance < 0;"
            ),
            new("ADD",
                "Extends a table by adding a new column, constraint, or feature. Used to evolve schemas without disrupting existing data.",
                "ALTER TABLE Employees ADD HireDate DATE DEFAULT GETDATE();"
            ),
            new("ALL",
                "Includes all rows in a subquery or operation, retaining duplicates (unlike DISTINCT). Common in comparisons.",
                "SELECT ProductName FROM Products WHERE Price > ALL (SELECT Price FROM Discounts WHERE Active = 1);"
            ),
            new("ALLOCATE",
                "Reserves a cursor for use in row-by-row processing. Sets up cursor-based operations.",
                "ALLOCATE OrderCursor;"
            ),
            new("ALTER",
                "Modifies an existing database object (e.g., table, column) without dropping it. Adapts schemas to new needs.",
                "ALTER TABLE Orders ALTER COLUMN OrderDate DATETIME NOT NULL;"
            ),
            new("AND",
                "Combines multiple conditions in a WHERE clause, requiring all to be true. Narrows result sets precisely.",
                "SELECT * FROM Employees WHERE Department = 'Sales' AND Salary > 50000;"
            ),
            new("ANY",
                "Compares a value to any value in a subquery, returning true if at least one matches. Flexible for partial matches.",
                "SELECT ProductName FROM Products WHERE Price > ANY (SELECT Price FROM Discounts WHERE Active = 1);"
            ),
            new("AS",
                "Assigns an alias to a column, table, or expression for readability or query simplification. Temporary renaming only.",
                "SELECT COUNT(*) AS OrderCount FROM Orders WHERE YEAR(OrderDate) = 2023;"
            ),
            new("ASC",
                "Sorts query results in ascending order (low to high). Default for ORDER BY if unspecified.",
                "SELECT ProductName, Price FROM Products ORDER BY Price ASC;"
            ),
            new("AUTHORIZATION",
                "Specifies ownership or permissions for a database object. Defines who controls it.",
                "CREATE TABLE Customers (ID INT, Name VARCHAR(50)) AUTHORIZATION dbo;"
            ),
            new("BACKUP",
                "Creates a copy of a database for recovery purposes. Essential for data protection.",
                "BACKUP DATABASE SalesDB TO DISK = 'D:\\Backups\\SalesDB_20230308.bak' WITH COMPRESSION;"
            ),
            new("BEGIN",
                "Starts a transaction or block of statements, often for ensuring data consistency. Pairs with COMMIT or ROLLBACK.",
                "BEGIN TRANSACTION; DELETE FROM Orders WHERE OrderDate < '2022-01-01'; COMMIT;"
            ),
            new("BETWEEN",
                "Filters rows where a value falls within an inclusive range (e.g., dates, numbers). Simplifies range queries.",
                "SELECT OrderID FROM Orders WHERE OrderDate BETWEEN '2023-01-01' AND '2023-12-31';"
            ),
            new("BIGINT",
                "Defines a large integer (64-bit) for big numbers. Suitable for IDs or counts exceeding INT limits.",
                "DECLARE @Population BIGINT = 7500000000; SELECT @Population;"
            ),
            new("BIT",
                "Defines a boolean data type (0 or 1). Compact for true/false flags.",
                "DECLARE @IsActive BIT = 1; SELECT * FROM Users WHERE Active = @IsActive;"
            ),
            new("BULK",
                "Imports large datasets from a file into a table efficiently. Speeds up data loading.",
                "BULK INSERT Customers FROM 'C:\\Data\\customers.csv' WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n');"
            ),
            new("CALL",
                "Invokes a stored procedure or function. Executes predefined logic.",
                "CALL UpdateInventory(101, 50);"
            ),
            new("CANCEL",
                "Stops the current operation or command. Useful for halting long-running tasks.",
                "CANCEL EXEC LongRunningProc;"
            ),
            new("CASE",
                "Returns a value based on conditional logic, like if-then-else. Great for custom data transformations.",
                "SELECT ProductName, CASE WHEN Stock < 10 THEN 'Low' ELSE 'Sufficient' END AS StockLevel FROM Products;"
            ),
            new("CAST",
                "Converts a value to a specified data type explicitly. Aligns types for operations or output.",
                "SELECT CAST(GETDATE() AS DATE) AS Today;"
            ),
            new("CATCH",
                "Begins a block to handle errors after a TRY block. Manages exceptions gracefully.",
                "BEGIN TRY INSERT INTO Orders (OrderID) VALUES (NULL); END TRY BEGIN CATCH PRINT 'Error: ' + ERROR_MESSAGE(); END CATCH;"
            ),
            new("CHAR",
                "Defines a fixed-length string with padded spaces. Useful for consistent string sizes.",
                "DECLARE @Gender CHAR(1) = 'M'; SELECT @Gender;"
            ),
            new("CHECK",
                "Enforces a condition on a column’s values. Ensures data meets rules.",
                "CREATE TABLE Employees (Age INT CHECK (Age >= 18));"
            ),
            new("CLOSE",
                "Shuts down a cursor, ending its use. Part of cursor lifecycle.",
                "DECLARE cur CURSOR FOR SELECT Name FROM Customers; OPEN cur; CLOSE cur;"
            ),
            new("COALESCE",
                "Returns the first non-NULL value from a list. Handles missing data effectively.",
                "SELECT COALESCE(Phone, Email, 'No Contact') AS ContactInfo FROM Customers;"
            ),
            new("COLUMN",
                "Identifies a column within a table or query. Core to SQL structure.",
                "CREATE TABLE Orders (OrderID INT, OrderDate DATE);"
            ),
            new("COMMIT",
                "Saves all changes in a transaction permanently. Finalizes successful operations.",
                "BEGIN TRANSACTION; UPDATE Inventory SET Quantity = Quantity - 1 WHERE ProductID = 101; COMMIT;"
            ),
            new("CONCAT",
                "Joins multiple strings into one. Useful for readable output.",
                "SELECT CONCAT(FirstName, ' ', LastName) AS FullName FROM Employees;"
            ),
            new("CONVERT",
                "Changes a value’s data type with formatting options (SQL Server). Adapts data for specific needs.",
                "SELECT CONVERT(VARCHAR, OrderDate, 1) AS USDate FROM Orders;"
            ),
            new("CREATE PROCEDURE",
                "Defines a stored procedure for reusable SQL logic. Encapsulates operations.",
                "CREATE PROCEDURE GetActiveCustomers AS BEGIN SELECT * FROM Customers WHERE Status = 'Active'; END;"
            ),
            new("CREATE",
                "Builds a new database object (e.g., table, view). Foundation of schema creation.",
                "CREATE TABLE Products (ProductID INT PRIMARY KEY, Name VARCHAR(50));"
            ),
            new("CROSS",
                "Generates a Cartesian product of two tables (all row combinations). Useful for exhaustive pairing.",
                "SELECT C.CustomerName, P.ProductName FROM Customers C CROSS JOIN Products P;"
            ),
            new("CURRENT_DATE",
                "Returns the current date without time (ANSI standard). Useful for date-only tracking.",
                "INSERT INTO Events (EventDate) VALUES (CURRENT_DATE);"
            ),
            new("CURRENT_TIMESTAMP",
                "Returns the current date and time (ANSI standard). Tracks real-time events.",
                "INSERT INTO Logs (Event, Time) VALUES ('Login', CURRENT_TIMESTAMP);"
            ),
            new("CURSOR",
                "Declares a cursor for row-by-row processing of query results. Enables iteration.",
                "DECLARE OrderCursor CURSOR FOR SELECT OrderID FROM Orders; OPEN OrderCursor;"
            ),
            new("DATABASE",
                "Creates or references a database container. Organizes data storage.",
                "CREATE DATABASE SalesDB;"
            ),
            new("DATE",
                "Defines a date-only data type. Stores dates without time.",
                "DECLARE @StartDate DATE = '2023-01-01'; SELECT @StartDate;"
            ),
            new("DATE_ADD",
                "Adds a time interval to a date (MySQL). Useful for date arithmetic.",
                "SELECT DATE_ADD('2023-01-01', INTERVAL 30 DAY) AS NewDate;"
            ),
            new("DATEDIFF",
                "Calculates the difference between two dates in specified units. Tracks time spans.",
                "SELECT DATEDIFF(DAY, OrderDate, GETDATE()) AS DaysSinceOrder FROM Orders;"
            ),
            new("DATETIME",
                "Defines a data type for date and time combined. Stores precise timestamps.",
                "DECLARE @EventTime DATETIME = GETDATE(); SELECT @EventTime;"
            ),
            new("DEALLOCATE",
                "Frees a cursor’s resources after use. Completes cursor cleanup.",
                "DECLARE cur CURSOR FOR SELECT Name FROM Customers; OPEN cur; CLOSE cur; DEALLOCATE cur;"
            ),
            new("DECIMAL",
                "Defines a fixed-precision number with scale. Accurate for financial data.",
                "DECLARE @Price DECIMAL(10,2) = 123.45; SELECT @Price;"
            ),
            new("DECLARE",
                "Defines a variable for use in scripts. Enables dynamic values.",
                "DECLARE @Total INT; SET @Total = (SELECT COUNT(*) FROM Orders); PRINT @Total;"
            ),
            new("DEFAULT",
                "Sets a fallback value for a column if none is provided. Simplifies data entry.",
                "CREATE TABLE Employees (Status VARCHAR(10) DEFAULT 'Active');"
            ),
            new("DELETE",
                "Removes rows from a table based on a condition. Deletes data permanently unless in a transaction.",
                "DELETE FROM Orders WHERE OrderDate < '2020-01-01';"
            ),
            new("DENSE_RANK",
                "Assigns a rank to rows within a partition, without gaps for ties. Useful for ranking without skips.",
                "SELECT Name, DENSE_RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS Rank FROM Employees;"
            ),
            new("DESC",
                "Sorts query results in descending order (high to low). Complements ASC in ORDER BY.",
                "SELECT ProductName, Price FROM Products ORDER BY Price DESC;"
            ),
            new("DISTINCT",
                "Eliminates duplicate rows from results. Summarizes unique values.",
                "SELECT DISTINCT Country FROM Customers;"
            ),
            new("DROP COLUMN",
                "Removes a column from a table. Alters schema by deletion.",
                "ALTER TABLE Customers DROP COLUMN Age;"
            ),
            new("DROP",
                "Deletes a database object (e.g., table) entirely. Irreversible, use cautiously.",
                "DROP TABLE TempData;"
            ),
            new("ELSE",
                "Specifies an alternative action if an IF condition is false. Completes conditional logic.",
                "IF (SELECT COUNT(*) FROM Orders) > 0 BEGIN PRINT 'Orders exist'; END ELSE BEGIN PRINT 'No orders'; END;"
            ),
            new("END",
                "Marks the end of a statement block (e.g., IF, BEGIN). Structures code.",
                "BEGIN UPDATE Customers SET Status = 'Active'; END;"
            ),
            new("ERROR_MESSAGE",
                "Returns the error text in a CATCH block. Diagnoses issues.",
                "BEGIN TRY INSERT INTO Orders (OrderID) VALUES (NULL); END TRY BEGIN CATCH SELECT ERROR_MESSAGE() AS Error; END CATCH;"
            ),
            new("EXCEPT",
                "Returns rows from the first query not present in the second, excluding duplicates. Finds differences.",
                "SELECT City FROM Customers EXCEPT SELECT City FROM Suppliers;"
            ),
            new("EXEC",
                "Runs a stored procedure or dynamic SQL. Executes prepared code.",
                "EXEC GetActiveCustomers;"
            ),
            new("EXECUTE",
                "Formal alternative to EXEC for running procedures or commands (SQL Server). Executes logic.",
                "EXECUTE UpdateInventory 101, 50;"
            ),
            new("EXISTS",
                "Checks if a subquery returns rows, returning true/false. Efficient for existence tests.",
                "SELECT CustomerName FROM Customers C WHERE EXISTS (SELECT 1 FROM Orders O WHERE O.CustomerID = C.CustomerID);"
            ),
            new("FETCH",
                "Retrieves the next row from a cursor. Iterates through results.",
                "DECLARE @ID INT; DECLARE cur CURSOR FOR SELECT CustomerID FROM Customers; OPEN cur; FETCH NEXT FROM cur INTO @ID; CLOSE cur; DEALLOCATE cur;"
            ),
            new("FLOAT",
                "Defines a floating-point number for approximate values. Good for decimals.",
                "DECLARE @Rate FLOAT = 0.075; SELECT @Rate * Price AS Tax FROM Orders;"
            ),
            new("FOR",
                "Controls loops or iterations, often with cursors. Processes rows sequentially.",
                "DECLARE @ID INT; DECLARE cur CURSOR FOR SELECT OrderID FROM Orders; OPEN cur; FETCH NEXT FROM cur INTO @ID; WHILE @@FETCH_STATUS = 0 BEGIN PRINT @ID; FETCH NEXT FROM cur INTO @ID; END; CLOSE cur; DEALLOCATE cur;"
            ),
            new("FOREIGN",
                "Sets a foreign key constraint to link tables. Maintains referential integrity.",
                "CREATE TABLE Orders (OrderID INT, CustomerID INT, FOREIGN KEY (CustomerID) REFERENCES Customers(ID));"
            ),
            new("FROM",
                "Specifies the table(s) to query. Core of data retrieval.",
                "SELECT Name, Age FROM Customers WHERE Country = 'USA';"
            ),
            new("FULL",
                "Performs a full outer join, including all rows from both tables with NULLs for non-matches. Comprehensive joining.",
                "SELECT C.CustomerName, O.OrderID FROM Customers C FULL OUTER JOIN Orders O ON C.CustomerID = O.CustomerID;"
            ),
            new("GETDATE",
                "Returns the current date and time in SQL Server. Timestamps actions.",
                "INSERT INTO Logs (Event, LogDate) VALUES ('Login', GETDATE());"
            ),
            new("GETUTCDATE",
                "Returns the current UTC date and time in SQL Server. Standardizes time globally.",
                "SELECT GETUTCDATE() AS UTCTime;"
            ),
            new("GO",
                "Separates SQL batches in SQL Server for independent execution. Organizes scripts.",
                "CREATE TABLE Temp (ID INT); GO INSERT INTO Temp VALUES (1); GO SELECT * FROM Temp; GO"
            ),
            new("GRANT",
                "Assigns permissions to users or roles. Manages access control.",
                "GRANT SELECT ON Customers TO SalesUser;"
            ),
            new("GROUP",
                "Aggregates rows by column values with functions like COUNT. Summarizes data.",
                "SELECT Department, COUNT(*) AS EmpCount FROM Employees GROUP BY Department;"
            ),
            new("HAVING",
                "Filters grouped results using aggregate conditions. Refines summaries.",
                "SELECT Department, AVG(Salary) FROM Employees GROUP BY Department HAVING AVG(Salary) > 60000;"
            ),
            new("IDENTITY",
                "Defines an auto-incrementing column value (SQL Server). Simplifies unique ID generation.",
                "CREATE TABLE Users (ID INT IDENTITY(1,1), Name VARCHAR(50));"
            ),
            new("IF",
                "Executes statements based on a condition. Adds logic to scripts.",
                "IF EXISTS (SELECT 1 FROM Orders) BEGIN PRINT 'Orders exist'; END;"
            ),
            new("IN",
                "Matches a column against a list of values. Simplifies multiple ORs.",
                "SELECT ProductName FROM Products WHERE CategoryID IN (1, 2, 3);"
            ),
            new("INDEX",
                "Creates an index to speed up queries. Boosts performance.",
                "CREATE INDEX idx_Name ON Customers (LastName);"
            ),
            new("INNER",
                "Performs an inner join, returning only matched rows. Standard join type.",
                "SELECT C.CustomerName, O.OrderDate FROM Customers C INNER JOIN Orders O ON C.CustomerID = O.CustomerID;"
            ),
            new("INPUT",
                "Defines parameters passed into procedures or functions. Enables dynamic inputs.",
                "CREATE PROCEDURE GetOrders @StartDate DATE AS SELECT * FROM Orders WHERE OrderDate >= @StartDate; EXEC GetOrders '2023-01-01';"
            ),
            new("INSERT",
                "Adds new rows to a table. Populates data.",
                "INSERT INTO Customers (Name, City) VALUES ('John Doe', 'New York');"
            ),
            new("INT",
                "Defines a 32-bit integer for whole numbers. Common for IDs.",
                "DECLARE @Count INT = (SELECT COUNT(*) FROM Orders); PRINT @Count;"
            ),
            new("INTERSECT",
                "Returns common rows from two queries, excluding duplicates. Identifies overlaps.",
                "SELECT City FROM Customers INTERSECT SELECT City FROM Suppliers;"
            ),
            new("INTO",
                "Stores query results in a new or existing table. Useful for archiving.",
                "SELECT CustomerID, Name INTO ActiveCustomers FROM Customers WHERE Status = 'Active';"
            ),
            new("IS NOT NULL",
                "Filters rows with non-NULL values in a column. Ensures data presence.",
                "SELECT OrderID FROM Orders WHERE ShippedDate IS NOT NULL;"
            ),
            new("IS NULL",
                "Filters rows with NULL values in a column. Identifies missing data.",
                "SELECT CustomerName FROM Customers WHERE Phone IS NULL;"
            ),
            new("ISNULL",
                "Replaces NULL with a specified value. Avoids NULL issues.",
                "SELECT ISNULL(Commission, 0) AS CommissionValue FROM Sales;"
            ),
            new("JOIN",
                "Combines rows from multiple tables based on a condition. Relational querying essential.",
                "SELECT P.ProductName, C.CategoryName FROM Products P JOIN Categories C ON P.CategoryID = C.CategoryID;"
            ),
            new("LEFT",
                "Performs a left outer join, including all left table rows and matching right table rows. Handles non-matches.",
                "SELECT C.CustomerName, O.OrderID FROM Customers C LEFT JOIN Orders O ON C.CustomerID = O.CustomerID;"
            ),
            new("LEN",
                "Returns the length of a string in characters. Validates or formats text.",
                "SELECT Name FROM Customers WHERE LEN(Name) > 10;"
            ),
            new("LIKE",
                "Filters rows using pattern matching with wildcards. Searches partial text.",
                "SELECT ProductName FROM Products WHERE ProductName LIKE '%Pro%';"
            ),
            new("LIMIT",
                "Restricts the number of rows returned (MySQL/PostgreSQL). Useful for pagination.",
                "SELECT * FROM Products ORDER BY Price DESC LIMIT 5;"
            ),
            new("LOWER",
                "Converts a string to lowercase. Standardizes text.",
                "SELECT LOWER(Email) AS EmailAddress FROM Users;"
            ),
            new("MERGE",
                "Performs insert, update, or delete on a target table based on source data. Synchronizes tables.",
                "MERGE INTO Inventory T USING NewStock S ON T.ProductID = S.ProductID WHEN MATCHED THEN UPDATE SET T.Quantity = T.Quantity + S.Quantity WHEN NOT MATCHED THEN INSERT (ProductID, Quantity) VALUES (S.ProductID, S.Quantity);"
            ),
            new("NEXT",
                "Advances a cursor to the next row. Used with FETCH for iteration.",
                "FETCH NEXT FROM OrderCursor INTO @OrderID, @CustomerID;"
            ),
            new("NOLOCK",
                "Query hint in SQL Server to read uncommitted data. Improves performance at consistency risk.",
                "SELECT * FROM Orders WITH (NOLOCK);"
            ),
            new("NOT IN",
                "Excludes rows where a column matches any value in a list. Opposite of IN.",
                "SELECT * FROM Employees WHERE Department NOT IN ('HR', 'Sales');"
            ),
            new("NOT NULL",
                "Enforces that a column cannot contain NULL. Ensures data integrity.",
                "CREATE TABLE Orders (OrderID INT NOT NULL, OrderDate DATE);"
            ),
            new("NOT",
                "Negates a condition in a WHERE clause. Inverts logic.",
                "SELECT * FROM Products WHERE NOT Discontinued = 1;"
            ),
            new("NOW",
                "Returns the current date and time (MySQL/PostgreSQL). Tracks real-time.",
                "INSERT INTO Events (EventName, EventTime) VALUES ('Meeting', NOW());"
            ),
            new("NULL",
                "Represents an unknown or missing value. Handles incomplete data.",
                "UPDATE Customers SET Phone = NULL WHERE Phone = '';"
            ),
            new("NULLS FIRST",
                "Sorts NULL values before non-NULL in ORDER BY (PostgreSQL/Oracle). Controls NULL placement.",
                "SELECT Name FROM Customers ORDER BY Age DESC NULLS FIRST;"
            ),
            new("NULLS LAST",
                "Sorts NULL values after non-NULL in ORDER BY (PostgreSQL/Oracle). Controls NULL placement.",
                "SELECT Name FROM Customers ORDER BY Age DESC NULLS LAST;"
            ),
            new("NVARCHAR",
                "Defines a variable-length Unicode string. Supports international characters.",
                "DECLARE @FullName NVARCHAR(100) = N'José González'; SELECT @FullName;"
            ),
            new("OBJECT_ID",
                "Returns the unique ID of a database object in SQL Server. Queries metadata.",
                "IF OBJECT_ID('dbo.Orders') IS NOT NULL PRINT 'Orders table exists';"
            ),
            new("OFFSET",
                "Skips a specified number of rows in a result set (SQL Server with FETCH). Enables pagination.",
                "SELECT Name FROM Customers ORDER BY ID OFFSET 10 ROWS FETCH NEXT 5 ROWS ONLY;"
            ),
            new("ON",
                "Specifies the condition for a JOIN or update/delete target. Links data.",
                "SELECT O.OrderID, C.CustomerName FROM Orders O JOIN Customers C ON O.CustomerID = C.CustomerID;"
            ),
            new("OPEN",
                "Activates a cursor for data retrieval. Precedes FETCH.",
                "DECLARE cur CURSOR FOR SELECT Name FROM Customers; OPEN cur;"
            ),
            new("OPTION",
                "Provides query hints to tweak execution plans in SQL Server. Optimizes performance.",
                "SELECT * FROM Orders OPTION (RECOMPILE);"
            ),
            new("OR",
                "Combines conditions, returning rows where at least one is true. Broadens filters.",
                "SELECT * FROM Products WHERE CategoryID = 1 OR Price < 10;"
            ),
            new("ORDER",
                "Sorts query results by columns, ascending or descending. Enhances readability.",
                "SELECT ProductName, Price FROM Products ORDER BY Price DESC;"
            ),
            new("OUTER",
                "Used with JOIN to include non-matching rows (e.g., LEFT OUTER). Expands join scope.",
                "SELECT C.CustomerName, O.OrderID FROM Customers C FULL OUTER JOIN Orders O ON C.CustomerID = O.CustomerID;"
            ),
            new("OUTPUT",
                "Captures affected rows from INSERT, UPDATE, or DELETE. Useful for auditing.",
                "UPDATE Orders SET Status = 'Shipped' OUTPUT deleted.Status, inserted.Status INTO AuditLog WHERE OrderID = 1001;"
            ),
            new("OVER",
                "Defines a window for aggregate or ranking functions. Enables advanced analytics.",
                "SELECT Name, AVG(Salary) OVER (PARTITION BY Department) AS AvgSalary FROM Employees;"
            ),
            new("PARTITION",
                "Divides rows into groups for window functions (used with OVER). Segments data.",
                "SELECT Name, RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS Rank FROM Employees;"
            ),
            new("PERCENT",
                "Expresses results as a percentage or limits rows by percentage (SQL Server). Useful for proportions.",
                "SELECT TOP 10 PERCENT ProductName, Price FROM Products ORDER BY Price DESC;"
            ),
            new("PRIMARY",
                "Defines a primary key to uniquely identify rows. Enforces uniqueness.",
                "CREATE TABLE Employees (EmployeeID INT PRIMARY KEY, Name VARCHAR(50));"
            ),
            new("PRINT",
                "Outputs a message to the client in SQL Server. Aids debugging.",
                "PRINT 'Processing complete for ' + CAST(GETDATE() AS VARCHAR);"
            ),
            new("PROCEDURE",
                "Defines a stored procedure in SQL Server. Encapsulates reusable logic.",
                "CREATE PROCEDURE UpdateStock @ProductID INT, @Qty INT AS BEGIN UPDATE Inventory SET Quantity = Quantity + @Qty WHERE ProductID = @ProductID; END;"
            ),
            new("PUBLIC",
                "Grants permissions to all database users. Broadens access.",
                "GRANT SELECT ON Products TO PUBLIC;"
            ),
            new("RANDOM",
                "Generates a random number (e.g., RAND() in MySQL). Useful for sampling.",
                "SELECT ProductName FROM Products ORDER BY RAND() LIMIT 1;"
            ),
            new("RANK",
                "Assigns a rank to rows within a partition, with gaps for ties. Useful for leaderboard-style ranking.",
                "SELECT Name, RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS Rank FROM Employees;"
            ),
            new("RECURSIVE",
                "Enables a CTE to reference itself for hierarchical data. Solves recursive problems.",
                "WITH RECURSIVE OrgChart AS (SELECT EmployeeID, ManagerID, Name FROM Employees WHERE ManagerID IS NULL UNION ALL SELECT E.EmployeeID, E.ManagerID, E.Name FROM Employees E JOIN OrgChart O ON E.ManagerID = O.EmployeeID) SELECT * FROM OrgChart;"
            ),
            new("RENAME",
                "Changes a database object’s name (SQL Server uses sp_rename). Updates schema.",
                "EXEC sp_rename 'dbo.OldTable', 'NewTable';"
            ),
            new("REPLACE",
                "Substitutes all occurrences of a string with another. Cleans data.",
                "SELECT REPLACE(Phone, '-', '') AS CleanPhone FROM Customers;"
            ),
            new("RETURN",
                "Exits a procedure or function, optionally returning a value. Controls flow.",
                "CREATE PROCEDURE CheckStock @ProductID INT AS BEGIN IF (SELECT Quantity FROM Inventory WHERE ProductID = @ProductID) < 0 RETURN -1; RETURN 0; END;"
            ),
            new("RIGHT",
                "Performs a right outer join, including all right table rows and matching left rows. Handles non-matches.",
                "SELECT C.CustomerName, O.OrderID FROM Customers C RIGHT JOIN Orders O ON C.CustomerID = O.CustomerID;"
            ),
            new("ROLLBACK",
                "Undoes all changes in a transaction. Reverts on error.",
                "BEGIN TRANSACTION; DELETE FROM Orders WHERE OrderID = 1001; ROLLBACK TRANSACTION;"
            ),
            new("ROUND",
                "Rounds a number to a specified decimal place. Formats numeric data.",
                "SELECT ROUND(UnitPrice * Quantity, 2) AS Total FROM OrderDetails;"
            ),
            new("ROW_NUMBER",
                "Assigns a unique sequential number to each row within a partition. Useful for enumeration.",
                "SELECT Name, ROW_NUMBER() OVER (PARTITION BY Department ORDER BY HireDate) AS RowNum FROM Employees;"
            ),
            new("SELECT",
                "Retrieves data from tables. Core of SQL querying.",
                "SELECT CustomerName, City FROM Customers WHERE Country = 'USA';"
            ),
            new("SET",
                "Assigns a value to a variable or modifies session options. Controls behavior.",
                "SET @Total = (SELECT SUM(Price) FROM Orders); PRINT @Total;"
            ),
            new("SMALLINT",
                "Defines a small integer (-32,768 to 32,767). Saves space for limited ranges.",
                "DECLARE @DaysWorked SMALLINT = 15; SELECT @DaysWorked * 8 AS Hours;"
            ),
            new("STRING_AGG",
                "Concatenates row values into a single string with a separator (SQL Server 2017+). Summarizes data into lists.",
                "SELECT STRING_AGG(ProductName, '; ') AS ProductList FROM Products WHERE CategoryID = 1;"
            ),
            new("SUBSTRING",
                "Extracts a portion of a string based on position and length. Useful for text manipulation.",
                "SELECT SUBSTRING(Name, 1, 5) AS ShortName FROM Customers;"
            ),
            new("TABLE",
                "Defines a table structure to store data. Basis of relational design.",
                "CREATE TABLE Vendors (VendorID INT PRIMARY KEY, Name VARCHAR(100));"
            ),
            new("TEMPORARY",
                "Creates a temporary table that exists for the session (common in MySQL/PostgreSQL). Useful for transient data.",
                "CREATE TEMPORARY TABLE TempOrders (OrderID INT, Amount DECIMAL(10,2));"
            ),
            new("TEXT",
                "Defines a large string type (deprecated in SQL Server; use VARCHAR(MAX)). Stores lengthy text.",
                "DECLARE @Notes TEXT = 'Detailed customer feedback...';"
            ),
            new("THROW",
                "Raises a custom error in SQL Server, typically in CATCH. Enhances error handling.",
                "BEGIN TRY IF (SELECT COUNT(*) FROM Orders) < 0 THROW 50001, 'Invalid order count', 1; END TRY BEGIN CATCH SELECT ERROR_MESSAGE(); END CATCH;"
            ),
            new("TINYINT",
                "Defines a very small integer (0 to 255). Ideal for small counters.",
                "DECLARE @Priority TINYINT = 3; SELECT * FROM Tasks WHERE Priority <= @Priority;"
            ),
            new("TO_DATE",
                "Converts a string to a date (Oracle/PostgreSQL). Ensures date handling.",
                "SELECT TO_DATE('2023-05-15', 'YYYY-MM-DD') AS StartDate;"
            ),
            new("TO_NUMBER",
                "Converts a string to a number (Oracle). Validates numeric input.",
                "SELECT TO_NUMBER('123.45') * 2 AS DoubleValue;"
            ),
            new("TOP",
                "Limits the number of rows returned in SQL Server. Complements ORDER BY for top results.",
                "SELECT TOP 5 ProductName, Price FROM Products ORDER BY Price DESC;"
            ),
            new("TRANSACTION",
                "Groups statements into a single unit, ensuring all succeed or fail. Protects consistency.",
                "BEGIN TRANSACTION; INSERT INTO Accounts (AccountID, Balance) VALUES (100, 500); COMMIT;"
            ),
            new("TRANSLATE",
                "Replaces specific characters in a string (SQL Server 2017+). Simplifies edits.",
                "SELECT TRANSLATE('12-34-56', '-', '.') AS DotFormat;"
            ),
            new("TRIGGER",
                "Automatically runs code on table events (e.g., INSERT). Enforces rules.",
                "CREATE TRIGGER trg_LogInsert ON Orders AFTER INSERT AS BEGIN INSERT INTO OrderLog (OrderID, LogDate) SELECT OrderID, GETDATE() FROM inserted; END;"
            ),
            new("TRIM",
                "Removes leading and trailing spaces (or characters in some DBMS). Cleans strings.",
                "SELECT TRIM('   Hello World   ') AS CleanText;"
            ),
            new("TRUNC",
                "Truncates a number or date to a specified precision (Oracle/PostgreSQL). Removes decimals or time.",
                "SELECT TRUNC(123.456, 1) AS Truncated FROM Dual;"
            ),
            new("TRUNCATE",
                "Deletes all rows from a table without logging, preserving structure. Faster than DELETE.",
                "TRUNCATE TABLE TempData;"
            ),
            new("TRY",
                "Begins an error-handling block in SQL Server, paired with CATCH. Manages exceptions.",
                "BEGIN TRY INSERT INTO Orders (OrderID) VALUES (NULL); END TRY BEGIN CATCH PRINT 'Insert failed'; END CATCH;"
            ),
            new("TRY_CAST",
                "Attempts a type conversion, returning NULL if it fails (SQL Server 2012+). Avoids errors.",
                "SELECT TRY_CAST('123.45' AS DECIMAL(5,2)) AS ValidNumber;"
            ),
            new("TRY_CONVERT",
                "Tries to convert a value to a type, returning NULL on failure (SQL Server 2012+). Handles invalid data.",
                "SELECT TRY_CONVERT(INT, 'abc') AS Number;"
            ),
            new("UNION ALL",
                "Combines query results, keeping duplicates. Faster than UNION.",
                "SELECT City FROM Customers UNION ALL SELECT City FROM Suppliers;"
            ),
            new("UNION",
                "Combines query results, removing duplicates. Merges distinct data.",
                "SELECT City FROM Customers UNION SELECT City FROM Suppliers;"
            ),
            new("UPDATE",
                "Modifies existing rows based on a condition. Updates data.",
                "UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = 'IT';"
            ),
            new("UPPER",
                "Converts a string to uppercase. Standardizes text.",
                "SELECT UPPER(LastName) AS Surname FROM Customers;"
            ),
            new("USE",
                "Switches to a specified database in SQL Server. Sets query context.",
                "USE SalesDB; SELECT * FROM Orders;"
            ),
            new("VALUES",
                "Provides data for an INSERT statement. Specifies row content.",
                "INSERT INTO Products (ProductID, Name, Price) VALUES (1001, 'Laptop', 999.99);"
            ),
            new("VARCHAR",
                "Defines a variable-length string with a maximum size. Efficient for text.",
                "DECLARE @Description VARCHAR(200) = 'High-quality headphones'; SELECT @Description;"
            ),
            new("VIEW",
                "Defines a virtual table based on a query. Simplifies complex queries.",
                "CREATE VIEW ActiveCustomers AS SELECT Name, City FROM Customers WHERE Status = 'Active';"
            ),
            new("WHERE",
                "Filters rows based on a condition. Narrows results.",
                "SELECT * FROM Orders WHERE OrderDate >= '2023-01-01' AND Total > 500;"
            ),
            new("WHILE",
                "Repeats statements while a condition is true. Loops in SQL.",
                "DECLARE @Counter INT = 0; WHILE @Counter < 5 BEGIN PRINT @Counter; SET @Counter = @Counter + 1; END;"
            ),
            new("WINDOW",
                "Defines a subset of rows for analytic functions (OVER clause). Enables calculations.",
                "SELECT EmployeeID, Salary, AVG(Salary) OVER (PARTITION BY Department) AS DeptAvg FROM Employees;"
            ),
            new("WITH",
                "Introduces a CTE for temporary result sets. Simplifies complex queries.",
                "WITH RecentOrders AS (SELECT * FROM Orders WHERE OrderDate > '2023-01-01') SELECT CustomerID, COUNT(*) FROM RecentOrders GROUP BY CustomerID;"
            )
        ];
}