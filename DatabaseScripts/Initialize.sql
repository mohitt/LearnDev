
Delete from Users

DBCC CHECKIDENT ('[Ads2].[dbo].[Users]', reseed, 1)

SET IDENTITY_INSERT [Ads2].[dbo].[Users] ON

INSERT INTO [Ads2].[dbo].[Users]
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

SET IDENTITY_INSERT [Ads2].[dbo].[Users] OFF
