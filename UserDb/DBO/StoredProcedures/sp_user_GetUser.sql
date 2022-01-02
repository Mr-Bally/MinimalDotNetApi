CREATE PROCEDURE [dbo].[sp_user_GetUser]
	@Id INT
AS
BEGIN
	SELECT * FROM dbo.[User] WHERE Id = @Id;
END
