using System;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.WebServices {
    public interface IFlickrService {
        Task<String> GetRandomPhotos(string tags, int photosCount);
    }
}
