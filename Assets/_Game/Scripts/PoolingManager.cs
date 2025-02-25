
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
        for (int i = 0; i < poolArr.Count; i++)
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
        foreach (var item in poolItems)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                GameObject a = Instantiate(item.prefab);
                a.SetActive(false);
                poolArr.Add(a);
            }
        }
    }
}



//public class PoolingManager : MonoBehaviour
//{
//    public static PoolingManager instance;

//    public GameObject bulletprefab;

//    public int size = 10;

//    private List<GameObject> bulletpool;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }

//    }
//    private void Start()
//    {
//        bulletpool = new List<GameObject>();
//        for (int i = 0; i < size; i++) {
//            GameObject bullet = Instantiate(bulletprefab);
//            bullet.SetActive(false);
//            bulletpool.Add(bullet);
//        }
//    }

//    public GameObject Getbullet()
//    {
//        foreach (GameObject bullet in bulletpool)
//        {

//            if (!bullet.activeInHierarchy)
//            {

//                return bullet;
//            }
//        }
//        return null;
//    }

//}
