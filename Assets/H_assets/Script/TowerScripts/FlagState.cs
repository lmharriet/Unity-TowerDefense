using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagState : MonoBehaviour
{
    public int flagID { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        flagID = -1;
    }

    public void DeActive()
    {
        flagID = -1;
        gameObject.SetActive(false);
    }

}
