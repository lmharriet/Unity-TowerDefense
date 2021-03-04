using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumSpace
{
    public enum TEAMCOLOR
    {
        NONE, RED, YELLOW, BLUE, GREEN
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

public class GlobalDefine
{
    public static Dictionary<EnumSpace.TEAMCOLOR, Color> colorDictionary;

}
