namespace LMS.Web.Utility
{
    public class SD
    {
        // URLs for API
        public static string GroupAPIBase {  get; set; }
        public static string StudentAPIBase {  get; set; }
        public static string SubjectAPIBase {  get; set; }

        public enum ApiType
        {
            GET, 
            POST, 
            PUT, 
            DELETE
        }
    }
}
