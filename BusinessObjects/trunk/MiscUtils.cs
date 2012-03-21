//===============================================================
// Filename: MiscUtils.cs
// Date: 05/09/09
// --------------------------------------------------------------
// Description:
//   Misc utils class
// --------------------------------------------------------------
// Dependencies:
//   None
// --------------------------------------------------------------
// Original author: PRD 05/09/09
// Revision history:
//===============================================================

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;

namespace Sedogo.BusinessObjects
{
    //===============================================================
    // Class: MiscUtils
    //===============================================================
    public class MiscUtils
    {
        //===============================================================
        // Function: MiscUtils (Constructor)
        //===============================================================
        public MiscUtils()
        {
        }

        //===============================================================
        // Function: GetUniqueFileName
        // Description:
        // Notes: No longer adds (1) etc on the end as this breaks
        //        files which are added into css style elements
        //        instead, add _1 onto the end
        //===============================================================
        public static string GetUniqueFileName(string filename)
        {
            filename = filename.Replace(" ", "_");

            string newPath = "";
            if (File.Exists(filename))
            {
                int fileNumber = 1;
                do
                {
                    int pos = filename.LastIndexOf(".");
                    if (pos > 0)
                    {
                        string tempFilename = filename.Substring(0, pos);
                        string tempExtension = filename.Substring(pos + 1, (filename.Length - pos - 1));
                        newPath = tempFilename + "_" + fileNumber.ToString() + "." + tempExtension;
                    }
                    else
                    {
                        string tempFilename = filename;
                        newPath = tempFilename + "_" + fileNumber.ToString();
                    }
                    fileNumber++;
                }
                while (File.Exists(newPath));
            }
            else
            {
                // No duplicate filename
                newPath = filename;
            }
            return newPath;
        }

        //===============================================================
        // Function: CreatePreviews
        // Description:
        //===============================================================
        public static int CreatePreviews(string filename, int userID)
        {
            int returnStatus = -1;

            GlobalData gd = new GlobalData("");
            int thumbnailSize = gd.GetIntegerValue("ThumbnailSize");
            int previewSize = gd.GetIntegerValue("PreviewSize");
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder");
            string fileStoreFolderTemp = fileStoreFolder + "\\temp";
            string fileStoreFolderProfilePics = fileStoreFolder + "\\profilePics";
            string thumbnailFileName = "";
            string previewFileName = "";

            int thumbnailStatus = GenerateThumbnail(fileStoreFolderTemp,
                filename, thumbnailSize, thumbnailSize, previewSize, previewSize,
                out thumbnailFileName, out previewFileName);

            if (thumbnailStatus > 0)
            {
                // Move the thumbnails to the /profilePics folder and update the user
                string destFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, filename));
                string destThumbnailFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, thumbnailFileName));
                string destPreviewFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, previewFileName));

                File.Move(Path.Combine(fileStoreFolderTemp, filename),
                    destFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, thumbnailFileName),
                    destThumbnailFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, previewFileName),
                    destPreviewFilename);

                SedogoUser user = new SedogoUser("", userID);
                user.profilePicFilename = filename;
                user.profilePicPreview = previewFileName;
                user.profilePicThumbnail = thumbnailFileName;
                user.UpdateUserProfilePic();

                returnStatus = 0;
            }

            return returnStatus;
        }

        //===============================================================
        // Function: CreatePreviews
        // Description:
        //===============================================================
        public static int CreatePreviews(string fullFileName)
        {
            int returnStatus = -1;
            string filename = Path.GetFileName(fullFileName);
            string filePath = Path.GetFullPath(fullFileName);
            GlobalData gd = new GlobalData("");
            int thumbnailSize = gd.GetIntegerValue("ThumbnailSize");
            int previewSize = gd.GetIntegerValue("PreviewSize");
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder");
            string fileStoreFolderTemp = fileStoreFolder + "\\temp";
            string fileStoreFolderProfilePics = fileStoreFolder + "\\profilePics";
            string thumbnailFileName = "";
            string previewFileName = "";
 //           File.Copy(fullFileName, Path.Combine(fileStoreFolderTemp, filename));
            int thumbnailStatus = GenerateThumbnail(filePath,
                filename, thumbnailSize, thumbnailSize, previewSize, previewSize,
                out thumbnailFileName, out previewFileName);

            if (thumbnailStatus > 0)
            {
                // Move the thumbnails to the /profilePics folder and update the user
                string destFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, filename));
                string destThumbnailFilename = MiscUtils.GetUniqueFileName(Path.Combine(filePath, thumbnailFileName));
                string destPreviewFilename = MiscUtils.GetUniqueFileName(Path.Combine(filePath, previewFileName));

                //File.Move(Path.Combine(fileStoreFolderTemp, filename), destFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, thumbnailFileName),
                    destThumbnailFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, previewFileName),
                    destPreviewFilename);
                returnStatus = 0;
            }

            return returnStatus;
        }

        //===============================================================
        // Function: CreateEventPicPreviews
        // Description:
        //===============================================================
        public static int CreateEventPicPreviews(string filename, int eventID)
        {
            int returnStatus = -1;

            GlobalData gd = new GlobalData("");
            int thumbnailSize = gd.GetIntegerValue("ThumbnailSize");
            int previewSize = gd.GetIntegerValue("PreviewSize");
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder");
            string fileStoreFolderTemp = fileStoreFolder + "\\temp";
            string fileStoreFolderProfilePics = fileStoreFolder + "\\eventPics";
            string thumbnailFileName = "";
            string previewFileName = "";

            int thumbnailStatus = GenerateThumbnail(fileStoreFolderTemp,
                filename, thumbnailSize, thumbnailSize, previewSize, previewSize,
                out thumbnailFileName, out previewFileName);

            if (thumbnailStatus > 0)
            {
                // Move the thumbnails to the /profilePics folder and update the user
                string destFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, filename));
                string destThumbnailFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, thumbnailFileName));
                string destPreviewFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, previewFileName));

                File.Move(Path.Combine(fileStoreFolderTemp, filename),
                    destFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, thumbnailFileName),
                    destThumbnailFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, previewFileName),
                    destPreviewFilename);

                SedogoEvent sedogoEvent = new SedogoEvent("", eventID);
                sedogoEvent.eventPicFilename = Path.GetFileName(destFilename);
                sedogoEvent.eventPicPreview = Path.GetFileName(destPreviewFilename);
                sedogoEvent.eventPicThumbnail = Path.GetFileName(destThumbnailFilename);
                sedogoEvent.UpdateEventPic();

                returnStatus = 0;
            }

            return returnStatus;
        }
        //===============================================================
        // Function: CreateEventPicPreviews
        // Description:
        //===============================================================
        public static int CreateEventPicPreviews(string filename)
        {
            int returnStatus = -1;

            GlobalData gd = new GlobalData("");
            int thumbnailSize = gd.GetIntegerValue("ThumbnailSize");
            int previewSize = gd.GetIntegerValue("PreviewSize");
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder");
            string fileStoreFolderTemp = fileStoreFolder + "\\temp";
            string fileStoreFolderProfilePics = fileStoreFolder + "\\eventPics";
            string thumbnailFileName = "";
            string previewFileName = "";

            int thumbnailStatus = GenerateThumbnail(fileStoreFolderTemp,
                filename, thumbnailSize, thumbnailSize, previewSize, previewSize,
                out thumbnailFileName, out previewFileName);

            if (thumbnailStatus > 0)
            {
                // Move the thumbnails to the /profilePics folder and update the user
                string destFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, filename));
                string destThumbnailFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, thumbnailFileName));
                string destPreviewFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, previewFileName));

                File.Move(Path.Combine(fileStoreFolderTemp, filename),
                    destFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, thumbnailFileName),
                    destThumbnailFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, previewFileName),
                    destPreviewFilename);
                returnStatus = 0;
            }

            return returnStatus;
        }

        //===============================================================
        // Function: CreateEventCommentImagePreviews
        // Description:
        //===============================================================
        public static int CreateEventCommentImagePreviews(string filename,
            out string savedFileName, out string thumbnailFileName, out string previewFileName)
        {
            int returnStatus = -1;

            GlobalData gd = new GlobalData("");
            int thumbnailSize = gd.GetIntegerValue("ThumbnailSize");
            int previewSize = 500;  // gd.GetIntegerValue("PreviewSize");
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder");
            string fileStoreFolderTemp = fileStoreFolder + "\\temp";
            string fileStoreFolderProfilePics = fileStoreFolder + "\\eventPics";
            savedFileName = "";
            thumbnailFileName = "";
            previewFileName = "";

            // Just for the goal comments, if the image is smaller than 500, leave it at its
            // original size
            System.Drawing.Image sourceImagePreview = System.Drawing.Image.FromFile(fileStoreFolderTemp + @"\" + filename);
            if (sourceImagePreview.Width < previewSize)
            {
                previewSize = sourceImagePreview.Width;
            }
            sourceImagePreview.Dispose();

            int thumbnailStatus = GenerateThumbnail(fileStoreFolderTemp,
                filename, thumbnailSize, thumbnailSize, previewSize, previewSize,
                out thumbnailFileName, out previewFileName);

            if (thumbnailStatus > 0)
            {
                // Move the thumbnails to the /profilePics folder and update the user
                string destFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, filename));
                string destThumbnailFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, thumbnailFileName));
                string destPreviewFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, previewFileName));

                File.Move(Path.Combine(fileStoreFolderTemp, filename),
                    destFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, thumbnailFileName),
                    destThumbnailFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, previewFileName),
                    destPreviewFilename);

                savedFileName = destFilename;
                thumbnailFileName = destThumbnailFilename;
                previewFileName = destPreviewFilename;

                returnStatus = 0;
            }

            return returnStatus;
        }

        
        //===============================================================
        // Function: CreateGoalPicPreviews
        // Description:
        //===============================================================
        public static int CreateGoalPicPreviews(string filename, int eventID, string loggedInContactName,
            int postedByUserID, string caption)
        {
            int returnStatus = -1;

            GlobalData gd = new GlobalData("");
            int thumbnailSize =  gd.GetIntegerValue("ThumbnailSize");//100;    
            int previewSize =  gd.GetIntegerValue("PreviewSize");//500;  
            string fileStoreFolder = gd.GetStringValue("FileStoreFolder");
            string fileStoreFolderTemp = fileStoreFolder + "\\temp";
            string fileStoreFolderProfilePics = fileStoreFolder + "\\eventPics";

            string thumbnailFileName = "";
            string previewFileName = "";
            int thumbnailStatus = GenerateThumbnail(fileStoreFolderTemp,
                filename, thumbnailSize, thumbnailSize, previewSize, previewSize,
                out thumbnailFileName, out previewFileName);

            if (thumbnailStatus > 0)
            {
                // Move the thumbnails to the /profilePics folder and update the user
                string destFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, filename));
                string destThumbnailFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, thumbnailFileName));
                string destPreviewFilename = MiscUtils.GetUniqueFileName(Path.Combine(fileStoreFolderProfilePics, previewFileName));

                File.Move(Path.Combine(fileStoreFolderTemp, filename),
                    destFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, thumbnailFileName),
                    destThumbnailFilename);
                File.Move(Path.Combine(fileStoreFolderTemp, previewFileName),
                    destPreviewFilename);

                SedogoEventPicture pic = new SedogoEventPicture(loggedInContactName);
                pic.eventID = eventID;
                pic.postedByUserID = postedByUserID;
                pic.eventImageFilename = Path.GetFileName(destFilename);
                pic.eventImagePreview = Path.GetFileName(destPreviewFilename);
                pic.eventImageThumbnail = Path.GetFileName(destThumbnailFilename);
                pic.caption = caption;
                pic.Add();

                returnStatus = pic.eventPictureID;
            }

            return returnStatus;
        }

        //===============================================================
        // Function: GenerateThumbnail
        //===============================================================
        private static int GenerateThumbnail(string fileStoreFolder, string filename,
            int thumbnailHeight, int thumbnailWidth,
            int previewHeight, int previewWidth,
            out string thumbnailFilename, out string previewFilename)
        {
            int returnStatus = -1;
            string ripMethod = "";
            thumbnailFilename = "";
            previewFilename = "";

            ErrorLog errorLog = new ErrorLog();

            try
            {
                ImageFormat imageFmt = ImageFormat.Jpeg; ;
                thumbnailFilename = "";
                previewFilename = "";

                if (filename.ToLower().IndexOf(".gif") > 0)
                {
                    imageFmt = ImageFormat.Gif;
                    ripMethod = "Image";
                }
                if (filename.ToLower().IndexOf(".jpg") > 0)
                {
                    imageFmt = ImageFormat.Jpeg;
                    ripMethod = "Image";
                }
                if (filename.ToLower().IndexOf(".jpeg") > 0)
                {
                    imageFmt = ImageFormat.Jpeg;
                    ripMethod = "Image";
                }
                if (filename.ToLower().IndexOf(".bmp") > 0)
                {
                    imageFmt = ImageFormat.Bmp;
                    ripMethod = "Image";
                }
                if (filename.ToLower().IndexOf(".png") > 0)
                {
                    imageFmt = ImageFormat.Png;
                    ripMethod = "Image";
                }

                if (ripMethod == "Image")
                {
                    // Create thumbnail
                    System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(fileStoreFolder + @"\" + filename);

                    // Correct the sizes taking into account the aspect ratio of the image
                    double pictureAspectRatio = (double)sourceImage.Width / (double)sourceImage.Height;
                    //if (pictureAspectRatio < 1)
                    //{
                    //    // Portrait orientation
                    //    thumbnailWidth = (int)(thumbnailWidth * pictureAspectRatio);
                    //}
                    //else
                    //{
                    //    // Landscape orientation
                        thumbnailHeight = (int)(thumbnailHeight / pictureAspectRatio);
                    //}
                    System.Drawing.Image imageThumbnail = new Bitmap(thumbnailWidth, thumbnailHeight, sourceImage.PixelFormat);
                    Graphics oGraphic = Graphics.FromImage(imageThumbnail);
                    oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    System.Drawing.Rectangle oRectangle = new System.Drawing.Rectangle(-1, -1, thumbnailWidth + 1, thumbnailHeight + 1);
                    //Rectangle oRectangle = new Rectangle(-1, -1, thumbnailWidth + 2, thumbnailHeight + 2);
                    //Rectangle oRectangle = new Rectangle(0, 0, thumbnailWidth, thumbnailHeight);
                    oGraphic.DrawImage(sourceImage, oRectangle);
                    thumbnailFilename = Path.GetFileNameWithoutExtension(filename) 
                        + "_T" + Path.GetExtension(filename);
                    imageThumbnail.Save(fileStoreFolder + @"\" + thumbnailFilename, imageFmt);
                    sourceImage.Dispose();

                    // Create preview
                    System.Drawing.Image sourceImagePreview = System.Drawing.Image.FromFile(fileStoreFolder + @"\" + filename);
                    //if (pictureAspectRatio < 1)
                    //{
                    //    // Portrait orientation
                    //    previewWidth = (int)(previewWidth * pictureAspectRatio);
                    //}
                    //else
                    //{
                    //    // Landscape orientation
                        previewHeight = (int)(previewHeight / pictureAspectRatio);
                    //}
                    System.Drawing.Image imageThumbnailPreview = new Bitmap(previewWidth, previewHeight, sourceImagePreview.PixelFormat);
                    Graphics oGraphicPreview = Graphics.FromImage(imageThumbnailPreview);
                    oGraphicPreview.CompositingQuality = CompositingQuality.HighQuality;
                    oGraphicPreview.SmoothingMode = SmoothingMode.HighQuality;
                    oGraphicPreview.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    System.Drawing.Rectangle oRectanglePreview = new System.Drawing.Rectangle(-1, -1, previewWidth + 2, previewHeight + 2);
                    //Rectangle oRectanglePreview = new Rectangle(0, 0, previewWidth, previewHeight);
                    oGraphicPreview.DrawImage(sourceImagePreview, oRectanglePreview);
                    previewFilename = Path.GetFileNameWithoutExtension(filename)
                        + "_P" + Path.GetExtension(filename);
                    imageThumbnailPreview.Save(fileStoreFolder + @"\" + previewFilename, imageFmt);
                    sourceImagePreview.Dispose();

                    returnStatus = 1;   // OK
                }
            }
            catch (Exception ex)
            {
                errorLog.WriteLog("MiscUtils", "GenerateThumbnail", ex.Message, logMessageLevel.errorMessage);
            }
            return returnStatus;
        }

        //===============================================================
        // Function: GetUniqueFileName
        // Description:
        //===============================================================
        public static void GetDateStringStartDate(SedogoUser user, string dateType, 
            DateTime rangeStartDate, DateTime rangeEndDate, int beforeBirthday,
            ref string dateString, ref DateTime startDate)
        {
            if (dateType == "D")
            {
                // Event occurs on a specific date
                dateString = startDate.ToString("ddd d MMMM yyyy");
            }
            if (dateType == "R")
            {
                // Event occurs in a date range - use the start date
                dateString = rangeStartDate.ToString("ddd d MMMM yyyy") + " to " + rangeEndDate.ToString("ddd d MMMM yyyy");

                startDate = rangeStartDate;
            }
            if (dateType == "A")
            {
                // Event occurs before birthday
                string dateSuffix = "";
                switch (beforeBirthday)
                {
                    case 1: case 21: case 31: case 41: case 51: case 61: case 71: case 81: case 91: case 101:
                        dateSuffix = "st";
                        break;
                    case 2: case 22: case 32: case 42: case 52: case 62: case 72: case 82: case 92: case 102:
                        dateSuffix = "nd";
                        break;
                    case 3: case 23: case 33: case 43: case 53: case 63: case 73: case 83: case 93: case 103:
                        dateSuffix = "rd";
                        break;
                    case 4: case 5: case 6: case 7: case 8: case 9: case 10:
                    case 11: case 12: case 13: case 14: case 15: case 16: case 17: case 18: case 19: case 20:
                    case 24: case 25: case 26: case 27: case 28: case 29: case 30:
                    case 34: case 35: case 36: case 37: case 38: case 39: case 40:
                    case 44: case 45: case 46: case 47: case 48: case 49: case 50:
                    case 54: case 55: case 56: case 57: case 58: case 59: case 60:
                    case 64: case 65: case 66: case 67: case 68: case 69: case 70:
                    case 74: case 75: case 76: case 77: case 78: case 79: case 80:
                    case 84: case 85: case 86: case 87: case 88: case 89: case 90:
                    case 94: case 95: case 96: case 97: case 98: case 99: case 100:
                    case 104: case 105: case 106: case 107: case 108: case 109: case 110:
                        dateSuffix = "th";
                        break;
                    default:
                        break;
                }
                dateString = "Before " + beforeBirthday.ToString() + dateSuffix + " birthday";

                //timelineStartDate = DateTime.Now;
                if (user.birthday > DateTime.MinValue)
                {
                    DateTime birthdayEndDate = user.birthday.AddYears(beforeBirthday);

                    TimeSpan ts = birthdayEndDate - DateTime.Now;   // timelineStartDate.AddYears(beforeBirthday);
                    if (ts.Days < 0)
                    {
                        // Birthday was in the past
                        //timelineStartDate = DateTime.Now;
                        //timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up

                        // Set start date so event is correctly placed below
                        startDate = DateTime.Now.AddDays(ts.Days);
                    }
                    else if (ts.Days >= 0 && ts.Days < 28)
                    {
                        // Birthday is within 28 days - extend the timeline a bit
                        //timelineEndDate = timelineStartDate.AddDays(28);        // Add 28 days so it shows up

                        //startDate = timelineStartDate;
                    }
                    else
                    {
                        //startDate = timelineStartDate;
                    }
                }
            }
        }

        //===============================================================
        // Function: IsValidEmailAddress
        // Description:
        //===============================================================
        public static Boolean IsValidEmailAddress(string emailAddress)
        {
            if (emailAddress == null)
            {
                return false;
            }
            else
            {
                return true;
                /*
                 * There is a problem with this as it excludes underscores - not sure what else it excludes as well
                Regex.IsMatch(emailAddress, @"
^
[-a-zA-Z0-9][-.a-zA-Z0-9]*
@
[-.a-zA-Z0-9]+
(\.[-.a-zA-Z0-9]+)*
\.
(
com|edu|info|gov|int|mil|net|org|biz|
name|museum|coop|aero|pro
|
[a-zA-Z]{2}
)
$",
                 RegexOptions.IgnorePatternWhitespace);
                 */
            }
        }

        //===============================================================
        // Function: BuildBroadcastEmail
        //===============================================================
        public static string BuildBroadcastEmail(string broadcastEmailContent)
        {
            string emailBody = "";

            GlobalData gd = new GlobalData("");
            string linkURL = gd.GetStringValue("SiteBaseURL");

            StringBuilder emailBodyCopy = new StringBuilder();

            emailBodyCopy.AppendLine("<html>");
            emailBodyCopy.AppendLine("<head><title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">");
            emailBodyCopy.AppendLine("<style type=\"text/css\">");
            emailBodyCopy.AppendLine("	body, td, p { font-size: 15px; color: #9B9885; font-family: Arial, Helvetica, Sans-Serif }");
            emailBodyCopy.AppendLine("	p { margin: 0 }");
            emailBodyCopy.AppendLine("	h1 { color: #00ccff; font-size: 18px; font-weight: bold; }");
            emailBodyCopy.AppendLine("	a, .blue { color: #00ccff; text-decoration: none; }");
            emailBodyCopy.AppendLine("	img { border: 0; }");
            emailBodyCopy.AppendLine("</style></head>");
            emailBodyCopy.AppendLine("<body bgcolor=\"#f0f1ec\">");
            emailBodyCopy.AppendLine("  <table width=\"692\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            //emailBodyCopy.AppendLine("	<tr><td colspan=\"3\"><img src=\"http://www.sedogo.com/email-template/images/email-template_01.png\" width=\"692\" height=\"32\" alt=\"\"></td></tr>");
            emailBodyCopy.AppendLine("	<tr><td style=\"background: #fff\" width=\"30\"></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"632\">");

            emailBodyCopy.AppendLine("			<p>" + broadcastEmailContent.Replace("\n", "<br/>") + "</p>");

            emailBodyCopy.AppendLine("			<p>To log into Sedogo, <a href=\"" + linkURL + "\"><u>click here</u></a>.</p>");
            emailBodyCopy.AppendLine("			<br /><br />");
            emailBodyCopy.AppendLine("			<p>Regards</p><a href=\"http://www.sedogo.com\" class=\"blue\"><strong>The Sedogo Team.</strong></a><br />");
            emailBodyCopy.AppendLine("			<br /><br /><br /><a href=\"http://www.sedogo.com\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/logo.gif\" />");
            emailBodyCopy.AppendLine("			</a></td>");
            emailBodyCopy.AppendLine("		<td style=\"background: #fff\" width=\"30\"></td></tr><tr><td colspan=\"3\">");
            //emailBodyCopy.AppendLine("			<img src=\"http://www.sedogo.com/email-template/images/email-template_05.png\" width=\"692\" height=\"32\" alt=\"\">");
            emailBodyCopy.AppendLine("		</td></tr><tr><td colspan=\"3\"><small>This message was intended for <<RECIPIENT>>. To stop receiving these emails, go to your profile and uncheck the 'Enable email notifications' option.<br/>Sedogo offices are located at Sedogo Ltd, The Studio, 17 Blossom St, London E1 6PL.</small></td></tr>");
            emailBodyCopy.AppendLine("		</td></tr></table></body></html>");

            emailBody = emailBodyCopy.ToString();

            return emailBody;
        }

        //===============================================================
        // Function: SendBroadcastEmail
        //===============================================================
        public static void SendBroadcastEmail()
        {
            GlobalData gd = new GlobalData("");
            string broadcastEmailWaiting = gd.GetStringValue("BroadcastEmailWaiting");
            if (broadcastEmailWaiting == "Y")
            {
                // update this straight away - if the broadcast fails then it will not send again to all users
                gd.UpdateStringValue("BroadcastEmailWaiting", "N");

                string broadcastEmailSubject = gd.GetStringValue("BroadcastEmailSubject");
                string broadcastEmailContent = gd.GetStringValue("BroadcastEmailContent");

                string emailBodyCopy = BuildBroadcastEmail(broadcastEmailContent);

                string SMTPServer = gd.GetStringValue("SMTPServer");
                string mailFromAddress = gd.GetStringValue("MailFromAddress");
                string mailFromUsername = gd.GetStringValue("MailFromUsername");
                string mailFromPassword = gd.GetStringValue("MailFromPassword");

                // Get a list of all users
                SqlConnection conn = new SqlConnection(GlobalSettings.connectionString);
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spSelectUserList";
                    DbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        string emailAddress = "";
                        string firstName = "";
                        string lastName = "";

                        int userID = int.Parse(rdr["UserID"].ToString());
                        if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
                        {
                            emailAddress = (string)rdr["EmailAddress"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                        {
                            firstName = (string)rdr["FirstName"];
                        }
                        if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                        {
                            lastName = (string)rdr["LastName"];
                        }
                        Boolean enableSendEmails = (Boolean)rdr["EnableSendEmails"];

                        if (enableSendEmails == true)
                        {
                            try
                            {
                                //MailMessage message = new MailMessage(mailFromAddress, "phil@tuclix.com");
                                MailMessage message = new MailMessage(mailFromAddress, emailAddress);
                                message.ReplyTo = new MailAddress("noreply@sedogo.com");

                                message.Subject = broadcastEmailSubject;
                                message.Body = emailBodyCopy.Replace("<<RECIPIENT>>", emailAddress);
                                message.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = SMTPServer;
                                if (mailFromPassword != "")
                                {
                                    // If the password is blank, assume mail relay is permitted
                                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                                }
                                smtp.Send(message);

                                SentEmailHistory emailHistory = new SentEmailHistory("");
                                emailHistory.subject = broadcastEmailSubject;
                                emailHistory.body = emailBodyCopy.ToString().Replace("<<RECIPIENT>>", emailAddress);
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                            catch (Exception ex)
                            {
                                SentEmailHistory emailHistory = new SentEmailHistory("");
                                emailHistory.subject = broadcastEmailSubject;
                                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString().Replace("<<RECIPIENT>>", emailAddress);
                                emailHistory.sentFrom = mailFromAddress;
                                emailHistory.sentTo = emailAddress;
                                emailHistory.Add();
                            }
                        }
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    ErrorLog errorLog = new ErrorLog();
                    errorLog.WriteLog("MiscUtils", "SendBroadcastEmail", ex.Message, logMessageLevel.errorMessage);
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //===============================================================
        // Function: SendBroadcastTestEmail
        //===============================================================
        public static void SendBroadcastTestEmail(string recipientEmailAddress)
        {
            GlobalData gd = new GlobalData("");

            string broadcastEmailSubject = gd.GetStringValue("BroadcastEmailSubject");
            string broadcastEmailContent = gd.GetStringValue("BroadcastEmailContent");

            string emailBodyCopy = BuildBroadcastEmail(broadcastEmailContent);

            string SMTPServer = gd.GetStringValue("SMTPServer");
            string mailFromAddress = gd.GetStringValue("MailFromAddress");
            string mailFromUsername = gd.GetStringValue("MailFromUsername");
            string mailFromPassword = gd.GetStringValue("MailFromPassword");

            try
            {
                MailMessage message = new MailMessage(mailFromAddress, recipientEmailAddress);
                message.ReplyTo = new MailAddress("noreply@sedogo.com");

                message.Subject = broadcastEmailSubject;
                message.Body = emailBodyCopy.Replace("<<RECIPIENT>>", recipientEmailAddress);
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTPServer;
                if (mailFromPassword != "")
                {
                    // If the password is blank, assume mail relay is permitted
                    smtp.Credentials = new System.Net.NetworkCredential(mailFromAddress, mailFromPassword);
                }
                smtp.Send(message);

                SentEmailHistory emailHistory = new SentEmailHistory("");
                emailHistory.subject = broadcastEmailSubject;
                emailHistory.body = emailBodyCopy.ToString().Replace("<<RECIPIENT>>", recipientEmailAddress);
                emailHistory.sentFrom = mailFromAddress;
                emailHistory.sentTo = recipientEmailAddress;
                emailHistory.Add();
            }
            catch (Exception ex)
            {
                SentEmailHistory emailHistory = new SentEmailHistory("");
                emailHistory.subject = broadcastEmailSubject;
                emailHistory.body = ex.Message + " -------- " + emailBodyCopy.ToString().Replace("<<RECIPIENT>>", recipientEmailAddress);
                emailHistory.sentFrom = mailFromAddress;
                emailHistory.sentTo = recipientEmailAddress;
                emailHistory.Add();
            }
        }


        /// <summary>
        /// Returns the absolute url
        /// </summary>
        /// <param name="relativeUrl">url</param>
        /// <returns>url</returns>
        public static string GetAbsoluteUrl(string relativeUrl)
        {
            HttpRequest Request = HttpContext.Current.Request;
            if (string.IsNullOrEmpty(relativeUrl))
                return null;
            string port = string.Empty;
            if (!Request.Url.IsDefaultPort)
                port = ":" + Request.Url.Port.ToString();
            string start = (Request.IsSecureConnection) ? "https://" : "http://";
            string result = string.Format("{0}{1}{2}{3}", start, Request.Url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl)).Replace('\\', '/');
            return result;
        }
    }
}
