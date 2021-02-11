using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public List<GameObject> pooledObject;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObject = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject _obj = (GameObject)Instantiate(objectToPool);
            _obj.SetActive(false);
            pooledObject.Add(_obj);
        }
    }

    public GameObject GetObjectFromPooler()
    {
        int _size = pooledObject.Count;
        for (int i = 0; i < _size; i++)
        {
            if(!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }
}
