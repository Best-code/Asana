using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FirebaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://csharpmills-default-rtdb.firebaseio.com/";
    private readonly string _authToken; // Optional if public DB

    public FirebaseService(string baseUrl, string authToken = null)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _authToken = authToken;
        _httpClient = new HttpClient();
    }

    private string BuildUrl(string path)
    {
        return $"{_baseUrl}/{path}.json{(_authToken != null ? $"?auth={_authToken}" : "")}";
    }

    public async Task<T> GetAsync<T>(string path)
    {
        var res = await _httpClient.GetAsync(BuildUrl(path));
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task SetAsync<T>(string path, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        await _httpClient.PutAsync(BuildUrl(path), new StringContent(json, Encoding.UTF8, "application/json"));
    }

    public async Task<string> PushAsync<T>(string path, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var response = await _httpClient.PostAsync(BuildUrl(path), new StringContent(json, Encoding.UTF8, "application/json"));
        var result = await response.Content.ReadAsStringAsync();
        dynamic resObj = JsonConvert.DeserializeObject(result);
        return resObj.name; // Firebase returns the generated key
    }

    public async Task DeleteAsync(string path)
    {
        await _httpClient.DeleteAsync(BuildUrl(path));
    }
}
