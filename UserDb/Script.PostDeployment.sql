IF NOT EXISTS (SELECT 1 FROM dbo.[User])
BEGIN
	INSERT INTO dbo.[User] (UserName)
	VALUES ('Admin');
END