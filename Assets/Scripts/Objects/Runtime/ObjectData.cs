using Lucyana.Attributs;
using UnityEngine;

namespace Lucyana.Objects
{
    [DefaultExecutionOrder(-100)]
    [CreateAssetMenu(fileName = "Objects", menuName = "Objects/Create New Object")]
    public class ObjectData : ScriptableObject
    {
        [Readonly] public uint ID;
        public string NameProduct;
        public int PriceSell;
        public int PriceBuy;
        public Sprite Icon;

        public void OnEnable()
        {
            if (ObjectDataBank.Instance == null)
                return;
            
            ObjectDataBank.Instance.AddObjectData(this);
        }
    }
}