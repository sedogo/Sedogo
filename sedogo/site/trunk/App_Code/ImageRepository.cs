using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sedogo.DAL;


public class ImageRepository
{
    private readonly SedogoDbEntities _dataContext = new SedogoDbEntities();

    public string GetImagePath(int id)
    {
        var user = _dataContext.Users.Where(x => x.UserID == id).FirstOrDefault();
        return user != null ? user.ProfilePicFilename : string.Empty;
    }
}