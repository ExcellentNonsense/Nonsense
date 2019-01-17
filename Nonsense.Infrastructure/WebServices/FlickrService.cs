using Microsoft.Extensions.Configuration;
using Nonsense.Application;
using Nonsense.Application.Gateways.WebServices;
using Nonsense.Common.Utilities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.WebServices {

    public sealed class FlickrService : IFlickrService {

        private readonly HttpClient _client;
        private readonly string _apiKey;

        private readonly int _perSearch_ApiLimit = 4000;
        private readonly int _perPage_ApiLimit = 500;

        public FlickrService(HttpClient client, IConfiguration config) {
            Guard.NotNull(client, nameof(client));
            Guard.NotNull(config, nameof(config));

            _client = client;
            _client.BaseAddress = new Uri(config["FlickrService:Uri"]);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("User-Agent", "ExcellentNonsense-Nonsense");

            _apiKey = config["FlickrService:ApiKey"];
        }

        public async Task<BoundaryResponse<string>> GetRandomImages(string tags, int imagesCount) {
            var result = new BoundaryResponse<string>(true, null, String.Empty);

            if (0 >= imagesCount || imagesCount > _perPage_ApiLimit) {
                result.Success = false;
                result.AddError($"The number of images requested is out of range - 0-{_perPage_ApiLimit}.");
            }

            if (result.Success) {
                Random randomNumbers = new Random();
                int pageNumber = randomNumbers.Next(1, _perSearch_ApiLimit / imagesCount);

                var url = new Uri("?method=flickr.photos.search&media=photos&privacy_filter=1&safe_search=1&extras=url_z&format=json&nojsoncallback=1&" +
                    $"api_key={_apiKey}&tags={tags}&per_page={imagesCount}&page={pageNumber}", UriKind.Relative);

                try {
                    result.Data = await _client.GetStringAsync(url);
                }
                catch (HttpRequestException) {
                    result.Success = false;
                    result.AddError("The request failed.");
                }
            }

            return result;
        }
    }
}
