using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : Singleton<SceneTest>
{
    protected SceneTest() { }

    public void print()
    {
        Debug.Log("hey!");
    }
}