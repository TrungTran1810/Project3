
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [System.Serializable]
    public class PoolItem
    {
       public int Amount;
        public GameObject prefab; 
    }
    public List<PoolItem> poolItems;
    public List<GameObject> poolArr;

    public GameObject GetGameObject(string tag)
    {
        for(int i = 0; i < poolArr.Count; i++)
        {
            if (!poolArr[i].activeInHierarchy && poolArr[i].tag == tag)
            {
                return poolArr[i];
            }
        }
        return null;
    }

    private void Start()
    {
        poolArr = new List<GameObject>();   
        foreach(var item in poolItems)
        {
            for(int i = 0; i < item.Amount; i++)
            {
                GameObject a = Instantiate(item.prefab);
                a.SetActive(false);
                poolArr.Add(a);
            }
        }
    }
}
