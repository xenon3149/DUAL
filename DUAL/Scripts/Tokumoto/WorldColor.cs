using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColor {

    static bool White = false;

    static public bool isWhite() {
        return White;
    }

    static public void SetWhite() {
        White = true;
    }
    static public void SetBlack()
    {
        White = false;
    }
    static public void ReverseColor()
    {
        White = !White;
    }
}
