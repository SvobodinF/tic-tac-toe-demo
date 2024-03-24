using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerPrefsStorage : IStorage
{
    public bool HasItemByKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void LoadByKey<T>(string key, out T value, T defaultValue)
    {
        string data = PlayerPrefs.GetString(key);
        value = FromByteArray(Convert.FromBase64String(data), defaultValue);
    }

    public void SaveByKey<T>(string key, in T value)
    {
        PlayerPrefs.SetString(key, Convert.ToBase64String(ToByteArray(value)));
    }

    public void ClearItemByKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private byte[] ToByteArray<T>(T obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    private T FromByteArray<T>(byte[] data, T defaultValue)
    {
        if (data == null || data.Length == 0)
            return defaultValue;

        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream(data))
        {
            object obj = bf.Deserialize(ms);
            return (T)obj;
        }
    }
}
