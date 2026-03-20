using System.IO;
using UnityEngine;

namespace Helpers
{
    //haha generic methods to save things and not to rewrite them all the time
    public static class SaveSystem
    {
        //here where T:new() means that it is guaranteed that a new T can be created
        public static T Load<T>(string path) where T : new()
        {
            if (File.Exists(path))
            {
                return JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            else
            {
                return new T();
            }
        }

        public static void Save<T>(string path, T data) 
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }
    }
}