using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.WebServices {

    public interface IFlickrService {

        Task<FlickrServiceResponse> GetRandomPhotos(string tags, int photosCount);
    }
}
