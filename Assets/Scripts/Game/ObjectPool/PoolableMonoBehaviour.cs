using UnityEngine;
using UnityEngine.Pool;

namespace KeceK.Game
{
    /// <summary>
    /// Base class for poolable objects. Inherit from this for easy pooling support.
    /// </summary>
    /// <typeparam name="T">The type of the derived class (itself)</typeparam>
    public abstract class PoolableMonoBehaviour<T> : MonoBehaviour, IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        protected IObjectPool<T> _pool;

        public virtual void SetPool(IObjectPool<T> pool)
        {
            _pool = pool;
        }

        public virtual void ReturnToPool()
        {
            _pool?.Release(this as T);
        }

        public virtual void OnCreate()
        {
            // Override in derived classes if needed
        }

        public virtual void OnGetFromPool()
        {
            // Override in derived classes if needed
        }

        public virtual void OnReturnToPool()
        {
            // Override in derived classes if needed
        }

        public virtual void OnDestroyPoolObject()
        {
            // Override in derived classes if needed
        }
    }
}

