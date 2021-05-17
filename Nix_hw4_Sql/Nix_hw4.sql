--1. Как называется самый дорогой товар из товарной категории №1?
SELECT ProductName 
FROM dbo.Products
INNER JOIN (SELECT 
CategoryID,
MAX(UnitPrice) as maxPrice
FROM dbo.Products 
WHERE CategoryID = 1
GROUP BY CategoryID) as m
ON UnitPrice = maxPrice

---------------------------------------------------------------------

--2. В какие города заказы комплектовались более десяти дней?
SELECT DISTINCT ShipCity
FROM dbo.Orders 
WHERE DATEDIFF(DAY, OrderDate, ShippedDate) > 10

---------------------------------------------------------------------

--3. Какие покупатели до сих пор ждут отгрузки своих заказов?
SELECT DISTINCT ContactName
FROM dbo.Orders INNER JOIN dbo.Customers
ON Orders.CustomerID = Customers.CustomerID
WHERE ShippedDate is NULL

---------------------------------------------------------------------

--4. Скольких покупателей обслужил продавец, лидирующий по общему количеству заказов?

--4.1 Вариант 1 (Возможно так длать неправильно, поэтому сделал и вариант 2)
SELECT t.c FROM 
(SELECT TOP (1) EmployeeID, COUNT(DISTINCT CustomerID) c, COUNT(OrderID) o
FROM dbo.Orders
GROUP By EmployeeID
ORDER BY o DESC) t
--4.2 Вариант 2 
SELECT t.c FROM
(SELECT EmployeeID, COUNT(DISTINCT CustomerID) c, COUNT(OrderID) o
FROM dbo.Orders
GROUP By EmployeeID) t
INNER JOIN
(SELECT MAX(t.o) m
FROM
(SELECT EmployeeID, COUNT(DISTINCT CustomerID) c, COUNT(OrderID) o
FROM dbo.Orders
GROUP By EmployeeID) t ) mt ON t.o = mt.m




-----------------------------------------------------------------------

--5. Сколько французских городов обслужил продавец №1 в 1997-м?

SELECT COUNT(DISTINCT ShipCity) c
FROM dbo.Orders
WHERE ShipCountry = 'France' AND EmployeeID = 1 AND YEAR(OrderDate) = 1997
GROUP By EmployeeID

----------------------------------------------------------------------

--6. В каких странах есть города, в которые было отправлено больше двух заказов?

SELECT ShipCountry
FROM dbo.Orders
WHERE ShippedDate IS NOT NULL
GROUP BY ShipCity, ShipCountry
HAVING COUNT(ShipCity) > 2

-----------------------------------------------------------------------

--7. Перечислите названия товаров, которые были проданы в количестве менее 1000 штук (quantity)?

SELECT ProductName
FROM Products Inner Join [Order Details] 
ON Products.ProductID = [Order Details].ProductID
GROUP BY ProductName
HAVING SUM(Quantity) < 1000

------------------------------------------------------------------------

--8. Как зовут покупателей, которые делали заказы с доставкой в другой город (не в тот, в котором они прописаны)?

SELECT DISTINCT ContactName FROM 
dbo.Customers
INNER JOIN 
dbo.Orders
ON Customers.CustomerID = Orders.CustomerID
WHERE Customers.City <> Orders.ShipCity

---------------------------------------------------------------------------

--9. Товарами из какой категории в 1997-м году заинтересовалось больше всего компаний, имеющих факс?

--9.1 Вариант с временной таблицей
SELECT CategoryName, COUNT(DISTINCT Orders.CustomerID) c
INTO #t
FROM Orders INNER JOIN [Order Details]
ON Orders.OrderID = [Order Details].OrderID
INNER JOIN Products 
ON [Order Details].ProductID = Products.ProductID
INNER JOIN Customers ON Customers.CustomerID = Orders.CustomerID
INNER JOIN Categories ON Categories.CategoryID = Products.CategoryID
WHERE Fax IS NOT NULL AND YEAR(OrderDate) = 1997
GROUP BY CategoryName

SELECT CategoryName 
FROM #t
INNER JOIN
(SELECT MAX(#t.c) m
FROM #t) m ON #t.c =m.m 

DROP TABLE #t 

--9.2 Вариант без временной таблицы (то же самое что и с временной таблицей,
--но вместо #t 2 раза пишется вложенный запрос)
SELECT CategoryName FROM
(SELECT CategoryName, COUNT(DISTINCT Orders.CustomerID) c
FROM Orders INNER JOIN [Order Details]
ON Orders.OrderID = [Order Details].OrderID
INNER JOIN Products 
ON [Order Details].ProductID = Products.ProductID
INNER JOIN Customers ON Customers.CustomerID = Orders.CustomerID
INNER JOIN Categories ON Categories.CategoryID = Products.CategoryID
WHERE Fax IS NOT NULL AND YEAR(OrderDate) = 1997
GROUP BY CategoryName) t
INNER JOIN (SELECT MAX(t.c) m FROM
(SELECT CategoryName, COUNT(DISTINCT Orders.CustomerID) c
FROM Orders INNER JOIN [Order Details]
ON Orders.OrderID = [Order Details].OrderID
INNER JOIN Products 
ON [Order Details].ProductID = Products.ProductID
INNER JOIN Customers ON Customers.CustomerID = Orders.CustomerID
INNER JOIN Categories ON Categories.CategoryID = Products.CategoryID
WHERE Fax IS NOT NULL AND YEAR(OrderDate) = 1997
GROUP BY CategoryName) t) m ON t.c =m.m 

---------------------------------------------------------------------------

--10. Сколько всего единиц товаров (то есть, штук – Quantity) продал каждый продавец (имя, фамилия) осенью 1996 года?

SELECT LastName, FirstName, SUM(Quantity) s
FROM Orders INNER JOIN [Order Details] 
ON Orders.OrderID = [Order Details].OrderID
INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID
WHERE OrderDate > '31-08-1996' AND OrderDate < '01-12-1996'
GROUP BY LastName, FirstName
