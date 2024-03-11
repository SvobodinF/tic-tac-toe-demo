public interface IStorage
{
    public void SaveByKey<T>(string key, in T value);
    public void LoadByKey<T>(string key, out T value, T defaultValue);
    public bool HasItemByKey(string key);
    public void ClearItemByKey(string key);
    public void ClearAll();
}
