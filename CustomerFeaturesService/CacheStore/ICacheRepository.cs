using System.Threading.Tasks;

namespace CustomerFeaturesApi.CacheStore
{
    public interface ICacheRepository
    {
        Task<object> GetAsync(string key);

        Task SetAsync(string key, object value);

    }
}
