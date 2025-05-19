------------GET ALL EMPLOYEE ------------------
CREATE PROCEDURE GetAllEmployees
AS
BEGIN
    WITH EmployeeCTE AS (
        SELECT Id, Name, Email, Phone, Address FROM Employees
    )
    SELECT * FROM EmployeeCTE
END;

------------ADD EMPLOYEE------------------
CREATE PROCEDURE AddEmployee
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(50),
    @Address NVARCHAR(255)
AS
BEGIN
    INSERT INTO Employees (Name, Email, Phone, Address)
    VALUES (@Name, @Email, @Phone, @Address)
END

------------ UPDATE EMPLOYEE ---------------
CREATE PROCEDURE UpdateEmployee
    @Id INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(50),
    @Address NVARCHAR(255)
AS
BEGIN
    UPDATE Employees
    SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address
    WHERE Id = @Id
END

----------- DELETE EMPLOYEE -------------
CREATE PROCEDURE DeleteEmployee
    @Id INT
AS
BEGIN
    DELETE FROM Employees WHERE Id = @Id
END
