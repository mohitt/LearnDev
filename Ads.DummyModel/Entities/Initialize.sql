
--##Ads
Delete from dbo.Ads
GO

DBCC CHECKIDENT ('dbo.Ads', reseed, 1)
GO



--##Users
Delete from Users
GO

DBCC CHECKIDENT ('[dbo].[Users]', reseed, 1)
GO

SET IDENTITY_INSERT [dbo].[Users] ON
GO

INSERT INTO [dbo].[Users]
           ([Id]
           ,[DisplayName]
           ,[RealName]
           ,[Email]
           ,[OpenId]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[LastUpdatedOn]
           ,[LastUpdatedBy])
     VALUES
           (
			1
           ,'Admin'
           ,'Administrator'
           ,'mohit@thakral.in'
           ,'http://d.devtalk.in/'
           ,getdate()
           ,1
           ,getdate()
           ,1)
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO

--##Categories
Delete from dbo.Categories
GO

DBCC CHECKIDENT ('dbo.Categories', reseed, 1)
GO

SET IDENTITY_INSERT dbo.Categories ON
GO

INSERT INTO dbo.Categories
                  (Id, DisplayValue ,[CreatedOn],[CreatedBy] ,[LastUpdatedOn],[LastUpdatedBy])
Select 1, N'Rooms',getdate(),1,getdate(),1
union
Select 2, N'Automobile',getdate(),1,getdate(),1
union
Select 3, N'Buy & Sell',getdate(),1,getdate(),1
union
Select 4, N'Tutions',getdate(),1,getdate(),1
union
Select 5, N'Immigration',getdate(),1,getdate(),1


SET IDENTITY_INSERT dbo.Categories OFF
GO


--##AdTypes
Delete from dbo.AdTypes
GO

DBCC CHECKIDENT ('dbo.AdTypes', reseed, 1)
GO

SET IDENTITY_INSERT dbo.AdTypes ON
GO

INSERT INTO dbo.AdTypes
                  (Id)
VALUES (1)
GO

SET IDENTITY_INSERT dbo.AdTypes OFF
GO

--##Ads

