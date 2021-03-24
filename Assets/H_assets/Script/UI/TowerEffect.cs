using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEffect : MonoBehaviour
{

    public Color circleColor;
    public GameObject circleObj;
    public int circleId;

    public void ReactCircle(Vector3 circleScale, float speed, EnumSpace.TEAMCOLOR playerColor, int id)
    {
        circleId = id;
        circleObj.transform.GetComponent<SpriteRenderer>().color = TowerManager.Instance.GetColor(playerColor);
        circleObj.transform.localScale = Vector3.Lerp(Vector3.zero, circleScale, Time.deltaTime * speed);
    }

    public void DeActiveReactCircle(float speed)
    {
        circleId = 0;
        circleObj.transform.localScale = Vector3.Lerp(circleObj.transform.localScale,
                Vector3.zero, Time.deltaTime * speed);

        if(circleObj.transform.localScale == Vector3.zero)
        {
            gameObject.SetActive(false);
        }

    }




}
