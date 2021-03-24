using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactObj : MonoBehaviour
{
    public GameObject circle;
    public int objId;
    bool isActive;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        circle.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            circle.transform.localScale = Vector3.Lerp(circle.transform.localScale,
                new Vector3(0.14f, 0.14f, 0.14f), Time.deltaTime * 5f);

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if(Physics.Raycast(ray,)
        }
        else
        {
            circle.transform.localScale = Vector3.Lerp(circle.transform.localScale,
                Vector3.zero, Time.deltaTime * 7f);
        }
    }

    private void OnMouseEnter()
    {
        isActive = true;
    }

    private void OnMouseExit()
    {
        isActive = false;
    }

  
}
