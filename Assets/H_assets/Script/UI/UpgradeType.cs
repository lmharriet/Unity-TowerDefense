using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeType : MonoBehaviour
{
    float width;
    float heigh;
    Transform mouse;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        width = spriteRenderer.sprite.rect.width;
        mouse = transform;


    }
    private void OnEnable()
    {
    }
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
       // mouse.position = Input.mousePosition;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.TransformPoint(new Vector3(mousePos.x, mousePos.y, 0));
        Transform rectPos = transform;
        rectPos.TransformPoint(new Vector3(spriteRenderer.sprite.rect.x, spriteRenderer.sprite.rect.y, 0));
        
        Debug.Log( "mouse Pos X : " +mouse.position.x);
        Debug.Log("rect.pos X : " + rectPos.position.x);

        if (mouse.position.x > rectPos.position.x && mouse.position.x < rectPos.position.x + width)
        {
            Debug.Log("I am here");
        }
        //    Debug.Log("");
    }

    //private void OnMouseOver()
    //{
    //    Debug.Log(gameObject.name);
    //}
}
