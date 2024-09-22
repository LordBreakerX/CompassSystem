using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LordBreakerX.Save
{
    public class SaveController : MonoBehaviour
    {
        private static List<SaveTransform> saveTransforms = new List<SaveTransform>();

        public static void Register(SaveTransform saveTransform)
        {
            if (saveTransform != null && !saveTransforms.Contains(saveTransform))
            {
                saveTransforms.Add(saveTransform);
            }
        }

        public static void Deregister(SaveTransform saveTransform)
        {
            if (saveTransform != null && saveTransforms.Contains(saveTransform))
            {
                saveTransforms.Remove(saveTransform);
            }
        }

        private static SaveTransform GetTransform(STransformData sTransformData)
        {
            SaveTransform saveTransform = null;

            if (saveTransforms.Count > 0)
            {
                foreach (SaveTransform transform in saveTransforms)
                {
                    if (transform.ObjectID == sTransformData.objectID)
                    {
                        saveTransform = transform;
                        break;
                    }
                }
            }

            return saveTransform;
        }

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene arg0)
        {
            SaveData data = new SaveData();

            data.SetData(saveTransforms);

            JsonSaveManager.SaveToJson("GameSave", data);
        }

        private void Start()
        {
            SaveData data = JsonSaveManager.LoadFromJson("GameSave", new SaveData());
            if (data.objectsLocation != null && data.objectsLocation.Length > 0)
            {
                foreach (STransformData trasformData in data.objectsLocation)
                {
                    SaveTransform saveTransform = GetTransform(trasformData);
                    if (saveTransform != null) saveTransform.SetTransformData(trasformData);
                }
            }
        }

        private void OnApplicationQuit()
        {
            OnSceneUnloaded(SceneManager.GetActiveScene());
        }

    }

    public struct SaveData
    {
        public STransformData[] objectsLocation;

        public void SetData(List<SaveTransform> saveTransforms)
        {
            List<STransformData> dataList = new List<STransformData>();
            foreach (SaveTransform transform in saveTransforms)
            {
                dataList.Add(transform.GetTransformData());
            }
            objectsLocation = dataList.ToArray();
        }
    }
}
