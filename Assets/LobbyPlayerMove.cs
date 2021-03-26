using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LobbyPlayerMove : MonoBehaviour
{

    public EnumSpace.GAMESTATE game;
    private Vector3 startPos;
    //public GameObject startTower;
    //public GameObject endTower;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetSceneName() { return sceneName; }
    
   
}
