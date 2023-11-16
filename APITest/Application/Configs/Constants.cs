namespace APITest.Application.Configs
{
    public class Constants
    {
        public static Dictionary<int, string> ErrorCodes = new Dictionary<int, string>() {
            { 200, "Request successful" },
            { 204 , "Resource not found" }
        };
    }

    public enum ErrorCodeEnum
    {
        Success = 200,
        NotFound = 204,
        InvalidData = 406
    }
}
