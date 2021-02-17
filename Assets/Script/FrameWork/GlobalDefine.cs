using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumSpace
{
    public enum TEAMCOLOR
    {
        NONE, RED, ORANGE, BLUE, GREEN
    }
}

public class GlobalDefine
{
    public static Dictionary<EnumSpace.TEAMCOLOR, Color> teamColorDictionary;

}
