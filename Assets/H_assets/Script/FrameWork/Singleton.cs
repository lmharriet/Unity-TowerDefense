using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static bool ShuttingDown = false;
    private static object Lock = new object();
    private static T t_Instance;

    public static T Instance
    {
        get
        {
            //게임 종료 시 object 보다 싱글톤의 OnDestroy가 먼저 실행 될 수도 있다.
            //해당 싱글톤을 gameObject.Ondestroy()에서는 사용하지 않거나 사용한다면 null체크를 해주자.

            if (ShuttingDown)
            {
                Debug.Log("'[Singleton] Instance'" + typeof(T) + "'already destroyed. Returning null.'");
                return null;
            }

            lock (Lock)     //Thread safe
            {
                if (t_Instance == null)
                {
                    //인스턴스 존재 여부 확인
                    t_Instance = (T)FindObjectOfType(typeof(T));

                    if (t_Instance == null)
                    {
                        //새로운 게임오브젝트를 만들어서 싱글톤 Attach

                        var singletonObj = new GameObject();
                        t_Instance = singletonObj.AddComponent<T>();
                        singletonObj.name = typeof(T).ToString() + "(singleton)";


                        //make instance persistent.
                        DontDestroyOnLoad(singletonObj);
                    }

                }
                return t_Instance;
            }//end of lock
        }
    }

    private void OnApplicationQuit()
    {
        ShuttingDown = true;
    }
    private void OnDestroy()
    {
        ShuttingDown = true;
    }

}
