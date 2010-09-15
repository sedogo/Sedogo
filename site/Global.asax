<%@ Application Language="C#" %>
<%@ Import Namespace="System.Xml.XPath" %>
<%@ Import Namespace="Sedogo.BusinessObjects" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">

    //===============================================================
    // Function:    Application_Start
    // Description: Code that runs on application startup
    //===============================================================
    void Application_Start(object sender, EventArgs e) 
    {
        string environment = ConfigurationManager.AppSettings["EnvironmentName"].ToString();
        string connectionString = ConfigurationManager.ConnectionStrings[environment].ToString();
        string errorLogFile = ConfigurationManager.AppSettings["ErrorLogFile"].ToString();
        string errorLogLevel = ConfigurationManager.AppSettings["ErrorLogLevel"].ToString();

        Application.Add("connectionString", connectionString);
        Application.Add("ErrorLogFile", errorLogFile);
        Application.Add("ErrorLogLevel", errorLogLevel);

        GlobalSettings.connectionString = (string)Application["connectionString"];
        GlobalSettings.errorLogFile = (string)Application["ErrorLogFile"];
        GlobalSettings.errorLogLevel = (logMessageLevel)int.Parse(Application["ErrorLogLevel"].ToString());

        var document = new XPathDocument(Path.Combine(Server.MapPath(Application["SystemRootPath"] as string), "version.xml"));
        var navigator = document.CreateNavigator();
        string strVersionNumber = navigator.SelectSingleNode("/version/number").Value;
        string strBuildNumber = navigator.SelectSingleNode("/version/build").Value;
        string strBuildDate = navigator.SelectSingleNode("/version/date").Value;

        Application.Add("VersionNumber", strVersionNumber);
        Application.Add("BuildNumber", strBuildNumber);
        Application.Add("BuildDate", strBuildDate);

        GlobalData gd = new GlobalData("");
        string dateFormat = gd.GetStringValue("DateFormat");
        Application.Add("DateFormat", dateFormat);
        
        ErrorLog errorLog = new ErrorLog();
        errorLog.WriteLog("Global_asax", "Application_Start", "Application start", logMessageLevel.infoMessage);

        //GlobalData gd = new GlobalData("");
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
