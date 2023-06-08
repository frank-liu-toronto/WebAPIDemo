using System.Text.Json;

namespace WebApp.Data
{
    public class WebApiException: Exception
    {
        public ErrorResponse? ErrorResponse { get; }

        public WebApiException(string errorJson)
        {
            ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
        }
    }
}
