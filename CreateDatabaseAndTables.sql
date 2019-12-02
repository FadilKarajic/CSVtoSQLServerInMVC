/********************************************************
   * This script creates the database named Customers and Customers table*
*********************************************************/
USE master;
GO

IF  DB_ID('Customers') IS NOT NULL
DROP DATABASE Customers;
GO

CREATE DATABASE Customers;
GO

USE Customers;

-- create the tables for the database



CREATE TABLE Customers (
  CustomerId       INT   PRIMARY KEY   IDENTITY (1,1) NOT NULL,
  CustomerName     VARCHAR(120)   NOT NULL,
  City             VARCHAR(75)    NOT NULL,
  State            VARCHAR(75)    NOT NULL,
  PostalCode       VARCHAR(10)    NOT NULL,
  Country	   VARCHAR(75)    NOT NULL,
  Market	   VARCHAR(75)    NOT NULL,
  Region	   VARCHAR(10)    NOT NULL
 
);





