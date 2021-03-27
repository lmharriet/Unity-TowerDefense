using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagState : MonoBehaviour
{
    public int flagID;
    public EnumSpace.TEAMCOLOR flagColor;
    // Start is called before the first frame update
    public void ActiveFlag(int towerId , EnumSpace.TEAMCOLOR color)
    {
        flagID = towerId;
        flagColor = color;
        transform.GetChild((int)color - 1).gameObject.SetActive(true);
    }

    public void DeActiveFlag()
    {
        flagID = -1;
        for (int i = 0; i < transform.childCount; i++)
        {
            var _child = transform.GetChild(i).gameObject;
            if (_child.activeSelf) _child.SetActive(false);
        }
        gameObject.SetActive(false);
    }

}
