using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumSpace
{
    public enum TEAMCOLOR
    {
        NONE, RED,  YELLOW, BLUE, GREEN
    }

    public enum TOWERKIND
    {
        TOWN, DEFENSE, FACTORY
    }

    public enum UPGRADE
    {
        LEVEL_UP, TO_TOWER, TO_FACTORY
    }
}

public class GlobalDefine : MonoBehaviour
{
    public Color color;

    public static Dictionary<EnumSpace.TEAMCOLOR, Color> colorDictionary = new Dictionary<EnumSpace.TEAMCOLOR, Color>();
    private void Awake()
    {
        SetColor();
    }
    public void SetColor()
    {

        color = Color.white;
        colorDictionary.Add(EnumSpace.TEAMCOLOR.NONE, color);
        color = Color.red;
        colorDictionary.Add(EnumSpace.TEAMCOLOR.RED, color);
        color = Color.yellow;
        colorDictionary.Add(EnumSpace.TEAMCOLOR.YELLOW, color);
        color = Color.blue;
        colorDictionary.Add(EnumSpace.TEAMCOLOR.BLUE, color);
        color = Color.green;
        colorDictionary.Add(EnumSpace.TEAMCOLOR.GREEN, color);
    }
}
