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
	FROM Messages M
	LEFT OUTER JOIN Events E
	ON M.EventID = E.EventID
	LEFT OUTER JOIN Users U
	ON U.UserID = E.UserID
	WHERE M.Deleted = 0
	AND M.MessageRead = 0
	AND M.UserID = @UserID
	AND ISNULL(E.Deleted,0) = 0
END
GO

GRANT EXEC ON spSelectUnreadMessageCountForUser TO sedogoUser
GO

/*===============================================================
// Function: spSelectSentMessageCountForUser
// Description:
//   Gets sent message count for a user
// --------------------------------------------------------------
// Parameters
//	 @UserID			int
//=============================================================*/
PRINT 'Creating spSelectSentMessageCountForUser...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectSentMessageCountForUser')
BEGIN
	DROP Procedure spSelectSentMessageCountForUser
END
GO

CREATE Procedure spSelectSentMessageCountForUser
	@UserID			int
AS
BEGIN
	SELECT COUNT(*)
	FROM Messages M
	LEFT OUTER JOIN Events E
	ON M.EventID = E.EventID
	WHERE M.Deleted = 0
	AND M.PostedByUserID = @UserID
	AND ISNULL(E.Deleted,0) = 0
	AND M.MessageRead = 0
END
GO

GRANT EXEC ON spSelectSentMessageCountForUser TO sedogoUser
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
	SELECT M.MessageID, M.EventID, M.PostedByUserID, M.MessageText, M.MessageRead,
		M.CreatedDate, M.CreatedByFullName, M.LastUpdatedDate, M.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType, E.UserID,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown,
		U.Birthday, U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview,
		U.ProfileText
	FROM Messages M
	LEFT OUTER JOIN Events E
	ON M.EventID = E.EventID
	JOIN Users U
	ON U.UserID = E.UserID
	WHERE M.Deleted = 0
	AND M.UserID = @UserID
	AND ISNULL(E.Deleted,0) = 0
	ORDER BY M.CreatedDate DESC
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
	SELECT M.MessageID, M.EventID, M.PostedByUserID, M.MessageText, M.MessageRead,
		M.CreatedDate, M.CreatedByFullName, M.LastUpdatedDate, M.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType, E.UserID,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown,
		U.Birthday, U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview,
		U.ProfileText
	FROM Messages M
	LEFT OUTER JOIN Events E
	ON M.EventID = E.EventID
	LEFT OUTER JOIN Users U
	ON U.UserID = M.PostedByUserID
	WHERE M.Deleted = 0
	AND M.MessageRead = 0
	AND M.UserID = @UserID
	AND ISNULL(E.Deleted,0) = 0
	ORDER BY M.CreatedDate DESC
END
GO

GRANT EXEC ON spSelectUnreadMessageList TO sedogoUser
GO

/*===============================================================
// Function: spSelectReadMessageList
// Description:
//   Selects messages
//=============================================================*/
PRINT 'Creating spSelectReadMessageList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectReadMessageList')
BEGIN
	DROP Procedure spSelectReadMessageList
END
GO

CREATE Procedure spSelectReadMessageList
	@UserID		int
AS
BEGIN
	SELECT M.MessageID, M.EventID, M.PostedByUserID, M.MessageText, M.MessageRead,
		M.CreatedDate, M.CreatedByFullName, M.LastUpdatedDate, M.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType, E.UserID,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown,
		U.Birthday, U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview,
		U.ProfileText
	FROM Messages M
	LEFT OUTER JOIN Events E
	ON M.EventID = E.EventID
	LEFT OUTER JOIN Users U
	ON U.UserID = M.PostedByUserID
	WHERE M.Deleted = 0
	AND M.MessageRead = 1
	AND M.UserID = @UserID
	AND ISNULL(E.Deleted,0) = 0
	ORDER BY M.CreatedDate DESC
END
GO

GRANT EXEC ON spSelectReadMessageList TO sedogoUser
GO

/*===============================================================
// Function: spSelectSentMessageList
// Description:
//   Selects messages
//=============================================================*/
PRINT 'Creating spSelectSentMessageList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectSentMessageList')
BEGIN
	DROP Procedure spSelectSentMessageList
END
GO

CREATE Procedure spSelectSentMessageList
	@UserID		int
AS
BEGIN
	SELECT M.MessageID, M.EventID, M.PostedByUserID, M.MessageText, M.MessageRead,
		M.CreatedDate, M.CreatedByFullName, M.LastUpdatedDate, M.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType, E.UserID,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown,
		U.Birthday, U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview,
		U.ProfileText
	FROM Messages M
	LEFT OUTER JOIN Events E
	ON M.EventID = E.EventID
	JOIN Users U
	ON U.UserID = M.UserID
	WHERE M.Deleted = 0
	AND M.PostedByUserID = @UserID
	AND ISNULL(E.Deleted,0) = 0
	ORDER BY M.CreatedDate DESC
END
GO

GRANT EXEC ON spSelectSentMessageList TO sedogoUser
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
