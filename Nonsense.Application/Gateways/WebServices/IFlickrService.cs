using System.Threading.Tasks;

namespace Nonsense.Application.Gateways.WebServices {

    public interface IFlickrService {

        Task<BoundaryResponse<string>> GetRandomImages(string tags, int imagesCount);
    }
}
