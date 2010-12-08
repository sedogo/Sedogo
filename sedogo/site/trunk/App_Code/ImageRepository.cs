using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sedogo.DAL;


public class ImageRepository
{
    private readonly SedogoDbEntities _dataContext = new SedogoDbEntities();

    public string GetImagePath(int id, ImageType imageType)
    {
        switch (imageType)
        {
            case ImageType.UserPreview:
            case ImageType.UserThumbnail:
                var user = _dataContext.Users.Where(x => x.UserID == id).FirstOrDefault();
                return user != null ? user.ProfilePicFilename : string.Empty;

            case ImageType.EventPreview:
            case ImageType.EventThumbnail:
                {
                    var _event = _dataContext.Events.Where(x => x.EventID == id).FirstOrDefault();
                    return _event != null ? _event.EventPicFilename : string.Empty;
                }
            case ImageType.EventCommentPreview:
            case ImageType.EventCommentThumbnail:
                {
                    var _event = _dataContext.EventComments.Where(x => x.EventCommentID == id).FirstOrDefault();
                    return _event != null ? _event.EventImageFilename : string.Empty;
                }
            case ImageType.EventPicturePreview:
            case ImageType.EventPictureThumbnail:
                {
                    var _event = _dataContext.EventPictures.Where(x => x.EventPictureID == id).FirstOrDefault();
                    return _event != null ? _event.ImageFilename : string.Empty;
                }
            default:
                return string.Empty;
        }
    }
}