namespace Todo.Ui.Apps
{
    public class ApiEndpoints
    {
        public string BaseUrl { get; private set; }
        public ApiEndpoints(string BaseUrl)
        {
            if (string.IsNullOrEmpty(BaseUrl))
            {
                BaseUrl = "https://localhost:7244/api";
            }
            this.BaseUrl = BaseUrl;
        }
        public string ToDo { get => Combine(BaseUrl, "activity"); }
        public string User { get => Combine(BaseUrl, "account"); }
        public static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
    }
}
