using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEffect : MonoBehaviour
{

    public Color circleColor;
    private Vector3 circleScale;
    public int circleId;
    public bool isActive;
    public float speed;

    private void Update()
    {
        //if (isActive)
        //{
        //    transform.localScale = circleScale;//Vector3.Lerp(Vector3.zero, circleScale, Time.deltaTime * speed);
        //}
        //else
        //{
        //    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * speed);

        //    if (transform.localScale == Vector3.zero)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}
    }
    public void ActiveCircle(float lerpSpeed, EnumSpace.TEAMCOLOR playerColor, int id)
    {
        circleScale = transform.localScale;
        isActive = true;
        circleId = id;
        circleColor = TowerManager.Instance.GetColor(playerColor);
        transform.GetComponent<SpriteRenderer>().color = circleColor;
        speed = lerpSpeed;

    }

    public void DeActiveCircle(float lerpSpeed)
    {
        isActive = false;
        circleId = -1;
        speed = lerpSpeed;

    }

}
