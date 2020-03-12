using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Transform poolHolder;
        public int size;
        public float scaleModifier;
    }

    public static ObjectPooler instance;

    public List<Pool> ListOfPools;

    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in ListOfPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.poolHolder);

                obj.transform.localScale *= pool.scaleModifier;

                obj.SetActive(false);

                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tagName, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (!PoolDictionary.ContainsKey(tagName))
        {
            Debug.Log("Object not found");
            return null;
        }

        GameObject objToSpawn = null;

        for (int i = 0; i < PoolDictionary[tagName].Count; i++)
        {
            objToSpawn = PoolDictionary[tagName].Dequeue();

            if (objToSpawn.activeSelf)
            {
                PoolDictionary[tagName].Enqueue(objToSpawn);
                objToSpawn = null;
                continue;
            }
            else if (!objToSpawn.activeSelf)
            {
                objToSpawn.SetActive(true);
                objToSpawn.transform.position = spawnPosition;
                objToSpawn.transform.rotation = spawnRotation;
                PoolDictionary[tagName].Enqueue(objToSpawn);
                break;
            }

        }

        return objToSpawn;
    }
}
