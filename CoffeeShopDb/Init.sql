/* First we'll create some logins. One per context database. */
IF NOT EXISTS (SELECT [loginname] FROM master.dbo.syslogins WHERE [name] = 'Cashier')
BEGIN
  CREATE LOGIN Cashier WITH PASSWORD = 'IDealWithMoney!@';
END
GO

IF NOT EXISTS (SELECT [loginname] FROM master.dbo.syslogins WHERE [name] = 'Barista')
BEGIN
  CREATE LOGIN Barista WITH PASSWORD = 'IMakeYouCoffee!@';
END
GO

IF NOT EXISTS (SELECT [loginname] FROM master.dbo.syslogins WHERE [name] = 'Baker')
BEGIN
  CREATE LOGIN Baker WITH PASSWORD = 'IMakeYouBagels!@';
END
GO

/* We'll need a database per context to keep our systems separated. */
IF NOT EXISTS (SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = 'CoffeeShop')
BEGIN
  CREATE DATABASE CoffeeShop;
END
GO

IF NOT EXISTS (SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = 'Barista')
BEGIN
  CREATE DATABASE Barista
END
GO

IF NOT EXISTS (SELECT [name] FROM master.dbo.sysdatabases WHERE [name] = 'Bakery')
BEGIN
  CREATE DATABASE Bakery
END
GO

/* Set up the CoffeeShop database */
USE CoffeeShop;
GO

IF DATABASE_PRINCIPAL_ID('Cashier') IS NULL
BEGIN
  CREATE USER Cashier FOR LOGIN Cashier;
  EXEC sp_addrolemember N'db_owner', N'Cashier';
END
GO

/* Set up the Barista database */
USE Barista;
GO

IF DATABASE_PRINCIPAL_ID('Barista') IS NULL
BEGIN
  CREATE USER Barista FOR LOGIN Barista;
  EXEC sp_addrolemember N'db_owner', N'Barista';
END
GO

/* Set up the Bakery database */
USE Bakery;
GO

IF DATABASE_PRINCIPAL_ID('Baker') IS NULL
BEGIN
  CREATE USER Baker FOR LOGIN Baker;
  EXEC sp_addrolemember N'db_owner', N'Baker';
END
GO
