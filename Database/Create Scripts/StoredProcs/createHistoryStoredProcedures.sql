/*===============================================================
// Filename: createHistoryStoredProcedures.sql
// Date: 27/10/09
// --------------------------------------------------------------
// Description:
//   This file creates the hisotry stored procedures
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 27/10/09
// Revision history:
//=============================================================*/

PRINT '== Starting createHistoryStoredProcedures.sql =='
GO
 
/*===============================================================
// Function: spAddSearchHistory
// Description:
//   Add search history
//=============================================================*/
PRINT 'Creating spAddSearchHistory...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddSearchHistory')
	BEGIN
		DROP Procedure spAddSearchHistory
	END
GO

CREATE Procedure spAddSearchHistory
	@SearchDate					datetime,
	@UserID						int,
	@SearchText					nvarchar(200),
	@SearchHits					int,
	@SearchHistoryID			int OUTPUT
AS
BEGIN
	INSERT INTO SearchHistory
	(
		SearchDate,
		UserID,
		SearchText,
		SearchHits
	)
	VALUES
	(
		@SearchDate,
		@UserID,
		@SearchText,
		@SearchHits
	)
	
	SET @SearchHistoryID = @@IDENTITY
END
GO

GRANT EXEC ON spAddSearchHistory TO sedogoUser
GO

/*===============================================================
// Function: spSelectSearchHistoryDetails
// Description:
//   Gets search history details
// --------------------------------------------------------------
// Parameters
//	 @SearchHistoryID		int
//=============================================================*/
PRINT 'Creating spSelectSearchHistoryDetails...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectSearchHistoryDetails')
BEGIN
	DROP Procedure spSelectSearchHistoryDetails
END
GO

CREATE Procedure spSelectSearchHistoryDetails
	@SearchHistoryID		int
AS
BEGIN
	SELECT SearchDate, UserID, SearchText, SearchHits
	FROM SearchHistory
	WHERE SearchHistoryID = @SearchHistoryID
END
GO

GRANT EXEC ON spSelectSearchHistoryDetails TO sedogoUser
GO
 
/*===============================================================
// Function: spSelectSearchHistoryList
// Description:
//   Gets search history list
// --------------------------------------------------------------
// Parameters
//	 @SearchHistoryID		int
//=============================================================*/
PRINT 'Creating spSelectSearchHistoryList...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectSearchHistoryList')
BEGIN
	DROP Procedure spSelectSearchHistoryList
END
GO

CREATE Procedure spSelectSearchHistoryList
	@SearchHistoryID		int
AS
BEGIN
	SELECT SearchHistoryID, SearchDate, UserID, SearchText, SearchHits
	FROM SearchHistory
	WHERE SearchHistoryID = @SearchHistoryID
END
GO

GRANT EXEC ON spSelectSearchHistoryList TO sedogoUser
GO

/*===============================================================
// Function: spSelectSearchHistoryTop5
// Description:
//   Gets search history list
//=============================================================*/
PRINT 'Creating spSelectSearchHistoryTop5...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectSearchHistoryTop5')
BEGIN
	DROP Procedure spSelectSearchHistoryTop5
END
GO

CREATE Procedure spSelectSearchHistoryTop5
AS
BEGIN
	SELECT TOP 5 SearchHistoryID, SearchDate, UserID, SearchText, SearchHits
	FROM SearchHistory
	ORDER BY SearchDate DESC
END
GO

GRANT EXEC ON spSelectSearchHistoryTop5 TO sedogoUser
GO

/*===============================================================
// Function: spSelectSearchHistoryPopularTop5
// Description:
//   Gets search history list
//=============================================================*/
PRINT 'Creating spSelectSearchHistoryPopularTop5...'
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spSelectSearchHistoryPopularTop5')
BEGIN
	DROP Procedure spSelectSearchHistoryPopularTop5
END
GO

CREATE Procedure spSelectSearchHistoryPopularTop5
AS
BEGIN
	SELECT SearchText, COUNT(SearchHistoryID) AS SearchCount
	FROM SearchHistory
	GROUP BY SearchText
	ORDER BY SearchCount DESC
	
END
GO

GRANT EXEC ON spSelectSearchHistoryPopularTop5 TO sedogoUser
GO

PRINT '== Finished createHistoryStoredProcedures.sql =='
GO
 