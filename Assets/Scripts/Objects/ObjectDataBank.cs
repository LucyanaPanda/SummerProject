using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create Object Data Bank")]
public class ObjectDataBank : ScriptableObject
{
    public List<ObjectData> allObjectsData;

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
}
