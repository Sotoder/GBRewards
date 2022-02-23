using System.IO;
using UnityEngine;

public class JsonData<T> : IData<T>
{
    private Encryptor _encryptor = new Encryptor();
    public void Save(T data, string path = null)
    {
        var saveString = JsonUtility.ToJson(data);
        File.WriteAllText(path, _encryptor.Encrypt(saveString));
    }

    public T Load(string path = null)
    {
        var loadString = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(_encryptor.Decrypt(loadString));
    }
}