using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isWhite;
    // Use this for initialization
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sp;
    cam2D cm;
    ItemData HaveItem;
    GameObject HaveItemInstanse;
    Vector3 ResponePoint;
    public float Speed = 5;
    bool isRight;
    bool ActiveFlag;
    void Awake()
    {
        ResponePoint = this.transform.position;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        cm = Camera.main.GetComponent<cam2D>();
        AnActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveFlag)
        {
            Move();
            SwichColor();
        }
        ReActive();
    }

    void Move()
    {
        TouchInfo ti = AppUtil.GetTouch();
        if (ti == TouchInfo.Began || ti == TouchInfo.Moved || ti == TouchInfo.Stationary)
        {
            Vector3 tp = AppUtil.GetTouchWorldPosition(Camera.main);
            if (Mathf.Abs(tp.x - transform.position.x) > 0.5f)
            {
                if (tp.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-Speed, rb.velocity.y);
                    isRight = false;
                }
                else
                {
                    rb.velocity = new Vector2(Speed, rb.velocity.y);
                    isRight = true;
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            int layerMask = (1 << LayerMask.NameToLayer(LayerMask.LayerToName( this.gameObject.layer - 3))|(1 << LayerMask.NameToLayer("Common")));
            RaycastHit2D Ghit1 = Physics2D.Raycast(transform.position, Vector2.down, col.bounds.size.y ,layerMask);
            if (Ghit1.collider.tag == "floor")
            {
                if (Ghit1.normal.x < 0.65f)
                {
                    if (Ghit1.normal.x > -0.65f && Ghit1.normal.x < 0)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, Vector2.Angle(Ghit1.normal, new Vector2(0, 1)));
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, 0, -Vector2.Angle(Ghit1.normal, new Vector2(0, 1)));
                    }
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
               

            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }


        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKeyDown("x")) {
            ItemRelease();
        }
    }

    void SwichColor() {
        if (Input.GetKeyDown("c") ) {
            WorldColor.ReverseColor();
            AnActive();
        }
    }

    void AnActive()
    {
        rb.simulated = false;
        //col.enabled = false;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        ActiveFlag = false;
    }

    void ReActive() {
        if (!ActiveFlag && WorldColor.isWhite() == isWhite)
        {
            rb.simulated = true;
            //col.enabled = true;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            ActiveFlag = true;
            cm.target = transform;
        }
    }

    void Jump() {
        RaycastHit2D Ghit1 = Physics2D.Raycast(transform.position, Vector2.down, col.bounds.size.y, 1 << 8);
        if(Ghit1.collider != null)
        {
            rb.velocity = new Vector2(0, 15);
        }
        
    }

    void OnTriggerEnter2D( Collider2D a ) {
        if (a.gameObject.tag == "JumpTrigger") {
            Jump();
        }
    }

    void ItemGet( GameObject Item ) {
        ItemData id = Item.GetComponent<ItemData>();
        if (id.GetItem) {
            HaveItem = id;
            HaveItemInstanse = Item;
            Item.SetActive(false);

        }
    }

    void ItemRelease()
    {
        if (HaveItem != null) {
            HaveItemInstanse.SetActive(true);
            float posx = -3;
            if (isRight) {
                posx *= -1;
            }
            HaveItemInstanse.transform.position = new Vector3(posx + this.transform.position.x, this.transform.position.y + 2, this.transform.position.z );
            HaveItem = null;
            HaveItemInstanse = null;
        }
    }

    void OnCollisionEnter2D(Collision2D a) {
        if (a.gameObject.tag == "Dmg") {
            this.transform.position = ResponePoint;
        }
        if (a.gameObject.tag == "item") {
            ItemGet(  a.gameObject );
        }
    }

}