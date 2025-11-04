using UnityEngine.Pool;

namespace KeceK.Game
{
    /// <summary>
    /// Interface for objects that can be pooled. Implement this on your MonoBehaviour components.
    /// </summary>
    /// <typeparam name="T">The type of the pooled object (itself)</typeparam>
    public interface IPoolable<T> where T : class
    {
        /// <summary>
        /// Called when the object is first created in the pool
        /// </summary>
        void OnCreate();
        
        /// <summary>
        /// Called when the object is retrieved from the pool
        /// </summary>
        void OnGetFromPool();
        
        /// <summary>
        /// Called when the object is returned to the pool
        /// </summary>
        void OnReturnToPool();
        
        /// <summary>
        /// Called when the object is being destroyed (pool exceeded max size)
        /// </summary>
        void OnDestroyPoolObject();
        
        /// <summary>
        /// Sets the pool reference for this object
        /// </summary>
        void SetPool(IObjectPool<T> pool);
        
        /// <summary>
        /// Returns this object to the pool
        /// </summary>
        void ReturnToPool();
    }
}

