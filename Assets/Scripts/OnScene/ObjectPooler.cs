using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler Instance;

	[System.Serializable]
	public class PoolInfo
	{
		public GameObject prefab;
		public Transform parentTransform;
		public int countToPool = 20;
	}

	[SerializeField] public List<PoolInfo> pools;

	private Dictionary<GameObject, List<GameObject>> pooledObjects;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		pooledObjects = new Dictionary<GameObject, List<GameObject>>();

		// Create pools for each specified prefab
		foreach (PoolInfo poolInfo in pools)
		{
			List<GameObject> objectPool = new List<GameObject>();
			for (int i = 0; i < poolInfo.countToPool; i++)
			{
				Transform parent = transform;
				if (poolInfo.parentTransform != null)
				{
					parent = poolInfo.parentTransform;
				}
				GameObject instantiatedObject = Instantiate(poolInfo.prefab, parent);
				instantiatedObject.SetActive(false);
				objectPool.Add(instantiatedObject);
			}
			pooledObjects.Add(poolInfo.prefab, objectPool);
		}
	}

	private void Start()
	{
		EventManager.Instance.OnNextLevelTrigger += HandleOnNextLevelTrigger;
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelTrigger -= HandleOnNextLevelTrigger;
	}

	public GameObject GetPooledObject(GameObject prefab)
	{
		if (pooledObjects.ContainsKey(prefab))
		{
			List<GameObject> objectPool = pooledObjects[prefab];
			for (int i = 0; i < objectPool.Count; i++)
			{
				if (!objectPool[i].activeInHierarchy)
				{
					return objectPool[i];
				}
			}
		}

		return null;
	}
	public List<GameObject> GetPooledObjectsList(GameObject prefab) 
	{
		if(pooledObjects.ContainsKey(prefab))
		{
			return pooledObjects[prefab];
		}
		return null;
	}

	public void ResetAllPooledObjects()
	{
		foreach (var keyValuePair in pooledObjects)
		{
			foreach (var obj in keyValuePair.Value)
			{
				obj.SetActive(false);
			}
		}
	}
	private void HandleOnNextLevelTrigger()
	{
		ResetAllPooledObjects();
	}
}
