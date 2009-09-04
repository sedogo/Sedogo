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
	StartDate						datetime		    NOT NULL,
	EndDate							datetime		    NOT NULL,
	Deleted							bit					NOT NULL,
	
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
	
	CreatedDate						datetime		    NOT NULL,
	LastUpdatedDate					datetime		    NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON TrackedEvents TO sedogoUser
GO

PRINT '== Finished createEventsTables.sql =='
   