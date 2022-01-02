CREATE PROCEDURE [dbo].[sp_user_AddUser]
	@UserName NVARCHAR(50)
AS
BEGIN
	INSERT INTO dbo.[User] (UserName)
	VALUES (@UserName);
END
