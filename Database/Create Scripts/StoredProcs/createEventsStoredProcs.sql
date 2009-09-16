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
	@StartDate					datetime,
	@CategoryID					int,
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
		StartDate,
		CategoryID,
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
		@StartDate,
		@CategoryID,
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
	SELECT UserID, EventName, StartDate, CategoryID, EventAchieved, Deleted,
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
	SELECT EventID, EventName, StartDate, CategoryID, EventAchieved,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
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
	SELECT EventID, EventName, StartDate, CategoryID, EventAchieved,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
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
	SELECT EventID, EventName, StartDate, CategoryID, EventAchieved,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND EventAchieved = 0
	AND StartDate >= @StartDate
	AND StartDate <= @EndDate
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
	SELECT EventID, EventName, StartDate, CategoryID, EventAchieved,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
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
	SELECT EventID, EventName, StartDate, CategoryID, EventAchieved,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
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
	SELECT EventID, EventName, StartDate, CategoryID, EventAchieved,
		EventPicFilename, EventPicThumbnail, EventPicPreview,
		CreatedDate, CreatedByFullName, LastUpdatedDate, LastUpdatedByFullName
	FROM Events
	WHERE Deleted = 0
	AND StartDate >= @StartDate
	AND StartDate <= @EndDate
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
	@StartDate						datetime,
	@CategoryID						int,
	@EventAchieved					bit,
	@LastUpdatedDate				datetime,
	@LastUpdatedByFullName			nvarchar(200)
AS
BEGIN
	UPDATE Events
	SET EventName				= @EventName,
		StartDate				= @StartDate,
		CategoryID				= @CategoryID,
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

PRINT '== Finished createEventsStoredProcs.sql =='
GO
