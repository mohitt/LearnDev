
--##Ads
-- Use this Script to execute Intial Data..
Use Ads

Delete from dbo.Ads


DBCC CHECKIDENT ('dbo.Ads', reseed, 1)




--##Users
Delete from Users


DBCC CHECKIDENT ('[dbo].[Users]', reseed, 1)


SET IDENTITY_INSERT [dbo].[Users] ON


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


SET IDENTITY_INSERT [dbo].[Users] OFF


--##Categories
Delete from dbo.Categories


DBCC CHECKIDENT ('dbo.Categories', reseed, 1)


SET IDENTITY_INSERT dbo.Categories ON


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



--##AdTypes
Delete from dbo.AdTypes


DBCC CHECKIDENT ('dbo.AdTypes', reseed, 1)


SET IDENTITY_INSERT dbo.AdTypes ON


INSERT INTO dbo.AdTypes
                  (Id)
VALUES (1)


SET IDENTITY_INSERT dbo.AdTypes OFF


--##Ads
