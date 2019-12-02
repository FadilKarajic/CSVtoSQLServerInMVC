/********************************************************
   * This script views CustomerView *
*********************************************************/

USE Customers;
GO

SELECT TOP (20000) [CustomerId]
      ,[CustomerName]
      ,[City]
      ,[State]
      ,[PostalCode]
      ,[Country]
      ,[Market]
      ,[Region]
  FROM [Customers].[dbo].[CustomerView]