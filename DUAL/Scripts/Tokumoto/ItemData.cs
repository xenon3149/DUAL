using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour {
    public bool isWhite;
    public ItemType Type;
    public bool GetItem;
    public bool TransportItem;
    public enum ItemType
    {
        Key,
        JumpTable,
        BigBox,
        SmallBox
    };
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sp;
    bool ActiveFlag;
    bool TransportFlag;
    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        AnActive();
    }

    // Update is called once per frame
    void Update()
    {
        ReActive();
    }

    void AnActive()
    {
        rb.simulated = false;
        col.enabled = false;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        ActiveFlag = false;
    }

    void ReActive()
    {
        if (!ActiveFlag && WorldColor.isWhite() == isWhite )
        {
            rb.simulated = true;
            col.enabled = true;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            ActiveFlag = true;
            if( TransportFlag)
            {
                GameObject a = null;
                if (!isWhite)
                {
                    a = Resources.Load("Sprite/white") as GameObject;
                }
                else
                {
                    a = Resources.Load("Sprite/black") as GameObject;
                }
                GameObject b = Instantiate(a);
                b.transform.position = transform.position;
                StartCoroutine(SpriteDelete(b));
                TransportFlag = false;
            }
        }
        if (ActiveFlag && WorldColor.isWhite() != isWhite)
        {
            AnActive();
        }
    }

    public void Transport() {
        TransportFlag = true;
        isWhite = !isWhite;
    }

    IEnumerator SpriteDelete( GameObject a ) {
        yield return new WaitForSeconds(1);
        if (a != null)
        {
            Destroy(a);
        } 
    }

}
