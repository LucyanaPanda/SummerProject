using System.Collections.Generic;
using Lucyana.Attributs;
using UnityEngine;

namespace Lucyana.Objects
{
    [DefaultExecutionOrder(-200)]
    [CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create Object Data Bank")]
    public class ObjectDataBank : ScriptableObject
    {
        public static ObjectDataBank Instance;
        
        private Dictionary<uint, ObjectData> bank = new Dictionary<uint, ObjectData>();
        [SerializeField, Readonly] private List<ObjectData> allObjectsData = new List<ObjectData>();

        public void OnEnable()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;
            RebuildDictionaryBank();
        }

        public void AddObjectData(ObjectData objData)
        {
            if (allObjectsData.Contains(objData)) return;
            
            objData.ID = allObjectsData.Count > 0
                ? allObjectsData[allObjectsData.Count - 1].ID + 1
                : 1;
            allObjectsData.Add(objData);
            bank.Add(objData.ID, objData);
            Debug.Log(bank.Count);
        }
        
        public ObjectData GetObjectDataFromBank(uint objectID)
        {
            if (bank.TryGetValue(objectID, out ObjectData objectData)) {
                return objectData;
            }
            return null;
        }

        private void RebuildDictionaryBank()
        {
            bank.Clear();
            foreach (ObjectData objData in allObjectsData)
            {
                bank.Add(objData.ID, objData);
            }
        }
    }
}
