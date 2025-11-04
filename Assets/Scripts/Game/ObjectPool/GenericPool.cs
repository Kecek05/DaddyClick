using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace KeceK.Game
{
    /// <summary>
    /// Generic object pool for any MonoBehaviour type. Handles pooling without needing GetComponent calls.
    /// </summary>
    /// <typeparam name="T">The component type to pool (must inherit from PoolableMonoBehaviour)</typeparam>
    public class GenericPool<T> : MonoBehaviour where T : PoolableMonoBehaviour<T>
    {
        [Title("References")]
        [SerializeField] [Required] private T _prefab;
        
        [Title("Settings")]
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private int _maxSize = 100;
        [Tooltip("This will check if an object is already in the pool when returning it. Useful for debugging but has a performance cost.")]
        [SerializeField] private bool _collectionCheck = false;

        private IObjectPool<T> _pool;
        private Transform _poolParent;
        public IObjectPool<T> Pool => _pool;

        private void Awake()
        {
            // Create a parent GameObject to organize pooled objects
            GameObject poolFolder = new GameObject($"{typeof(T).Name}_Pool");
            _poolParent = poolFolder.transform;
            
            _pool = new ObjectPool<T>(
                CreatePooledItem, 
                OnGetFromPool, 
                OnReturnedToPool, 
                OnDestroyPoolObject, 
                _collectionCheck, 
                _initialPoolSize, 
                _maxSize);
        }
        
        public T Get()
        {
            return _pool.Get();
        }
        
        public void Release(T item)
        {
            _pool.Release(item);
        }
        
        private T CreatePooledItem()
        {
            T instance = Instantiate(_prefab, _poolParent);
            instance.SetPool(_pool);
            instance.OnCreate();
            return instance;
        }
        
        private void OnReturnedToPool(T obj)
        {
            obj.OnReturnToPool();
            obj.gameObject.SetActive(false);
        }
        
        private void OnGetFromPool(T obj)
        {
            obj.gameObject.SetActive(true);
            obj.OnGetFromPool();
        }
        
        /// <summary>
        /// Invoked when we exceed max pool size and need to destroy object
        /// </summary>
        private void OnDestroyPoolObject(T obj)
        {
            obj.OnDestroyPoolObject();
            Destroy(obj.gameObject);
        }
    }
}

