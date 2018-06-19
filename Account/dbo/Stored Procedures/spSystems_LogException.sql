CREATE PROCEDURE [dbo].[spSystems_LogException]
	@ExceptionID int out, 
    @ExceptionSource VARCHAR(MAX), 
    @AccountID INT, 
    @LoggedTime DATETIME, 
    @ExceptionError VARCHAR(MAX), 
    @ReproductionData VARCHAR(MAX) 
AS
	Select @ExceptionID = Max(ExceptionID) +1 From Systems_ExceptionLog
	IF(@ExceptionID is null)
	BEGIN
		Select @ExceptionID = 1
	END
	Insert Into Systems_ExceptionLog(ExceptionID, ExceptionSource, AccountID, LoggedTime, ExceptionError, ReproductionData) Values (@ExceptionID, @ExceptionSource, @AccountID, @LoggedTime, @ExceptionError, @ReproductionData)
RETURN 0