using Lucyana.Attributs;
using UnityEngine;

namespace Lucyana.Objects
{
    [CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create New Object")]
    public class ObjectData : ScriptableObject
    {
        [Readonly] public uint ID;
        public string NameProduct;
        public int PriceSell;
        public int PriceBuy;
        public Sprite Icon;

        public ObjectData()
        {
            if (ObjectDataBank.Instance == null)
            {
                Debug.LogError("[ObjectData] Object Data Bank Not Found");
                return;
            }
            ObjectDataBank.Instance.AddObjectData(this);
        }
    }
}