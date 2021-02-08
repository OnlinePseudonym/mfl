using MFL.Common.JsonConverters;
using MFL.DataTransferObjects;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MFL.Services.Clients
{
    public interface IMFLHttpClient
    {
        HttpClient Client { get; }
        Task<MFLApiResponse> GetFromJsonAsync(string endpoint, JsonConverter customConverter = null);
    }
    public class MFLHttpClient : IMFLHttpClient
    {
        public HttpClient Client { get; }
        public MFLHttpClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<MFLApiResponse> GetFromJsonAsync(string endpoint, JsonConverter customConverter = null)
        {
            MFLApiResponse results;

            var options = new JsonSerializerOptions();

            options.Converters.Add(new SingleOrManyConverter<PlayerDTO>());
            options.Converters.Add(new SingleOrManyConverter<FranchiseDTO>());
            options.Converters.Add(new SingleOrManyConverter<LeagueInstanceDTO>());

            if (customConverter != null)
            {
                options.Converters.Add(customConverter);
            }

            var headers = Client.DefaultRequestHeaders;

            results = await Client.GetFromJsonAsync<MFLApiResponse>(endpoint, options);

            return results;
        }
    }
}
