--I.  Left hand column lists

--1) Full-text index 

CREATE FULLTEXT CATALOG SedogoEventsCatalog;
CREATE UNIQUE INDEX UQ_Events ON dbo.[Events](EventId);
CREATE FULLTEXT INDEX ON dbo.[Events]
(
    EventName                       --Full-text index column name 
    Language 2057	--2057 is the LCID for British English
)
KEY INDEX UQ_Events ON SedogoEventsCatalog 	--Unique index
WITH CHANGE_TRACKING AUTO            			--Population type;
GO
--2) SIDEBAR$SelectSimilarEvents

/****** Object:  StoredProcedure [dbo].[SIDEBAR$SelectSimilarEvents]    Script Date: 09/23/2010 04:43:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SIDEBAR$SelectSimilarEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SIDEBAR$SelectSimilarEvents]
GO
/****** Object:  StoredProcedure [dbo].[SIDEBAR$SelectSimilarEvents]    Script Date: 09/23/2010 04:43:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SIDEBAR$SelectSimilarEvents]
	@SearchWord		nvarchar(50),
	@EventId		int
AS
BEGIN
	SELECT TOP 4 *
	FROM [Events]
	WHERE Deleted = 0  AND PrivateEvent = 0 	AND [Events].EventAchieved=0 
	AND FREETEXT(EventName, @SearchWord)  AND EventID != @EventId
	ORDER BY EventName ASC
END
GO
--3) SIDEBAR$SelectOtherEvents

/****** Object:  StoredProcedure [dbo].[SIDEBAR$SelectOtherEvents]    Script Date: 09/23/2010 04:43:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SIDEBAR$SelectOtherEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SIDEBAR$SelectOtherEvents]
GO
/****** Object:  StoredProcedure [dbo].[SIDEBAR$SelectOtherEvents]    Script Date: 09/23/2010 04:43:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SIDEBAR$SelectOtherEvents]
	@EventId			int
AS
BEGIN
	SELECT TOP 4 *
	FROM [Events]
	WHERE Deleted = 0
		AND PrivateEvent = 0  AND [Events].EventAchieved=0 
		AND UserId IN (SELECT UserID FROM [Events] WHERE EventId = @EventId) 
		AND EventID != @EventId
	ORDER BY EventName ASC
END
GO