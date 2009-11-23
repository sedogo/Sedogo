 /*===============================================================
// Filename: createEventsTables.sql
// Date: 16/08/09
// --------------------------------------------------------------
// Description:
//   This file creates the Events tables
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 16/08/09
// Revision history:
//=============================================================*/

PRINT '== Starting createEventsTables.sql =='

/*===============================================================
// Table: Events
//=============================================================*/

PRINT 'Creating Events...'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Events')
	BEGIN
		DROP Table Events
	END
GO

CREATE TABLE Events
(
	EventID							int					NOT NULL PRIMARY KEY IDENTITY,
	UserID							int					NOT NULL,
	EventName						nvarchar(200)		NOT NULL,
	EventVenue						nvarchar(max)		NULL,
	EventDescription				nvarchar(max)		NULL,
	MustDo							bit					NOT NULL,
	
	DateType						nchar(1)			NOT NULL,
	
	StartDate						datetime		    NULL,
	RangeStartDate					datetime		    NULL,
	RangeEndDate					datetime		    NULL,
	BeforeBirthday					int					NULL,
	
	EventAchieved					bit					NOT NULL,
	Deleted							bit					NOT NULL,
	CategoryID						int					NULL,
	PrivateEvent					bit					NOT NULL,
	CreatedFromEventID				int					NULL,
	TimezoneID						int					NOT NULL,
	
	EventPicFilename				nvarchar(200)		NULL,
	EventPicThumbnail				nvarchar(200)		NULL,
	EventPicPreview					nvarchar(200)		NULL,
	
	CreatedDate						datetime		    NOT NULL,
	CreatedByFullName				nvarchar(200)	    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL,
	LastUpdatedByFullName			nvarchar(200)	    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON Events TO sedogoUser
GO

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_Events_UserID')
    DROP INDEX IX_Events_UserID ON Events
GO

CREATE INDEX IX_Events_UserID
    ON Events ( UserID ); 
GO

/*===============================================================
// Table: TrackedEvents
//=============================================================*/

PRINT 'Creating TrackedEvents...'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'TrackedEvents')
	BEGIN
		DROP Table TrackedEvents
	END
GO

CREATE TABLE TrackedEvents
(
	TrackedEventID					int					NOT NULL PRIMARY KEY IDENTITY,
	EventID							int					NOT NULL,
	UserID							int					NOT NULL,
	JoinPending						bit					NOT NULL,
	ShowOnTimeline					bit					NOT NULL,
	
	CreatedDate						datetime		    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON TrackedEvents TO sedogoUser
GO

/*===============================================================
// Table: EventComments
//=============================================================*/

PRINT 'Creating EventComments...'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'EventComments')
	BEGIN
		DROP Table EventComments
	END
GO

CREATE TABLE EventComments
(
	EventCommentID					int					NOT NULL PRIMARY KEY IDENTITY,
	EventID							int					NOT NULL,
	PostedByUserID					int					NOT NULL,
	
	CommentText						nvarchar(max)		NULL,
	Deleted							bit					NOT NULL,
	
	CreatedDate						datetime		    NOT NULL,
	CreatedByFullName				nvarchar(200)	    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL,
	LastUpdatedByFullName			nvarchar(200)	    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON EventComments TO sedogoUser
GO

/*===============================================================
// Table: Messages
//=============================================================*/

PRINT 'Creating Messages...'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Messages')
	BEGIN
		DROP Table Messages
	END
GO

CREATE TABLE Messages
(
	MessageID						int					NOT NULL PRIMARY KEY IDENTITY,
	EventID							int					NULL,		-- Null incase we add non-event messages
	UserID							int					NOT NULL,
	PostedByUserID					int					NOT NULL,
	
	MessageText						nvarchar(max)		NULL,
	
	MessageRead						bit					NOT NULL,
	Deleted							bit					NOT NULL,
	
	CreatedDate						datetime		    NOT NULL,
	CreatedByFullName				nvarchar(200)	    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL,
	LastUpdatedByFullName			nvarchar(200)	    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON Messages TO sedogoUser
GO

/*===============================================================
// Table: EventInvites
//=============================================================*/

PRINT 'Creating EventInvites...'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'EventInvites')
	BEGIN
		DROP Table EventInvites
	END
GO

CREATE TABLE EventInvites
(
	EventInviteID					int					NOT NULL PRIMARY KEY IDENTITY,
	GUID							nvarchar(50)		NOT NULL,
	EventID							int					NOT NULL,
	
	UserID							int					NULL,
	EmailAddress					nvarchar(200)	    NOT NULL,
	InviteAdditionalText			nvarchar(max)		NULL,
	
	InviteEmailSent					bit					NOT NULL,
	InviteEmailSentEmailAddress		nvarchar(200)	    NULL,
	InviteEmailSentDate				datetime		    NULL,
	InviteAccepted					bit					NOT NULL,
	InviteAcceptedDate				datetime		    NULL,
	InviteDeclined					bit					NOT NULL,
	InviteDeclinedDate				datetime		    NULL,
	
	Deleted							bit					NOT NULL,
	
	CreatedDate						datetime		    NOT NULL,
	CreatedByFullName				nvarchar(200)	    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL,
	LastUpdatedByFullName			nvarchar(200)	    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON EventInvites TO sedogoUser
GO

/*===============================================================
// Table: EventAlerts
//=============================================================*/

PRINT 'Creating EventAlerts...'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'EventAlerts')
	BEGIN
		DROP Table EventAlerts
	END
GO

CREATE TABLE EventAlerts
(
	EventAlertID					int					NOT NULL PRIMARY KEY IDENTITY,
	EventID							int					NOT NULL,
	
	AlertDate						datetime		    NULL,
	AlertText						nvarchar(max)		NULL,
	Completed						bit					NOT NULL,
	Deleted							bit					NOT NULL,
	
	CreatedDate						datetime		    NOT NULL,
	CreatedByFullName				nvarchar(200)	    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL,
	LastUpdatedByFullName			nvarchar(200)	    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON EventAlerts TO sedogoUser
GO

PRINT '== Finished createEventsTables.sql =='
   