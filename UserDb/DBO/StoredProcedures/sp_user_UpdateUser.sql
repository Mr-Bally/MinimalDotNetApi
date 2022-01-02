CREATE PROCEDURE [dbo].[sp_user_UpdateUser]
	@Id INT,
	@UserName NVARCHAR(50)
AS
BEGIN
	UPDATE dbo.[User]
	SET UserName = @UserName
	WHERE Id = @Id;
END
