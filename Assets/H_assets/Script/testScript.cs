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
        unitcount = 1;
        //Debug.LogFormat("현재 쓰레드 : {0}", Thread.CurrentThread.ManagedThreadId);
        //Debug.LogFormat("현재 프레임 : {0}", Time.frameCount);

        unitcount+= await testAsync();
        Debug.LogFormat("현재 unitCount : {0}", unitcount);
    }
    private void Update()
    {
        Debug.Log("update() 실행 중");
    }

    async Task<int> testAsync()
    {
        //Debug.Log("현재 testAsync");
        //await Task.Delay(3000);
        //Debug.Log("3초 기다림");

        //return 7;

        //await Task.Run(() =>
        //{

        //    Debug.LogFormat("현재 unitCount : {0}", unitcount);
        //    // Debug.LogFormat("현재 쓰레드 : {0}", Thread.CurrentThread.ManagedThreadId);
        //});

        int units = 0;
        await Task.Run(() =>
        {
            for (int i = 0; i < count; i++)
            {
                units += i;
                Thread.Sleep(1000);
            }
        });

        Debug.Log("units : " + units);
        return units;

    }

    //// Update is called once per frame
    //void Update()
    //{
    //    x = Input.GetAxis("Horizontal");
    //    z = Input.GetAxis("Vertical"); 

    //    transform.Translate(new Vector3(x,0,z) * Time.deltaTime * speed);

    //}
}
