CREATE TABLE [dbo].[Token]
(
	[pk] INT IDENTITY(0,1) NOT NULL PRIMARY KEY, 
    [hash] NVARCHAR(255) NOT NULL, 
    [userFk] INT NOT NULL, 
    [accessTime] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Token_UserAccount] FOREIGN KEY ([userFk]) REFERENCES [UserAccount]([pk]), 
    CONSTRAINT [AK_Token_Hash] UNIQUE ([hash])
)

GO
