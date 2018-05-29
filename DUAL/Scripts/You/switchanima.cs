using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchanima : MonoBehaviour {

    Animator animator;
    public GameObject Door;
    public bool Loop = false;//

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D othen)
    {
        if (othen.gameObject.tag == "Player" || othen.gameObject.tag == "Frog")
        {
            animator.SetBool("open", true);
            Door.GetComponent<door>().door_open = true;
        }
    }

    void OnTriggerExit2D(Collider2D othen)
    {
        if (Loop)
        {
            if (othen.gameObject.tag == "Player" || othen.gameObject.tag == "Frog")
            {
                animator.SetBool("open", false);
                Door.GetComponent<door>().door_open = false;
            }
        }
    }

}
