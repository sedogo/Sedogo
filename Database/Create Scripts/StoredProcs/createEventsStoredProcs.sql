/*===============================================================
// Filename: createEventsStoredProcs.sql
// Date: 17/08/09
// --------------------------------------------------------------
// Description:
//   This file creates the Events stored procedures
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 17/08/09
// Revision history:
//=============================================================*/

PRINT '== Starting createEventsStoredProcs.sql =='
GO

/*===============================================================
// Function: spAddEvent
// Description:
//   Add an event to the database
//=============================================================*/
PRINT 'Creating spAddEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddEvent')
	BEGIN
		DROP Procedure spAddEvent
	END
GO

CREATE Procedure spAddEvent
	@UserID						int,
	@EventName					nvarchar(200),
	@DateType					nchar(1),
	@StartDate					datetime,
	@RangeStartDate				datetime,
	@RangeEndDate				datetime,
	@BeforeBirthday				int,
	@CategoryID					int,
	@PrivateEvent				bit,
	@CreatedDate				datetime,
	@CreatedByFullName			nvarchar(200),
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200),
	@EventID					int OUTPUT
AS
BEGIN
	INSERT INTO Events
	(
		UserID,
		EventName,
		DateType,
		StartDate,
		RangeStartDate,
		RangeEndDate,
		BeforeBirthday,
		CategoryID,
		PrivateEvent,
		EventAchieved,
		Deleted,
		CreatedDate,
		CreatedByFullName,
		LastUpdatedDate,
		LastUpdatedByFullName
	)
	VALUES
	(
		@UserID,
		@EventName,
		@DateType,
		@StartDate,
		@RangeStartDate,
		@RangeEndDate,
		@BeforeBirthday,
		@CategoryID,
		@PrivateEvent,
		0,		-- EventAchieved
		0,		-- Deleted
		@CreatedDate,
		@CreatedByFullName,
		@LastUpdatedDate,
		@LastUpdatedByFullName
	)
	
	SET @EventID = @@IDENTITY
END
GO

GRANT EXEC ON spAddEvent TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventDetails
// Description:
//   Gets event details
// --------------------------------------------------------------
// Parameters
//	 @EventID			int
//=============================================================*/
PRINT 'Creating spSelectEventDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventDetails')
BEGIN
	DROP Procedure spSelectEventDetails
END
GO

CREATE Procedure spSelectEventDetails
	@EventID			int
AS
BEGIN
	SELECT UserID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, Deleted, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE EventID = @EventID
END
GO

GRANT EXEC ON spSelectEventDetails TO sedogoUser
GO

/*===============================================================
// Function: spSelectFullEventList
// Description:
//   Selects the users event list
//=============================================================*/
PRINT 'Creating spSelectFullEventList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectFullEventList')
BEGIN
	DROP Procedure spSelectFullEventList
END
GO

CREATE Procedure spSelectFullEventList
	@UserID			int
AS
BEGIN
	SELECT EventID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
	AND UserID = @UserID
	ORDER BY StartDate
END
GO

GRANT EXEC ON spSelectFullEventList TO sedogoUser
GO

/*===============================================================
// Function: spSelectFullEventListByCategory
// Description:
//   Selects the users event list
//=============================================================*/
PRINT 'Creating spSelectFullEventListByCategory...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectFullEventListByCategory')
BEGIN
	DROP Procedure spSelectFullEventListByCategory
END
GO

CREATE Procedure spSelectFullEventListByCategory
	@UserID			int
AS
BEGIN
	SELECT EventID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
	AND UserID = @UserID
	ORDER BY CategoryID
END
GO

GRANT EXEC ON spSelectFullEventListByCategory TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventList
// Description:
//   Selects the users event list
//=============================================================*/
PRINT 'Creating spSelectEventList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventList')
BEGIN
	DROP Procedure spSelectEventList
END
GO

CREATE Procedure spSelectEventList
	@UserID			int,
	@StartDate		datetime,
	@EndDate		datetime
AS
BEGIN
	SELECT EventID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
	AND StartDate >= @StartDate
	AND StartDate <= @EndDate
	AND UserID = @UserID
	ORDER BY StartDate
END
GO

GRANT EXEC ON spSelectEventList TO sedogoUser
GO

/*===============================================================
// Function: spSelectFullEventListIncludingAchieved
// Description:
//   Selects the users event list
//=============================================================*/
PRINT 'Creating spSelectFullEventListIncludingAchieved...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectFullEventListIncludingAchieved')
BEGIN
	DROP Procedure spSelectFullEventListIncludingAchieved
END
GO

CREATE Procedure spSelectFullEventListIncludingAchieved
	@UserID			int
AS
BEGIN
	SELECT EventID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND UserID = @UserID
	ORDER BY StartDate
END
GO

GRANT EXEC ON spSelectFullEventListIncludingAchieved TO sedogoUser
GO

/*===============================================================
// Function: spSelectFullEventListIncludingAchievedByCategory
// Description:
//   Selects the users event list
//=============================================================*/
PRINT 'Creating spSelectFullEventListIncludingAchievedByCategory...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectFullEventListIncludingAchievedByCategory')
BEGIN
	DROP Procedure spSelectFullEventListIncludingAchievedByCategory
END
GO

CREATE Procedure spSelectFullEventListIncludingAchievedByCategory
	@UserID			int
AS
BEGIN
	SELECT EventID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND UserID = @UserID
	ORDER BY CategoryID
END
GO

GRANT EXEC ON spSelectFullEventListIncludingAchievedByCategory TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventListIncludingAchieved
// Description:
//   Selects the users event list
//=============================================================*/
PRINT 'Creating spSelectEventListIncludingAchieved...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventListIncludingAchieved')
BEGIN
	DROP Procedure spSelectEventListIncludingAchieved
END
GO

CREATE Procedure spSelectEventListIncludingAchieved
	@UserID			int,
	@StartDate		datetime,
	@EndDate		datetime
AS
BEGIN
	SELECT EventID, EventName, DateType, StartDate, RangeStartDate, RangeEndDate,
		BeforeBirthday, CategoryID, EventAchieved, PrivateEvent,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND StartDate >= @StartDate
	AND StartDate <= @EndDate
	AND UserID = @UserID
	ORDER BY StartDate
END
GO

GRANT EXEC ON spSelectEventListIncludingAchieved TO sedogoUser
GO

/*===============================================================
// Function: spUpdateEvent
// Description:
//   Update event details
//=============================================================*/
PRINT 'Creating spUpdateEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEvent')
BEGIN
	DROP Procedure spUpdateEvent
END
GO

CREATE Procedure spUpdateEvent
	@EventID						int,
	@EventName						nvarchar(200),
	@DateType						nchar(1),
	@StartDate						datetime,
	@RangeStartDate					datetime,
	@RangeEndDate					datetime,
	@BeforeBirthday					int,
	@CategoryID						int,
	@PrivateEvent					bit,
	@EventAchieved					bit,
	@LastUpdatedDate				datetime,
	@LastUpdatedByFullName			nvarchar(200)
AS
BEGIN
	UPDATE Events
	SET EventName				= @EventName,
		DateType				= @DateType,
		StartDate				= @StartDate,
		RangeStartDate			= @RangeStartDate,
		RangeEndDate			= @RangeEndDate,
		BeforeBirthday			= @BeforeBirthday,
		CategoryID				= @CategoryID,
		PrivateEvent			= @PrivateEvent,
		EventAchieved			= @EventAchieved,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventID = @EventID
END
GO

GRANT EXEC ON spUpdateEvent TO sedogoUser
GO

/*===============================================================
// Function: spUpdateEventPics
// Description:
//   Update event details
//=============================================================*/
PRINT 'Creating spUpdateEventPics...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEventPics')
BEGIN
	DROP Procedure spUpdateEventPics
END
GO

CREATE Procedure spUpdateEventPics
	@EventID						int,
	@EventPicFilename				nvarchar(200),
	@EventPicThumbnail				nvarchar(200),
	@EventPicPreview				nvarchar(200),
	@LastUpdatedDate				datetime,
	@LastUpdatedByFullName			nvarchar(200)
AS
BEGIN
	UPDATE Events
	SET EventPicFilename		= @EventPicFilename,
		EventPicThumbnail		= @EventPicThumbnail,
		EventPicPreview			= @EventPicPreview,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventID = @EventID
END
GO

GRANT EXEC ON spUpdateEventPics TO sedogoUser
GO

/*===============================================================
// Function: spDeleteEvent
// Description:
//   Delete event
//=============================================================*/
PRINT 'Creating spDeleteEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteEvent')
BEGIN
	DROP Procedure spDeleteEvent
END
GO

CREATE Procedure spDeleteEvent
	@EventID			int
AS
BEGIN
	UPDATE Events
	SET Deleted = 1
	WHERE EventID = @EventID
END
GO

GRANT EXEC ON spDeleteEvent TO sedogoUser
GO

/*===============================================================
// Function: spAddEventComment
// Description:
//   Add an event comment to the database
//=============================================================*/
PRINT 'Creating spAddEventComment...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddEventComment')
	BEGIN
		DROP Procedure spAddEventComment
	END
GO

CREATE Procedure spAddEventComment
	@EventID				int,
	@PostedByUserID			int,
	@CommentText			nvarchar(max),
	@CreatedDate			datetime,
	@CreatedByFullName		nvarchar(200),
	@LastUpdatedDate		datetime,
	@LastUpdatedByFullName	nvarchar(200),
	@EventCommentID			int OUTPUT
AS
BEGIN
	INSERT INTO EventComments
	(
		EventID,
		PostedByUserID,
		CommentText,
		Deleted,
		CreatedDate,
		CreatedByFullName,
		LastUpdatedDate,
		LastUpdatedByFullName
	)
	VALUES
	(
		@EventID,
		@PostedByUserID,
		@CommentText,
		0,		-- Deleted
		@CreatedDate,
		@CreatedByFullName,
		@LastUpdatedDate,
		@LastUpdatedByFullName
	)
	
	SET @EventCommentID = @@IDENTITY
END
GO

GRANT EXEC ON spAddEventComment TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventCommentDetails
// Description:
//   Gets event comment details
// --------------------------------------------------------------
// Parameters
//	 @EventCommentID			int
//=============================================================*/
PRINT 'Creating spSelectEventCommentDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventCommentDetails')
BEGIN
	DROP Procedure spSelectEventCommentDetails
END
GO

CREATE Procedure spSelectEventCommentDetails
	@EventCommentID			int
AS
BEGIN
	SELECT EventID, PostedByUserID, CommentText, Deleted,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM EventComments
	WHERE EventCommentID = @EventCommentID
END
GO

GRANT EXEC ON spSelectEventCommentDetails TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventCommentsList
// Description:
//   Selects the events comments
//=============================================================*/
PRINT 'Creating spSelectEventCommentsList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventCommentsList')
BEGIN
	DROP Procedure spSelectEventCommentsList
END
GO

CREATE Procedure spSelectEventCommentsList
	@EventID		int
AS
BEGIN
	SELECT C.EventCommentID, C.PostedByUserID, C.CommentText, 
		C.CreatedDate, C.CreatedByFullName, C.LastUpdatedDate, C.LastUpdatedByFullName,
		U.FirstName, U.LastName, U.EmailAddress
	FROM EventComments C
	JOIN Users U
	ON C.PostedByUserID = U.UserID
	WHERE C.Deleted = 0
	AND C.EventID = @EventID
	ORDER BY C.CreatedDate DESC
END
GO

GRANT EXEC ON spSelectEventCommentsList TO sedogoUser
GO

/*===============================================================
// Function: spUpdateEventComment
// Description:
//   Update event comment
//=============================================================*/
PRINT 'Creating spUpdateEventComment...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEventComment')
BEGIN
	DROP Procedure spUpdateEventComment
END
GO

CREATE Procedure spUpdateEventComment
	@EventCommentID				int,
	@CommentText				nvarchar(max),
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE EventComments
	SET CommentText				= @CommentText,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventCommentID = @EventCommentID
END
GO

GRANT EXEC ON spUpdateEventComment TO sedogoUser
GO

/*===============================================================
// Function: spDeleteEventComment
// Description:
//   Delete event comment
//=============================================================*/
PRINT 'Creating spDeleteEventComment...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteEventComment')
BEGIN
	DROP Procedure spDeleteEventComment
END
GO

CREATE Procedure spDeleteEventComment
	@EventCommentID				int,
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE EventComments
	SET Deleted					= 1,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventCommentID = @EventCommentID
END
GO

GRANT EXEC ON spDeleteEventComment TO sedogoUser
GO

/*===============================================================
// Function: spSearchEvents
// Description:
//   Search events
//=============================================================*/
PRINT 'Creating spSearchEvents...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSearchEvents')
BEGIN
	DROP Procedure spSearchEvents
END
GO

CREATE Procedure spSearchEvents
	@UserID			int,
	@SearchText		nvarchar(1000)
AS
BEGIN
	SELECT E.EventID, E.UserID, E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate,
		E.BeforeBirthday, E.CategoryID, E.EventAchieved, E.PrivateEvent,
		E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		E.CreatedDate, E.CreatedByFullName, E.LastUpdatedDate, E.LastUpdatedByFullName,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown, U.ProfilePicThumbnail
	FROM Events E
	JOIN Users U
	ON E.UserID = U.UserID
	WHERE E.Deleted = 0
	AND E.EventAchieved = 0
	AND E.PrivateEvent = 0
	AND E.UserID <> @UserID			-- Do not return events belonging to the searching user
	AND ( (@SearchText = '') 
	 OR (UPPER(E.EventName) LIKE '%'+UPPER(@SearchText)+'%') ) 
	ORDER BY E.StartDate
END
GO

GRANT EXEC ON spSearchEvents TO sedogoUser
GO

/*===============================================================
// Function: spAddTrackedEvent
// Description:
//   Add a tracked event 
//=============================================================*/
PRINT 'Creating spAddTrackedEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddTrackedEvent')
	BEGIN
		DROP Procedure spAddTrackedEvent
	END
GO

CREATE Procedure spAddTrackedEvent
	@EventID				int,
	@UserID					int,
	@CreatedDate			datetime,
	@LastUpdatedDate		datetime,
	@TrackedEventID			int OUTPUT
AS
BEGIN
	INSERT INTO TrackedEvents
	(
		EventID,
		UserID,
		CreatedDate,
		LastUpdatedDate
	)
	VALUES
	(
		@EventID,
		@UserID,
		@CreatedDate,
		@LastUpdatedDate
	)
	
	SET @TrackedEventID = @@IDENTITY
END
GO

GRANT EXEC ON spAddTrackedEvent TO sedogoUser
GO

/*===============================================================
// Function: spSelectTrackedEventDetails
// Description:
//   Gets tracked event details
// --------------------------------------------------------------
// Parameters
//	 @TrackedEventID			int
//=============================================================*/
PRINT 'Creating spSelectTrackedEventDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectTrackedEventDetails')
BEGIN
	DROP Procedure spSelectTrackedEventDetails
END
GO

CREATE Procedure spSelectTrackedEventDetails
	@TrackedEventID			int
AS
BEGIN
	SELECT EventID, UserID,
		CreatedDate, LastUpdatedDate
	FROM TrackedEvents
	WHERE TrackedEventID = @TrackedEventID
END
GO

GRANT EXEC ON spSelectTrackedEventDetails TO sedogoUser
GO

/*===============================================================
// Function: spSelectTrackedEventListByUserID
// Description:
//   Selects the tracked events list
//=============================================================*/
PRINT 'Creating spSelectTrackedEventListByUserID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectTrackedEventListByUserID')
BEGIN
	DROP Procedure spSelectTrackedEventListByUserID
END
GO

CREATE Procedure spSelectTrackedEventListByUserID
	@UserID		int
AS
BEGIN
	SELECT T.TrackedEventID, T.EventID, T.UserID, T.CreatedDate, T.LastUpdatedDate,
		E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.EventAchieved, E.CategoryID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview
	FROM TrackedEvents T
	JOIN Events E
	ON T.EventID = E.EventID
	WHERE T.UserID = @UserID
	AND E.Deleted = 0
	ORDER BY E.EventName DESC
	
END
GO

GRANT EXEC ON spSelectTrackedEventListByUserID TO sedogoUser
GO

/*===============================================================
// Function: spSelectTrackingUsersByEventID
// Description:
//   Selects the list of users tracking an event
//=============================================================*/
PRINT 'Creating spSelectTrackingUsersByEventID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectTrackingUsersByEventID')
BEGIN
	DROP Procedure spSelectTrackingUsersByEventID
END
GO

CREATE Procedure spSelectTrackingUsersByEventID
	@EventID		int
AS
BEGIN
	SELECT T.TrackedEventID, T.EventID, T.UserID, T.CreatedDate, T.LastUpdatedDate,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown, U.Birthday,
		U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview
	FROM TrackedEvents T
	JOIN Users U
	ON T.UserID = U.UserID
	WHERE T.EventID = @EventID
	AND U.Deleted = 0
	ORDER BY U.LastName DESC
	
END
GO

GRANT EXEC ON spSelectTrackingUsersByEventID TO sedogoUser
GO

/*===============================================================
// Function: spDeleteTrackedEvent
// Description:
//   Delete tracked event
//=============================================================*/
PRINT 'Creating spDeleteTrackedEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteTrackedEvent')
BEGIN
	DROP Procedure spDeleteTrackedEvent
END
GO

CREATE Procedure spDeleteTrackedEvent
	@TrackedEventID				int
AS
BEGIN
	DELETE TrackedEvents
	WHERE TrackedEventID = @TrackedEventID

END
GO

GRANT EXEC ON spDeleteTrackedEvent TO sedogoUser
GO

/*===============================================================
// Function: spSelectTrackedEventID
// Description:
//   
// --------------------------------------------------------------
// Parameters
//	 @TrackedEventID			int
//=============================================================*/
PRINT 'Creating spSelectTrackedEventID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectTrackedEventID')
BEGIN
	DROP Procedure spSelectTrackedEventID
END
GO

CREATE Procedure spSelectTrackedEventID
	@EventID			int,
	@UserID				int
AS
BEGIN
	SELECT TrackedEventID
	FROM TrackedEvents
	WHERE EventID = @EventID
	AND UserID = @UserID
END
GO

GRANT EXEC ON spSelectTrackedEventID TO sedogoUser
GO

PRINT '== Finished createEventsStoredProcs.sql =='
GO
