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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
                    string tempFilename = filename.Substring(0, pos);
                    string tempExtension = filename.Substring(pos + 1, (filename.Length - pos - 1));
                    newPath = tempFilename + "_" + fileNumber.ToString() + "." + tempExtension;
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
                sedogoEvent.eventPicFilename = filename;
                sedogoEvent.eventPicPreview = previewFileName;
                sedogoEvent.eventPicThumbnail = thumbnailFileName;
                sedogoEvent.UpdateEventPic();

                returnStatus = 0;
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

                if (filename.IndexOf(".gif") > 0)
                {
                    imageFmt = ImageFormat.Gif;
                    ripMethod = "Image";
                }
                if (filename.IndexOf(".jpg") > 0)
                {
                    imageFmt = ImageFormat.Jpeg;
                    ripMethod = "Image";
                }
                if (filename.IndexOf(".bmp") > 0)
                {
                    imageFmt = ImageFormat.Bmp;
                    ripMethod = "Image";
                }

                if (ripMethod == "Image")
                {
                    // Create thumbnail
                    System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(fileStoreFolder + @"\" + filename);

                    // Correct the sizes taking into account the aspect ratio of the image
                    double pictureAspectRatio = (double)sourceImage.Width / (double)sourceImage.Height;
                    if (pictureAspectRatio < 1)
                    {
                        // Portrait orientation
                        thumbnailWidth = (int)(thumbnailWidth * pictureAspectRatio);
                    }
                    else
                    {
                        // Landscape orientation
                        thumbnailHeight = (int)(thumbnailHeight / pictureAspectRatio);
                    }
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
                    if (pictureAspectRatio < 1)
                    {
                        // Portrait orientation
                        previewWidth = (int)(previewWidth * pictureAspectRatio);
                    }
                    else
                    {
                        // Landscape orientation
                        previewHeight = (int)(previewHeight / pictureAspectRatio);
                    }
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
    }
}
