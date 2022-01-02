CREATE PROCEDURE [dbo].[sp_user_DeleteUser]
	@Id INT
AS
BEGIN
	DELETE FROM dbo.[User] WHERE Id = @Id;
END