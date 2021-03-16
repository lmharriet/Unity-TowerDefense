using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagState : MonoBehaviour
{
    public int flagID;
    // Start is called before the first frame update
    public void ActiveFlag(int towerId)
    {
        flagID = towerId;
    }

    public void DeActiveFlag()
    {
        flagID = -1;
        gameObject.SetActive(false);
    }

}
