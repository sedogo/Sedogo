using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Sedogo.BusinessObjects;

public partial class admin_Misc_RenewThumbnails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonRenewThumbnails_Click(object sender, EventArgs e)
    {

        if (TextBoxImagesPath.Text == "") return;
            foreach (string fileName in Directory.GetFiles(TextBoxImagesPath.Text))
            {
               string fName = Path.GetFileName(fileName);
                if (fName.Contains("_T") || fileName.Contains("_P")) continue;
                MiscUtils.CreatePreviews(fileName);

            }
    }
}
