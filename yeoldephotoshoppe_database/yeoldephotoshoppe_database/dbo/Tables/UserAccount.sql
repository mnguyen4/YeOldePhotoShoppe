CREATE TABLE [dbo].[UserAccount]
(
	[pk] INT IDENTITY(0,1) NOT NULL PRIMARY KEY, 
    [username] NVARCHAR(255) NOT NULL , 
    [password] NVARCHAR(255) NOT NULL, 
    [firstName] NVARCHAR(255) NULL, 
    [lastName] NVARCHAR(255) NULL, 
    [email] NVARCHAR(255) NOT NULL, 
    CONSTRAINT [AK_UserAccount_Username] UNIQUE ([username]) 
)

GO
