using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPooler : MonoBehaviour
{
    [System.Serializable]
    public class VFX_Magazine
    {
        public string tag;
        public GameObject vfxToPool;
        public Transform vfxHolder;
        public int numberOfVFX;
        public float scaleModifier;
    }

    public static VFXPooler Instance;

    public List<VFX_Magazine> ListOfVfxMagazines;

    public Dictionary<string, Queue<GameObject>> MagazineDictionary;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MagazineDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var magazine in ListOfVfxMagazines)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < magazine.numberOfVFX; i++)
            {
                GameObject obj = Instantiate(magazine.vfxToPool, magazine.vfxHolder);

                obj.transform.localScale *= magazine.scaleModifier;


                obj.SetActive(false);

                objectPool.Enqueue(obj);
            }

            MagazineDictionary.Add(magazine.tag, objectPool);
        }
    }



    public GameObject SpawnVfxFromPool(string tagName, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (!MagazineDictionary.ContainsKey(tagName))
        {
            Debug.Log("VFX Not Found");
            return null;
        }

        GameObject objToSpawn = null;

        for (int i = 0; i < MagazineDictionary[tagName].Count; i++)
        {
            objToSpawn = MagazineDictionary[tagName].Dequeue();

            if (objToSpawn.activeSelf)
            {
                MagazineDictionary[tagName].Enqueue(objToSpawn);
                objToSpawn = null;
                continue;
            }
            else if (!objToSpawn.activeSelf)
            {
                objToSpawn.SetActive(true);
                objToSpawn.transform.position = spawnPosition;
                objToSpawn.transform.rotation = spawnRotation;
                MagazineDictionary[tagName].Enqueue(objToSpawn);
                break;
            }
        }

        if(objToSpawn == null)
        {
            Debug.Log("Magazine Empty");
        }

        return objToSpawn;
    }
}
