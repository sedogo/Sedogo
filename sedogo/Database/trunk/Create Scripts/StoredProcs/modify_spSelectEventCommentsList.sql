USE [sedogo4]
GO
/****** Object:  StoredProcedure [dbo].[spSelectEventCommentsList]    Script Date: 11/29/2010 21:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER Procedure [dbo].[spSelectEventCommentsList]
	@EventID		int
AS
BEGIN
	SELECT C.EventCommentID, C.PostedByUserID, C.CommentText, 
		C.EventImageFilename, C.EventImagePreview, C.EventVideoFilename, C.EventVideoLink, C.EventLink,
		C.CreatedDate, C.CreatedByFullName, C.LastUpdatedDate, C.LastUpdatedByFullName,
		U.FirstName, U.LastName, U.EmailAddress, U.ProfilePicThumbnail, U.ProfilePicPreview, U.AvatarNumber,
		U.Gender, U.UserID, U.GUID
	FROM EventComments C
	JOIN Users U
	ON C.PostedByUserID = U.UserID
	WHERE C.Deleted = 0
	AND C.EventID = @EventID
	ORDER BY C.CreatedDate DESC
END
