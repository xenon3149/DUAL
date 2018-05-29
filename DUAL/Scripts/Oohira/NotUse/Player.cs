using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}



	//キーボード入力でPlayerを動かす関数
	void move() {
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.AddForce (new Vector2 (10, 0));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.AddForce (new Vector2 (-10, 0));
		}
	}

}
