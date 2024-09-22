using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LordBreakerX.Save
{
    public static class JsonSaveManager
    {
        public static void SaveToJson<TData>(string saveName, TData saveData) 
        {
            string jsonData = JsonUtility.ToJson(saveData);
            string filePath = $"{Application.persistentDataPath}/{saveName}.json";
            File.WriteAllText(filePath, jsonData);
        }

        public static TData LoadFromJson<TData>(string saveName, TData defaultData)
        {
            string filePath = $"{Application.persistentDataPath}/{saveName}.json";
            TData loadedData = defaultData;


            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                loadedData = JsonUtility.FromJson<TData>(jsonData);
            }

            return loadedData;
        }
    }
}
