using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum TOWERKIND
    {
        HOUSE, DEFENSE, CASTLE
    }
    public TOWERKIND kind;
    protected int unitNum;
    protected int maxUnit = 100;
    protected int level;
    protected float delay;
    protected float spawnTime = 0f;
    protected TextMesh showText;

    [Range(3, 10)]
    public float speed;

    public int myId;

    //mouse drag
    public GameObject departObj;
    public GameObject arriveObj;
    public int touchCount = 0;


    protected virtual void Awake()
    {
        showText = GetComponentInChildren<TextMesh>();

    }
    protected virtual void Start()
    {
        showText.text = unitNum.ToString();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CreateUnit();
    }

    protected void SpawnUnit()
    {
        if (maxUnit < unitNum) return;

        if (spawnTime > delay)
        {
            spawnTime = 0f;
            unitNum += 1;
            showText.text = unitNum.ToString();
        }
    }

    protected void InitSpawnUnit(float spawnDelay, int count = 0)
    {
        delay = spawnDelay;
        unitNum = count;
    }

    public IEnumerator DragObject()
    {
        Vector3 screen = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen.z));

        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            transform.position = curPosition;
            yield return null;
        }
    }


    protected void CreateUnit()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
        RaycastHit _hit;

        if (Input.GetMouseButtonDown(0) && touchCount < 2)
        {
            if(Physics.Raycast(_ray, out _hit))
            {
                if(_hit.transform.GetComponent<Building>().myId.Equals
                    (_hit.transform.GetComponent<Building>().myId))
                {
                    if(touchCount ==0)
                    {
                        departObj = _hit.transform.gameObject;
                    }
                    else if(touchCount ==1)
                    {
                        if (departObj != _hit.transform.gameObject)
                        {
                            arriveObj = _hit.transform.gameObject;
                        }
                    }
                }
            }
        }


        if(touchCount ==2)
        {
         //  Instantiate(unit)
        }

    }

}
