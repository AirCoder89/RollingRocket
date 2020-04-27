﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools) //loop through all the pools
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i< pool.size; i++) //fill the pool
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
	}

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("ObjectPooler : Pool with " + tag + "doesn't exists !");
            return null;
        }
        
        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj =  objToSpawn.GetComponent<IPooledObject>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
       poolDictionary[tag].Enqueue(objToSpawn);
       
        return objToSpawn;
    }

	
}
