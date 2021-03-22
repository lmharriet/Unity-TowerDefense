using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    int randomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).Rotate(0, -25 * randomSpeed * Time.deltaTime, 0);

        transform.Translate(Vector3.left * randomSpeed * Time.deltaTime);
    }
}
