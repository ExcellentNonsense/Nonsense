using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.WebServices {

    public interface IFlickrService {

        Task<FlickrServiceResponse> GetRandomImages(string tags, int imagesCount);
    }
}
