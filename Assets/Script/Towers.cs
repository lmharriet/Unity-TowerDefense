using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Towers : MonoBehaviour
{
    int babyMush;
    TextMesh mushCount;
    List<GameObject> mushObj = new List<GameObject>();

    public GameObject mush;

    Ray ray;
    RaycastHit hit;
    Transform destination;

    private void Awake()
    {
        mushCount = transform.GetChild(0).GetComponent<TextMesh>();
    }
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            var obj = Instantiate(mush, transform.position, Quaternion.identity, transform);
            mushObj.Add(obj);
            mushObj[i].SetActive(false);

        }

        babyMush = 20;
        mushCount.text = babyMush.ToString();
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Instantiate(mush, _hit.point, Quaternion.identity);
                destination.position = hit.point;

            }

        }

        for (int i = 0; i < mushObj.Count; i++)
        {
            if (mushObj[i].transform.position != hit.point)
            {

            }

        }

    }



}
