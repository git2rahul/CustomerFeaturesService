using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerFeaturesApi.CacheStore
{
    public class RedIsCache : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public RedIsCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<object> GetAsync(string key)
        {
            string formattedKey = string.Format("{0}", key);
            var cachedValue = await _distributedCache.GetAsync(formattedKey);
            if (cachedValue != null)
            {
                var value = ByteArrayToObject(cachedValue);
                return await Task.FromResult(value);
            }
            return null;
        }

        public Task SetAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

    }
}
