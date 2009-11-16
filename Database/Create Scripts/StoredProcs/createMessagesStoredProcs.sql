/*===============================================================
// Filename: createMessagesStoredProcs.sql
// Date: 28/09/09
// --------------------------------------------------------------
// Description:
//   This file creates the messages stored procedures
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 28/09/09
// Revision history:
//=============================================================*/

PRINT '== Starting createMessagesStoredProcs.sql =='
GO

/*===============================================================
// Function: spAddMessage
// Description:
//   Add a message to the database
//=============================================================*/
PRINT 'Creating spAddMessage...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddMessage')
	BEGIN
		DROP Procedure spAddMessage
	END
GO

CREATE Procedure spAddMessage
	@EventID				int,
	@UserID					int,
	@PostedByUserID			int,
	@MessageText			nvarchar(max),
	@CreatedDate			datetime,
	@CreatedByFullName		nvarchar(200),
	@LastUpdatedDate		datetime,
	@LastUpdatedByFullName	nvarchar(200),
	@MessageID				int OUTPUT
AS
BEGIN
	INSERT INTO Messages
	(
		EventID,
		UserID,
		PostedByUserID,
		MessageText,
		MessageRead,
		Deleted,
		CreatedDate,
		CreatedByFullName,
		LastUpdatedDate,
		LastUpdatedByFullName
	)
	VALUES
	(
		@EventID,
		@UserID,
		@PostedByUserID,
		@MessageText,
		0,		-- MessageRead
		0,		-- Deleted
		@CreatedDate,
		@CreatedByFullName,
		@LastUpdatedDate,
		@LastUpdatedByFullName
	)
	
	SET @MessageID = @@IDENTITY
END
GO

GRANT EXEC ON spAddMessage TO sedogoUser
GO

/*===============================================================
// Function: spSelectMessageDetails
// Description:
//   Gets event message details
// --------------------------------------------------------------
// Parameters
//	 @MessageID			int
//=============================================================*/
PRINT 'Creating spSelectMessageDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectMessageDetails')
BEGIN
	DROP Procedure spSelectMessageDetails
END
GO

CREATE Procedure spSelectMessageDetails
	@MessageID			int
AS
BEGIN
	SELECT EventID, UserID, PostedByUserID, MessageText, MessageRead, Deleted,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Messages
	WHERE MessageID = @MessageID
END
GO

GRANT EXEC ON spSelectMessageDetails TO sedogoUser
GO

/*===============================================================
// Function: spSelectMessageCountForEvent
// Description:
//   Gets event message count for an event
// --------------------------------------------------------------
// Parameters
//	 @EventID			int
//=============================================================*/
PRINT 'Creating spSelectMessageCountForEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectMessageCountForEvent')
BEGIN
	DROP Procedure spSelectMessageCountForEvent
END
GO

CREATE Procedure spSelectMessageCountForEvent
	@EventID			int
AS
BEGIN
	SELECT COUNT(*)
	FROM Messages
	WHERE EventID = @EventID
	AND Deleted = 0

END
GO

GRANT EXEC ON spSelectMessageCountForEvent TO sedogoUser
GO

/*===============================================================
// Function: spSelectUnreadMessageCountForUser
// Description:
//   Gets unread message count for a user
// --------------------------------------------------------------
// Parameters
//	 @UserID			int
//=============================================================*/
PRINT 'Creating spSelectUnreadMessageCountForUser...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectUnreadMessageCountForUser')
BEGIN
	DROP Procedure spSelectUnreadMessageCountForUser
END
GO

CREATE Procedure spSelectUnreadMessageCountForUser
	@UserID			int
AS
BEGIN
	SELECT COUNT(*)
	FROM Messages
	WHERE UserID = @UserID
	AND Deleted = 0
	AND MessageRead = 0
END
GO

GRANT EXEC ON spSelectUnreadMessageCountForUser TO sedogoUser
GO

/*===============================================================
// Function: spSelectMessageList
// Description:
//   Selects messages
//=============================================================*/
PRINT 'Creating spSelectMessageList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectMessageList')
BEGIN
	DROP Procedure spSelectMessageList
END
GO

CREATE Procedure spSelectMessageList
	@UserID		int
AS
BEGIN
	SELECT MessageID, EventID, PostedByUserID, MessageText, 
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Messages
	WHERE Deleted = 0
	AND UserID = @UserID
	ORDER BY CreatedDate DESC
END
GO

GRANT EXEC ON spSelectMessageList TO sedogoUser
GO

/*===============================================================
// Function: spSelectUnreadMessageList
// Description:
//   Selects messages
//=============================================================*/
PRINT 'Creating spSelectUnreadMessageList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectUnreadMessageList')
BEGIN
	DROP Procedure spSelectUnreadMessageList
END
GO

CREATE Procedure spSelectUnreadMessageList
	@UserID		int
AS
BEGIN
	SELECT M.MessageID, M.EventID, M.PostedByUserID, M.MessageText, 
		M.CreatedDate, M.CreatedByFullName, M.LastUpdatedDate, M.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown,
		U.Birthday, U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview,
		U.ProfileText
	FROM Messages M
	JOIN Events E
	ON M.EventID = E.EventID
	JOIN Users U
	ON U.UserID = E.UserID
	WHERE M.Deleted = 0
	AND M.MessageRead = 0
	AND M.UserID = @UserID
	AND E.Deleted = 0
	ORDER BY M.CreatedDate DESC
END
GO

GRANT EXEC ON spSelectUnreadMessageList TO sedogoUser
GO

/*===============================================================
// Function: spUpdateMessage
// Description:
//   Update message
//=============================================================*/
PRINT 'Creating spUpdateMessage...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateMessage')
BEGIN
	DROP Procedure spUpdateMessage
END
GO

CREATE Procedure spUpdateMessage
	@MessageID					int,
	@MessageText				nvarchar(max),
	@MessageRead				bit,
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE Messages
	SET MessageText				= @MessageText,
		MessageRead				= @MessageRead,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE MessageID = @MessageID
END
GO

GRANT EXEC ON spUpdateMessage TO sedogoUser
GO

/*===============================================================
// Function: spDeleteMessage
// Description:
//   Delete message
//=============================================================*/
PRINT 'Creating spDeleteMessage...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteMessage')
BEGIN
	DROP Procedure spDeleteMessage
END
GO

CREATE Procedure spDeleteMessage
	@MessageID					int,
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE Messages
	SET Deleted					= 1,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE MessageID = @MessageID
END
GO

GRANT EXEC ON spDeleteMessage TO sedogoUser
GO
 
PRINT '== Finished createMessagesStoredProcs.sql =='
GO
