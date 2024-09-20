namespace KironWebAPI.Core.Interfaces
{
    public interface ICacheService
    {
        T GetData<T>(string cachekey);
        bool SetData<T>(string cachekey, T value, int time);
        void RemoveData(string cachekey);
    }
}
