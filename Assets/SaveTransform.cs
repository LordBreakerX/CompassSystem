using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LordBreakerX.Save;

namespace LordBreakerX
{
    [System.Serializable]
    public struct STransformData
    {
        public string objectID;
        public Vector3 position;
        public Quaternion rotation;

        public STransformData(string objectID, Vector3 position, Quaternion rotation)
        {
            this.objectID = objectID;
            this.position = position;
            this.rotation = rotation;
            
        }
    }

    public class SaveTransform : MonoBehaviour
    {
        private const string SAVE_NAME = "SAVE_TEST";

        private static int _lastIDNumber = -1;

        public string ObjectID { get; private set; }

        private void Awake()
        {
            _lastIDNumber++;
            ObjectID = $"SaveTransform_{name}_{_lastIDNumber}";
            SaveController.Register(this);
        }

        public STransformData GetTransformData()
        {
            return new STransformData(ObjectID, transform.position, transform.rotation);
        }

        public void SetTransformData(STransformData data)
        {
            transform.position = data.position;
            transform.rotation = data.rotation;
        }
    }
}
