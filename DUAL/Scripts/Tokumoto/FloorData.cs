using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorData : MonoBehaviour {
    public bool isWhite;
    public bool DoubleColor;
    Collider2D col;
    SpriteRenderer sp;
    bool ActiveFlag;
    Material White;
    Material Black;
    void Awake()
    {
        White = Resources.Load("Material/FloorWhite") as Material;
        Black = Resources.Load("Material/FloorBlack") as Material;
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        AnActive();
    }
	void Update () {
        ReActive();
    }
    void AnActive()
    {
        //col.enabled = false;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        ActiveFlag = false;
    }

    void ReActive()
    {
        if ((!ActiveFlag && WorldColor.isWhite() != isWhite) || DoubleColor)
        {
            //col.enabled = true;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            if (DoubleColor)
            {
                if (!WorldColor.isWhite())
                {
                    sp.material = White;
                }
                else
                {
                    sp.material = Black;
                }
            }
            ActiveFlag = true;
        }
        if (ActiveFlag && WorldColor.isWhite() == isWhite && !DoubleColor) {
            AnActive();
        }
    }
}
