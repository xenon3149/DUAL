using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//機能：イバラの機能を持つスクリプト
//
//アタッチ：イバラにアタッチ
public class Ivy : MonoBehaviour {
	const int RECOIL_POWER = 6000;		//吹っ飛ばし力

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter2D( Collision2D col ) {
		if (col.gameObject.tag == "Player") {
			var player = col.gameObject.GetComponent<Controller2D> ();
			if (player.scissors) {	//ハサミを持っているとイバラが消える
				Destroy (this.gameObject);
			} else {	//ハサミを持っていないと吹っ飛ばされる
				player.move = false;
				player.jump = true;
				float vec_x = Mathf.Sign ( col.transform.position.x - transform.position.x );
				Vector3 recoil = new Vector3 (vec_x, 1f, 0f);
				col.gameObject.GetComponent<Rigidbody2D> ().AddForce ( recoil * RECOIL_POWER );	
			}
		}
	}

}
