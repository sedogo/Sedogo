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
	@TimezoneID					int,
	@PrivateEvent				bit,
	@CreatedFromEventID			int,
	@EventDescription			nvarchar(max),
	@EventVenue					nvarchar(max),
	@MustDo						bit,
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
		TimezoneID,
		PrivateEvent,
		CreatedFromEventID,
		EventDescription,
		EventVenue,
		MustDo,
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
		@TimezoneID,
		@PrivateEvent,
		@CreatedFromEventID,
		@EventDescription,
		@EventVenue,
		@MustDo,
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, Deleted, 
		PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
	AND UserID = @UserID
	
	UNION 
	
	SELECT E.EventID, E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate,
		E.BeforeBirthday, E.CategoryID, E.TimezoneID, E.EventAchieved, E.PrivateEvent, E.CreatedFromEventID,
		E.EventDescription, E.EventVenue, E.MustDo,
		E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		E.CreatedDate, E.CreatedByFullName, E.LastUpdatedDate, E.LastUpdatedByFullName
	FROM Events E
	JOIN TrackedEvents T
	ON E.EventID = T.EventID
	WHERE E.Deleted = 0
	AND E.EventAchieved = 0
	AND T.UserID = @UserID
	AND T.ShowOnTimeline = 1
	AND T.JoinPending = 0 
	
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND UserID = @UserID
	
	UNION 
	
	SELECT E.EventID, E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate,
		E.BeforeBirthday, E.CategoryID, E.TimezoneID, E.EventAchieved, E.PrivateEvent, E.CreatedFromEventID,
		E.EventDescription, E.EventVenue, E.MustDo,
		E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		E.CreatedDate, E.CreatedByFullName, E.LastUpdatedDate, E.LastUpdatedByFullName
	FROM Events E
	JOIN TrackedEvents T
	ON E.EventID = T.EventID
	WHERE E.Deleted = 0
	AND E.EventAchieved = 0
	AND T.UserID = @UserID
	AND T.ShowOnTimeline = 1
	AND T.JoinPending = 0
	
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
		BeforeBirthday, CategoryID, TimezoneID, EventAchieved, PrivateEvent, CreatedFromEventID,
		EventDescription, EventVenue, MustDo,
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
	@TimezoneID						int,
	@PrivateEvent					bit,
	@CreatedFromEventID				int,
	@EventAchieved					bit,
	@EventDescription				nvarchar(max),
	@EventVenue						nvarchar(max),
	@MustDo							bit,
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
		TimezoneID				= @TimezoneID,
		PrivateEvent			= @PrivateEvent,
		CreatedFromEventID		= @CreatedFromEventID,
		EventAchieved			= @EventAchieved,
		EventDescription		= @EventDescription,
		EventVenue				= @EventVenue,
		MustDo					= @MustDo,
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
		E.BeforeBirthday, E.CategoryID, E.TimezoneID, E.EventAchieved, E.PrivateEvent, E.CreatedFromEventID,
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
	 OR (UPPER(E.EventName) LIKE '%'+UPPER(@SearchText)+'%')
	 OR (UPPER(U.FirstName) + ' ' + UPPER(U.LastName) LIKE '%'+UPPER(@SearchText)+'%') ) 
	ORDER BY E.StartDate
END
GO

GRANT EXEC ON spSearchEvents TO sedogoUser
GO

/*===============================================================
// Function: spSearchEventsAdvanced
// Description:
//   Search events
//=============================================================*/
PRINT 'Creating spSearchEventsAdvanced...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSearchEventsAdvanced')
BEGIN
	DROP Procedure spSearchEventsAdvanced
END
GO

CREATE Procedure spSearchEventsAdvanced
	@UserID			int,
	@EventName		nvarchar(1000),
	@EventVenue		nvarchar(1000),
	@OwnerName		nvarchar(1000)
AS
BEGIN
	SELECT E.EventID, E.UserID, E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate,
		E.BeforeBirthday, E.CategoryID, E.TimezoneID, E.EventAchieved, E.PrivateEvent, E.CreatedFromEventID,
		E.EventVenue,
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
	AND ( (UPPER(E.EventName) LIKE '%'+UPPER(@EventName)+'%')
	 AND (UPPER(ISNULL(E.EventVenue,'')) LIKE '%'+UPPER(@EventVenue)+'%')
	 AND (UPPER(U.FirstName) + ' ' + UPPER(U.LastName) LIKE '%'+UPPER(@OwnerName)+'%') ) 
	ORDER BY E.StartDate
END
GO

GRANT EXEC ON spSearchEventsAdvanced TO sedogoUser
GO

/*===============================================================
// Function: spSelectHomePageEvents
// Description:
//   Search events
//=============================================================*/
PRINT 'Creating spSelectHomePageEvents...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectHomePageEvents')
BEGIN
	DROP Procedure spSelectHomePageEvents
END
GO

CREATE Procedure spSelectHomePageEvents
AS
BEGIN
	SELECT TOP 10 E.EventID, E.UserID, E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate,
		E.BeforeBirthday, E.CategoryID, E.TimezoneID, E.EventAchieved, E.PrivateEvent, E.CreatedFromEventID,
		E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		E.CreatedDate, E.CreatedByFullName, E.LastUpdatedDate, E.LastUpdatedByFullName,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown, U.ProfilePicThumbnail
	FROM Events E
	JOIN Users U
	ON E.UserID = U.UserID
	WHERE E.Deleted = 0
	AND E.EventAchieved = 0
	AND E.PrivateEvent = 0
	--AND ( (@SearchText = '') 
	-- OR (UPPER(E.EventName) LIKE '%'+UPPER(@SearchText)+'%')
	-- OR (UPPER(U.FirstName) + ' ' + UPPER(U.LastName) LIKE '%'+UPPER(@SearchText)+'%') ) 
	ORDER BY E.StartDate DESC
END
GO

GRANT EXEC ON spSelectHomePageEvents TO sedogoUser
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
	@ShowOnTimeline			bit,
	@JoinPending			bit,
	@CreatedDate			datetime,
	@LastUpdatedDate		datetime,
	@TrackedEventID			int OUTPUT
AS
BEGIN
	INSERT INTO TrackedEvents
	(
		EventID,
		UserID,
		ShowOnTimeline,
		JoinPending,
		CreatedDate,
		LastUpdatedDate
	)
	VALUES
	(
		@EventID,
		@UserID,
		@ShowOnTimeline,
		@JoinPending,
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
	SELECT EventID, UserID, ShowOnTimeline, JoinPending,
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
	SELECT T.TrackedEventID, T.EventID, T.UserID, T.ShowOnTimeline, 
		T.JoinPending, T.CreatedDate, T.LastUpdatedDate,
		E.EventName, E.DateType, E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.EventAchieved, E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.FirstName, U.LastName, U.EmailAddress
	FROM TrackedEvents T
	JOIN Events E
	ON T.EventID = E.EventID
	JOIN Users U
	ON U.UserID = E.UserID
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
	SELECT T.TrackedEventID, T.EventID, T.UserID, T.ShowOnTimeline, 
		T.JoinPending, T.CreatedDate, T.LastUpdatedDate,
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
// Function: spSelectTrackingUserCountByEventID
// Description:
//   Selects the count of users tracking an event
//=============================================================*/
PRINT 'Creating spSelectTrackingUserCountByEventID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectTrackingUserCountByEventID')
BEGIN
	DROP Procedure spSelectTrackingUserCountByEventID
END
GO

CREATE Procedure spSelectTrackingUserCountByEventID
	@EventID		int
AS
BEGIN
	SELECT COUNT(*)
	FROM TrackedEvents T
	JOIN Users U
	ON T.UserID = U.UserID
	WHERE T.EventID = @EventID
	AND U.Deleted = 0
	
END
GO

GRANT EXEC ON spSelectTrackingUserCountByEventID TO sedogoUser
GO

/*===============================================================
// Function: spUpdateTrackedEvent
// Description:
//   Delete tracked event
//=============================================================*/
PRINT 'Creating spUpdateTrackedEvent...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateTrackedEvent')
BEGIN
	DROP Procedure spUpdateTrackedEvent
END
GO

CREATE Procedure spUpdateTrackedEvent
	@TrackedEventID				int,
	@ShowOnTimeline				bit,
	@JoinPending				bit
	@LastUpdatedDate			datetime
AS
BEGIN
	UPDATE TrackedEvents
	SET ShowOnTimeline		= @ShowOnTimeline,
		JoinPending			= @JoinPending,
		LastUpdatedDate		= @LastUpdatedDate
	WHERE TrackedEventID = @TrackedEventID

END
GO

GRANT EXEC ON spUpdateTrackedEvent TO sedogoUser
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

/*===============================================================
// Function: spSelectTrackedEventCountByUserID
// Description:
//   Selects the tracked events for a user
//=============================================================*/
PRINT 'Creating spSelectTrackedEventCountByUserID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectTrackedEventCountByUserID')
BEGIN
	DROP Procedure spSelectTrackedEventCountByUserID
END
GO

CREATE Procedure spSelectTrackedEventCountByUserID
	@UserID		int
AS
BEGIN
	SELECT COUNT(*)
	FROM TrackedEvents T
	JOIN Events E
	ON T.EventID = E.EventID
	WHERE T.UserID = @UserID
	AND E.Deleted = 0
	
END
GO

GRANT EXEC ON spSelectTrackedEventCountByUserID TO sedogoUser
GO

/*===============================================================
// Function: spAddEventInvite
// Description:
//   Add an event invite to the database
//=============================================================*/
PRINT 'Creating spAddEventInvite...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddEventInvite')
	BEGIN
		DROP Procedure spAddEventInvite
	END
GO

CREATE Procedure spAddEventInvite
	@EventID						int,
	@GUID							nvarchar(50),
	@UserID							int,
	@EmailAddress					nvarchar(200),
	@InviteAdditionalText			nvarchar(max),
	@InviteEmailSent				bit,
	@InviteEmailSentEmailAddress	nvarchar(200),
	@InviteEmailSentDate			datetime,
	@CreatedDate					datetime,
	@CreatedByFullName				nvarchar(200),
	@LastUpdatedDate				datetime,
	@LastUpdatedByFullName			nvarchar(200),
	@EventInviteID					int OUTPUT
AS
BEGIN
	INSERT INTO EventInvites
	(
		EventID,
		GUID,
		UserID,
		EmailAddress,
		InviteAdditionalText,
		InviteEmailSent,
		InviteEmailSentEmailAddress,
		InviteEmailSentDate,
		Deleted,
		InviteAccepted,
		InviteDeclined,
		CreatedDate,
		CreatedByFullName,
		LastUpdatedDate,
		LastUpdatedByFullName
	)
	VALUES
	(
		@EventID,
		@GUID,
		@UserID,
		@EmailAddress,
		@InviteAdditionalText,
		@InviteEmailSent,
		@InviteEmailSentEmailAddress,
		@InviteEmailSentDate,
		0,		-- Deleted
		0,		-- InviteAccepted
		0,		-- InviteDeclined
		@CreatedDate,
		@CreatedByFullName,
		@LastUpdatedDate,
		@LastUpdatedByFullName
	)
	
	SET @EventInviteID = @@IDENTITY
END
GO

GRANT EXEC ON spAddEventInvite TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventInviteDetails
// Description:
//   Gets event invite details
// --------------------------------------------------------------
// Parameters
//	 @EventInviteID			int
//=============================================================*/
PRINT 'Creating spSelectEventInviteDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventInviteDetails')
BEGIN
	DROP Procedure spSelectEventInviteDetails
END
GO

CREATE Procedure spSelectEventInviteDetails
	@EventInviteID			int
AS
BEGIN
	SELECT EventID, GUID, UserID, EmailAddress, InviteAdditionalText, Deleted,
		InviteEmailSent, InviteEmailSentEmailAddress, InviteEmailSentDate,
		InviteAccepted, InviteAcceptedDate, InviteDeclined, InviteDeclinedDate,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM EventInvites
	WHERE EventInviteID = @EventInviteID
END
GO

GRANT EXEC ON spSelectEventInviteDetails TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventInvitesList
// Description:
//   Selects the events invites
//=============================================================*/
PRINT 'Creating spSelectEventInvitesList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventInvitesList')
BEGIN
	DROP Procedure spSelectEventInvitesList
END
GO

CREATE Procedure spSelectEventInvitesList
	@EventID		int
AS
BEGIN
	SELECT EventInviteID, GUID, UserID, EmailAddress, InviteAdditionalText, 
		InviteEmailSent, InviteEmailSentEmailAddress, InviteEmailSentDate,
		InviteAccepted, InviteAcceptedDate, InviteDeclined, InviteDeclinedDate,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM EventInvites
	WHERE Deleted = 0
	AND EventID = @EventID
	ORDER BY EmailAddress
END
GO

GRANT EXEC ON spSelectEventInvitesList TO sedogoUser
GO

/*===============================================================
// Function: spUpdateEventInvite
// Description:
//   Update event invite
//=============================================================*/
PRINT 'Creating spUpdateEventInvite...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEventInvite')
BEGIN
	DROP Procedure spUpdateEventInvite
END
GO

CREATE Procedure spUpdateEventInvite
	@EventInviteID					int,
	@UserID							int,
	@EmailAddress					nvarchar(200),
	@InviteAdditionalText			nvarchar(max),
	@InviteEmailSent				bit,
	@InviteEmailSentEmailAddress	nvarchar(200),
	@InviteEmailSentDate			datetime,
	@InviteAccepted					bit,
	@InviteAcceptedDate				datetime,
	@InviteDeclined					bit,
	@InviteDeclinedDate				datetime,
	@LastUpdatedDate				datetime,
	@LastUpdatedByFullName			nvarchar(200)
AS
BEGIN
	UPDATE EventInvites
	SET EmailAddress					= @EmailAddress,
		UserID							= @UserID,
		InviteAdditionalText			= @InviteAdditionalText,
		InviteEmailSent					= @InviteEmailSent,
		InviteEmailSentEmailAddress		= @InviteEmailSentEmailAddress,
		InviteEmailSentDate				= @InviteEmailSentDate,
		InviteAccepted					= @InviteAccepted,
		InviteAcceptedDate				= @InviteAcceptedDate,
		InviteDeclined					= @InviteDeclined,
		InviteDeclinedDate				= @InviteDeclinedDate,
		LastUpdatedDate					= @LastUpdatedDate,
		LastUpdatedByFullName			= @LastUpdatedByFullName
	WHERE EventInviteID = @EventInviteID
END
GO

GRANT EXEC ON spUpdateEventInvite TO sedogoUser
GO

/*===============================================================
// Function: spDeleteEventInvite
// Description:
//   Delete event invite
//=============================================================*/
PRINT 'Creating spDeleteEventInvite...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteEventInvite')
BEGIN
	DROP Procedure spDeleteEventInvite
END
GO

CREATE Procedure spDeleteEventInvite
	@EventInviteID				int,
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE EventInvites
	SET Deleted					= 1,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventInviteID = @EventInviteID
END
GO

GRANT EXEC ON spDeleteEventInvite TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventInviteCountByEventID
// Description:
//   Selects the tracked events for a user
//=============================================================*/
PRINT 'Creating spSelectEventInviteCountByEventID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventInviteCountByEventID')
BEGIN
	DROP Procedure spSelectEventInviteCountByEventID
END
GO

CREATE Procedure spSelectEventInviteCountByEventID
	@EventID		int
AS
BEGIN
	SELECT COUNT(*)
	FROM EventInvites I
	JOIN Events E
	ON I.EventID = E.EventID
	WHERE I.EventID = @EventID
	AND E.Deleted = 0
	AND I.Deleted = 0
	
END
GO

GRANT EXEC ON spSelectEventInviteCountByEventID TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventInviteCountByEventIDAndEmailAddress
// Description:
//   Used to check if a particular email address has already
//	 been invited to a specific event
//=============================================================*/
PRINT 'Creating spSelectEventInviteCountByEventIDAndEmailAddress...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventInviteCountByEventIDAndEmailAddress')
BEGIN
	DROP Procedure spSelectEventInviteCountByEventIDAndEmailAddress
END
GO

CREATE Procedure spSelectEventInviteCountByEventIDAndEmailAddress
	@EventID						int,
	@InviteEmailSentEmailAddress	nvarchar(200)
AS
BEGIN
	SELECT COUNT(*)
	FROM EventInvites
	WHERE EventID = @EventID
	AND Deleted = 0
	AND EmailAddress = @InviteEmailSentEmailAddress
	
END
GO

GRANT EXEC ON spSelectEventInviteCountByEventIDAndEmailAddress TO sedogoUser
GO

/*===============================================================
// Function: spSelectPendingInviteCountForUser
// Description:
//=============================================================*/
PRINT 'Creating spSelectPendingInviteCountForUser...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectPendingInviteCountForUser')
BEGIN
	DROP Procedure spSelectPendingInviteCountForUser
END
GO

CREATE Procedure spSelectPendingInviteCountForUser
	@UserID						int
AS
BEGIN
	SELECT COUNT(*)
	FROM EventInvites
	WHERE UserID = @UserID
	AND Deleted = 0
	AND InviteAccepted = 0
	AND InviteDeclined = 0
	
END
GO

GRANT EXEC ON spSelectPendingInviteCountForUser TO sedogoUser
GO

/*===============================================================
// Function: spSelectPendingInviteListForUser
// Description:
//   Selects the users pending invites
//=============================================================*/
PRINT 'Creating spSelectPendingInviteListForUser...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectPendingInviteListForUser')
BEGIN
	DROP Procedure spSelectPendingInviteListForUser
END
GO

CREATE Procedure spSelectPendingInviteListForUser
	@UserID		int
AS
BEGIN
	SELECT I.EventInviteID, I.GUID, I.EventID, I.EmailAddress, I.InviteAdditionalText, 
		I.InviteEmailSent, I.InviteEmailSentEmailAddress, I.InviteEmailSentDate,
		I.InviteAccepted, I.InviteAcceptedDate, I.InviteDeclined, I.InviteDeclinedDate,
		I.CreatedDate, I.CreatedByFullName, I.LastUpdatedDate, I.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview,
		U.EmailAddress, U.FirstName, U.LastName, U.Gender, U.HomeTown,
		U.Birthday, U.ProfilePicFilename, U.ProfilePicThumbnail, U.ProfilePicPreview,
		U.ProfileText
	FROM EventInvites I
	JOIN Events E
	ON I.EventID = E.EventID
	JOIN Users U
	ON U.UserID = E.UserID
	WHERE I.Deleted = 0
	AND E.Deleted = 0
	AND I.UserID = @UserID
	AND I.InviteAccepted = 0
	AND I.InviteDeclined = 0
	ORDER BY I.CreatedDate
	
END
GO

GRANT EXEC ON spSelectPendingInviteListForUser TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventInviteIDFromGUID
// Description:
//=============================================================*/
PRINT 'Creating spSelectEventInviteIDFromGUID...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventInviteIDFromGUID')
BEGIN
	DROP Procedure spSelectEventInviteIDFromGUID
END
GO

CREATE Procedure spSelectEventInviteIDFromGUID
	@GUID		nvarchar(50)
AS
BEGIN
	SELECT EventInviteID
	FROM EventInvites
	WHERE GUID = @GUID
	AND Deleted = 0
	
END
GO

GRANT EXEC ON spSelectEventInviteIDFromGUID TO sedogoUser
GO

/*===============================================================
// Function: spAddEventAlert
// Description:
//   Add an event alert to the database
//=============================================================*/
PRINT 'Creating spAddEventAlert...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddEventAlert')
	BEGIN
		DROP Procedure spAddEventAlert
	END
GO

CREATE Procedure spAddEventAlert
	@EventID				int,
	@AlertDate				datetime,
	@AlertText				nvarchar(max),
	@CreatedDate			datetime,
	@CreatedByFullName		nvarchar(200),
	@LastUpdatedDate		datetime,
	@LastUpdatedByFullName	nvarchar(200),
	@EventAlertID			int OUTPUT
AS
BEGIN
	INSERT INTO EventAlerts
	(
		EventID,
		AlertDate,
		AlertText,
		Completed,
		Deleted,
		CreatedDate,
		CreatedByFullName,
		LastUpdatedDate,
		LastUpdatedByFullName
	)
	VALUES
	(
		@EventID,
		@AlertDate,
		@AlertText,
		0,		-- Completed
		0,		-- Deleted
		@CreatedDate,
		@CreatedByFullName,
		@LastUpdatedDate,
		@LastUpdatedByFullName
	)
	
	SET @EventAlertID = @@IDENTITY
END
GO

GRANT EXEC ON spAddEventAlert TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventAlertDetails
// Description:
//   Gets event alert details
//=============================================================*/
PRINT 'Creating spSelectEventAlertDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventAlertDetails')
BEGIN
	DROP Procedure spSelectEventAlertDetails
END
GO

CREATE Procedure spSelectEventAlertDetails
	@EventAlertID			int
AS
BEGIN
	SELECT EventID, AlertDate, AlertText, Completed, Deleted,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM EventAlerts
	WHERE EventAlertID = @EventAlertID
END
GO

GRANT EXEC ON spSelectEventAlertDetails TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventAlertList
// Description:
//   Selects the events alerts
//=============================================================*/
PRINT 'Creating spSelectEventAlertList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventAlertList')
BEGIN
	DROP Procedure spSelectEventAlertList
END
GO

CREATE Procedure spSelectEventAlertList
	@EventID		int
AS
BEGIN
	SELECT EventAlertID, AlertDate, AlertText, Completed,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM EventAlerts
	WHERE Deleted = 0
	AND EventID = @EventID
	ORDER BY CreatedDate DESC
END
GO

GRANT EXEC ON spSelectEventAlertList TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventAlertListPending
// Description:
//   Selects the events alerts
//=============================================================*/
PRINT 'Creating spSelectEventAlertListPending...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventAlertListPending')
BEGIN
	DROP Procedure spSelectEventAlertListPending
END
GO

CREATE Procedure spSelectEventAlertListPending
	@EventID		int
AS
BEGIN
	SELECT EventAlertID, AlertDate, AlertText, 
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM EventAlerts
	WHERE Completed = 0
	AND Deleted = 0
	AND EventID = @EventID
	ORDER BY CreatedDate DESC
END
GO

GRANT EXEC ON spSelectEventAlertListPending TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventAlertListPendingByUser
// Description:
//   Selects the events alerts
//=============================================================*/
PRINT 'Creating spSelectEventAlertListPendingByUser...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventAlertListPendingByUser')
BEGIN
	DROP Procedure spSelectEventAlertListPendingByUser
END
GO

CREATE Procedure spSelectEventAlertListPendingByUser
	@UserID		int
AS
BEGIN
	SELECT A.EventAlertID, A.AlertDate, A.AlertText, 
		A.CreatedDate, A.CreatedByFullName, A.LastUpdatedDate, A.LastUpdatedByFullName,
		E.EventName, E.EventDescription, E.EventVenue, E.MustDo, E.DateType,
		E.StartDate, E.RangeStartDate, E.RangeEndDate, E.BeforeBirthday,
		E.CategoryID, E.TimezoneID, E.EventPicFilename, E.EventPicThumbnail, E.EventPicPreview
	FROM EventAlerts A
	JOIN Events E
	ON A.EventID = E.EventID
	WHERE A.Completed = 0
	AND A.Deleted = 0
	AND E.Deleted = 0
	AND E.UserID = @UserID
	ORDER BY A.CreatedDate DESC
END
GO

GRANT EXEC ON spSelectEventAlertListPendingByUser TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventAlertCountPending
// Description:
//   Selects the number of events alerts
//=============================================================*/
PRINT 'Creating spSelectEventAlertCountPending...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventAlertCountPending')
BEGIN
	DROP Procedure spSelectEventAlertCountPending
END
GO

CREATE Procedure spSelectEventAlertCountPending
	@EventID		int
AS
BEGIN
	SELECT COUNT(*)
	FROM EventAlerts
	WHERE Completed = 0
	AND Deleted = 0
	AND EventID = @EventID
END
GO

GRANT EXEC ON spSelectEventAlertCountPending TO sedogoUser
GO

/*===============================================================
// Function: spSelectEventAlertCountPendingByUser
// Description:
//   Selects the number of events alerts
//=============================================================*/
PRINT 'Creating spSelectEventAlertCountPendingByUser...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectEventAlertCountPendingByUser')
BEGIN
	DROP Procedure spSelectEventAlertCountPendingByUser
END
GO

CREATE Procedure spSelectEventAlertCountPendingByUser
	@UserID		int
AS
BEGIN
	SELECT COUNT(*)
	FROM EventAlerts A
	JOIN Events E
	ON A.EventID = E.EventID
	WHERE A.Completed = 0
	AND A.Deleted = 0
	AND E.UserID = @UserID
END
GO

GRANT EXEC ON spSelectEventAlertCountPendingByUser TO sedogoUser
GO

/*===============================================================
// Function: spUpdateEventAlert
// Description:
//   Update event alert
//=============================================================*/
PRINT 'Creating spUpdateEventAlert...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEventAlert')
BEGIN
	DROP Procedure spUpdateEventAlert
END
GO

CREATE Procedure spUpdateEventAlert
	@EventAlertID				int,
	@AlertDate					datetime,
	@AlertText					nvarchar(max),
	@Completed					bit,
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE EventAlerts
	SET AlertText				= @AlertText,
		AlertDate				= @AlertDate,
		Completed				= @Completed,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventAlertID = @EventAlertID
END
GO

GRANT EXEC ON spUpdateEventAlert TO sedogoUser
GO

/*===============================================================
// Function: spDeleteEventAlert
// Description:
//   Delete event alert
//=============================================================*/
PRINT 'Creating spDeleteEventAlert...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteEventAlert')
BEGIN
	DROP Procedure spDeleteEventAlert
END
GO

CREATE Procedure spDeleteEventAlert
	@EventAlertID				int,
	@LastUpdatedDate			datetime,
	@LastUpdatedByFullName		nvarchar(200)
AS
BEGIN
	UPDATE EventAlerts
	SET Deleted					= 1,
		LastUpdatedDate			= @LastUpdatedDate,
		LastUpdatedByFullName	= @LastUpdatedByFullName
	WHERE EventAlertID = @EventAlertID
END
GO

GRANT EXEC ON spDeleteEventAlert TO sedogoUser
GO




/*===============================================================
// Function: spSelectLatestEvents
// Description:
//   Gets most recent events
//=============================================================*/
PRINT 'Creating spSelectLatestEvents...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectLatestEvents')
BEGIN
	DROP Procedure spSelectLatestEvents
END
GO

CREATE Procedure spSelectLatestEvents
	@LoggedInUserID	int
AS
BEGIN
	SELECT TOP 5 EventID, UserID, EventName, EventVenue, DateType,
		StartDate, RangeStartDate, RangeEndDate, BeforeBirthday,
		EventAchieved, CategoryID, TimezoneID, EventPicFilename, EventPicThumbnail, EventPicPreview
	FROM Events
	WHERE UserID <> @LoggedInUserID
	AND Deleted = 0
	AND PrivateEvent = 0
	ORDER BY CreatedDate DESC
	
END
GO

GRANT EXEC ON spSelectLatestEvents TO sedogoUser
GO

PRINT '== Finished createEventsStoredProcs.sql =='
GO
