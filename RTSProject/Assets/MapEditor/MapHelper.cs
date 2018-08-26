using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapHelper {
    public static int StringToInt(string toInt)
    {
        int number;
        int.TryParse(toInt, out number);
        return number;
    }

    public static float StringToFloat(string toFloat)
    {
        float ret;
        float.TryParse(toFloat, out ret);
        return ret;
    }

    public static bool CheckForNumeric(string stringToCheck)
    {
        int number;
        return int.TryParse(stringToCheck, out number);
    }
}
