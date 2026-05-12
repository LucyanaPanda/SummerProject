using System.Collections.Generic;
using Lucyana.Attributs;
using UnityEngine;

namespace Lucyana.Objects
{
    [CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create Object Data Bank")]
    public class ObjectDataBank : ScriptableObject
    {
        public static ObjectDataBank Instance;
        [Readonly] public List<ObjectData> allObjectsData = new List<ObjectData>();

        public ObjectDataBank()
        {
            if (Instance != null && Instance != this)
            {
                
                DestroyImmediate(this);
                return;
            }
            Instance = this;
        }
        
        public void SetIds()
        {
            uint maxId = 0;
            foreach (ObjectData obj in allObjectsData)
            {
                if (maxId < obj.ID)
                {
                    maxId = obj.ID;
                }
            }

            foreach (ObjectData obj in allObjectsData)
            {
                if (obj.ID == 0)
                {
                    maxId++;
                    obj.ID = maxId;
                }
            }
        }

        public void AddObjectData(ObjectData objData)
        {
            objData.ID = (uint)(allObjectsData.FindLast().ID + 1);
            allObjectsData.Add(objData);
        }
    }
}
