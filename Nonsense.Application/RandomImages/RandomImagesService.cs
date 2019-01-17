using Nonsense.Application.Gateways.WebServices;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.Application.RandomImages {

    public class RandomImagesService : IRandomImagesService {

        private readonly IFlickrService _flickrService;
        private readonly string imagesCategory = "nonsense";

        public RandomImagesService(IFlickrService flickrService) {
            Guard.NotNull(flickrService, nameof(flickrService));

            _flickrService = flickrService;
        }

        public async Task<BoundaryResponse<string>> GetFlickrImages(int imagesCount) {
            var response = await _flickrService.GetRandomImages(imagesCategory, imagesCount);

            return new BoundaryResponse<string>(response.Success, response.ErrorsList, response.Data);
        }
    }
}
