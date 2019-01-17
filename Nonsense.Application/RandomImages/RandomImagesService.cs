using Nonsense.Application.Gateways.WebServices;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.Application.RandomImages {

    public class RandomImagesService : IRandomImagesService {

        private readonly IFlickrService _flickrService;

        public RandomImagesService(IFlickrService flickrService) {
            Guard.NotNull(flickrService, nameof(flickrService));

            _flickrService = flickrService;
        }

        public async Task<BoundaryResponse<string>> GetFlickrImages() {
            var response = await _flickrService.GetRandomImages(tags: "nonsense", imagesCount: 10);

            return new BoundaryResponse<string>(response.Success, response.ErrorsList, response.Data);
        }
    }
}
