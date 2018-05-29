using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitch : MonoBehaviour {

	Animator animator;
	public GameObject Door;
	public GameObject Door2;
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
			animator.SetBool("open", true);
			Door.GetComponent<door>().door_open = true;
			Door2.GetComponent<door> ().door_open = true;
	}

	void OnTriggerExit2D(Collider2D othen)
	{
		if (Loop)
		{
			if (othen.gameObject.tag == "Player" || othen.gameObject.tag == "item")
			{
				animator.SetBool("open", false);
				Door.GetComponent<door>().door_open = false;
				Door2.GetComponent<door>().door_open = false;
			}
		}
	}

}
