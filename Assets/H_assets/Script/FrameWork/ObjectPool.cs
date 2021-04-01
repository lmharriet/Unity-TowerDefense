using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //class를 inspector창에서 볼 수있게 해주는 어트리뷰트
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefToPool;
    public bool shouldExpend;
}
public class ObjectPool : MonoBehaviour
{
    public List<ObjectPoolItem> itemToPool;

    public static ObjectPool instance;
    private List<GameObject> pooledObject;

    private void Awake()
    {
        if (ObjectPool.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            pooledObject = new List<GameObject>();

            foreach (ObjectPoolItem item in itemToPool)
            {
                for (int i = 0; i < item.amount; i++)
                {
                    GameObject _obj = Instantiate(item.prefToPool);
                    _obj.SetActive(false);
                    _obj.transform.parent = transform;
                    pooledObject.Add(_obj);
                }
            }
            Debug.Log("obejct pool is here");

        }
        else
        {
            Debug.Log("opjectPool null아님");
        }

    }

    public GameObject GetObjectFromPooler()
    {
        int _size = pooledObject.Count;
        for (int i = 0; i < _size; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }

    public GameObject GetObjectFromPooler(string tag)
    {
        int _size = pooledObject.Count;
        for (int i = 0; i < _size; i++)
        {

            if (!pooledObject[i].activeInHierarchy)
            {
                if (pooledObject[i].CompareTag(tag))
                    return pooledObject[i];
            }

        }

        foreach (ObjectPoolItem item in itemToPool)
        {
            if (item.prefToPool.CompareTag(tag))
            {
                if (item.shouldExpend)
                {
                    GameObject _obj = Instantiate(item.prefToPool);
                    _obj.SetActive(false);
                    pooledObject.Add(_obj);
                    return _obj;
                }
            }

        }

        return null;
    }

    public void DeActiveAllPref()
    {
        for(int i=0;i< pooledObject.Count;i++)
        {
            pooledObject[i].SetActive(false);
        }
    }
}


