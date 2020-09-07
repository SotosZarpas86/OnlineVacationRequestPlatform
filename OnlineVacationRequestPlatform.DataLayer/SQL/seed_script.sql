--ROLES

INSERT INTO [dbo].[Roles] ([Id],[DateAdded],[DateModified],[Name])
VALUES ('70E9F96E-4428-4BC4-88A3-623F49E858F1','2020-09-05','2020-09-05','Employee')
GO

INSERT INTO [dbo].[Roles] ([Id],[DateAdded],[DateModified],[Name])
VALUES('6C15CE8E-1920-41B6-B556-B6A14FD0A794','2020-09-05','2020-09-05','Admin')
GO

--USERS
--Employee
INSERT INTO [dbo].[Users] ([Id],[DateAdded],[DateModified],[FirstName] ,[LastName],[Email],[Password],[SupervisorId],[RoleId])
VALUES('DF958606-FEB3-401C-931D-4FBB9ABA20F4','2020-09-05','2020-09-05','John','Doe','','bfcX++36cvYV3gXb8nuJdw==',null ,'70E9F96E-4428-4BC4-88A3-623F49E858F1')
GO

INSERT INTO [dbo].[Users] ([Id],[DateAdded],[DateModified],[FirstName] ,[LastName],[Email],[Password],[SupervisorId],[RoleId])
VALUES('F2600A2B-02C2-456E-ABA1-5C61ABE6F9CA','2020-09-05','2020-09-05','Jane','Doe','','bfcX++36cvYV3gXb8nuJdw==','DF958606-FEB3-401C-931D-4FBB9ABA20F4' ,'70E9F96E-4428-4BC4-88A3-623F49E858F1')
GO

--Admin
INSERT INTO [dbo].[Users] ([Id],[DateAdded],[DateModified],[FirstName] ,[LastName],[Email],[Password],[SupervisorId],[RoleId])
VALUES('DAC9D3B2-0F4A-419B-9194-E8EDABEF0715','2020-09-05','2020-09-05','Test','Admin','','bfcX++36cvYV3gXb8nuJdw==',null ,'6C15CE8E-1920-41B6-B556-B6A14FD0A794')
GO