namespace TwoFace.Cache.Abstract
{
    public interface ICacheService
    {
        T Get<T>(string cacheKey) where T : class;
        void Set(string cacheKey, object item);
        void Set(string cacheKey, object item, int minutes);
    }
}
