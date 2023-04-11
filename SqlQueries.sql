--1. Get the list of non-discontinued products including the category name ordered by product name.


SELECT  p.*, c.CategoryName FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE p.Discontinued=0
ORDER BY p.ProductName;


-- 2. Display the name of Nancy Davolio's clients.
SELECT c.CompanyName AS ClientName, e.FirstName + ' ' + e.LastName AS EmployeeName FROM Customers c
INNER JOIN Orders o ON o.CustomerID = c.CustomerID
INNER JOIN Employees e ON o.EmployeeID = e.EmployeeID
WHERE e.FirstName='Nancy' AND e.LastName='Davolio'
ORDER BY c.CompanyName;

-- 3. Display the total billed per year for employee Steven Buchanan.
SELECT SUM(od.UnitPrice) AS TotalBill, DATEPART(year, o.OrderDate) AS BillYear, e.FirstName + ' ' + e.LastName AS EmployeeName FROM Orders o
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
INNER JOIN Employees e ON o.EmployeeID = e.EmployeeID
WHERE e.FirstName='Steven' AND e.LastName = 'Buchanan'

GROUP BY DATEPART(year, o.OrderDate), e.FirstName, e.LastName;

-- 4. Show the name of the employees who report directly or indirectly to Andrew Fuller.
WITH cteEmployees as
(
    SELECT e1.EmployeeId,e1.FirstName, e1.LastName, 1 as lvl
    FROM Employees e1
    WHERE e1.ReportsTo in (SELECT EmployeeID FROM Employees where FirstName='Andrew' AND LastName = 'Fuller')
    UNION ALL
    SELECT e2.EmployeeID, e2.FirstName, e2.LastName, lvl+1
    FROM Employees e2  
    INNER JOIN cteEmployees ce ON e2.ReportsTo = ce.EmployeeID
)
SELECT *
FROM cteEmployees
ORDER BY lvl, EmployeeID