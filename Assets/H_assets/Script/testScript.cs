using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;

public class testScript : MonoBehaviour
{
    //float x, z;
    //float speed = 4f;

    public int unitcount;
    private float delay;

    public int count;
    // Start is called before the first frame update
    async void Start()
    {
        //Debug.LogFormat("현재 쓰레드 : {0}", Thread.CurrentThread.ManagedThreadId);
        //Debug.LogFormat("현재 프레임 : {0}", Time.frameCount);

        Debug.Log("run() invoke in start()");
        await testAsync();
        Debug.Log("Run() returns");

        //Debug.LogFormat("현재 unitCount : {0}", unitcount);
    }
    private void Update()
    {
        Debug.Log("update() 실행 중");
    }

    async Task testAsync()
    {
        int result = 0;
        await Task.Run(() =>
        {

            for (int i = 0; i < count; i++)
            {
                Debug.Log(i);
                result += i;
               // Thread.Sleep(1000);
              
            }
        });

        Debug.Log("result : " + result);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    x = Input.GetAxis("Horizontal");
    //    z = Input.GetAxis("Vertical"); 

    //    transform.Translate(new Vector3(x,0,z) * Time.deltaTime * speed);

    //}
}
