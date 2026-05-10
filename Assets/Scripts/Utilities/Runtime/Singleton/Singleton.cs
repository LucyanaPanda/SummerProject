using UnityEngine;

namespace Lucyana.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : class
    {
        public static T Instance;

        public virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this as T;
        }

        public virtual void OnDestroy()
        {
            if (ReferenceEquals(Instance, this))
            {
                Instance = null;
            }
        }
    }
}
