using UnityEngine;

namespace Lucyana.Objects.Items
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public abstract class Item : MonoBehaviour, ILootableObject<ObjectData>
    {
        [SerializeField] protected ObjectData data;
        protected MeshRenderer meshRenderer;
        protected MeshCollider meshCollider;
        protected Rigidbody rb;
        protected virtual void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
            rb = GetComponent<Rigidbody>();
        }

        public abstract ObjectData Loot();

        #region GetData

        public ObjectData GetData()
        {
            return data;
        }

        #endregion
    }
}
