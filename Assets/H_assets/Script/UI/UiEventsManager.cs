using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEventsManager : MonoBehaviour
{
    public MouseDrag mouseDrag;

    void Awake()
    {   
        
        
    }


    public void SetPercentage(float value)
    {
        mouseDrag.SendPercentage = value;
    }
}

