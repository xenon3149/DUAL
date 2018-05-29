using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour {


    Animator animator;
    public bool door_open = false;
    public bool Loop = false;
    public GameObject Event_GameObj;
    public GameObject door_image;
    public string doornumber = "01";
    Collider2D _Collider;
    



    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        _Collider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Loop)
        {
            if (door_open)
            {
                if (door_image != null)
                {
                    door_image.GetComponent<door>().door_open = true;
                }
                animator.SetBool("door", true);
                _Collider.enabled = false;
            }
            else
            {
                if (door_image != null)
                {
                    door_image.GetComponent<door>().door_open = false;
                }
                animator.SetBool("door", false);
                _Collider.enabled = true;
            }
        }
        else
        {
            if (door_open)
            {
                if (door_image != null)
                {
                    door_image.GetComponent<door>().door_open = true;
                }
                animator.SetBool("door", true);
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                if (Event_GameObj != null)
                {
                    Event_GameObj.SetActive(true);
                }
            }
        }
	}
}
