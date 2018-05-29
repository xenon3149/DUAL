using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    GameObject player;
    public string keynumber = "01";

	GameObject knight_white;
	public float _dontGetTime;

    // Use this for initialization
    void Start()
    {
		knight_white = GameObject.Find ( "knight_white" );
		_dontGetTime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Main Camera").GetComponent<Camera2D>().player;
		_dontGetTime -= Time.deltaTime;

		if (transform.position.y < -508f) {
			Destroy (this.gameObject);
			if (GameObject.Find ("StageUI")) {
				GameObject.Find ("StageUI").GetComponent< StageUIManager > ().UsingKey ();
			}
			if (GameObject.Find ("Tutorial3UI")) {
				GameObject.Find ("Tutorial3UI").GetComponent< TutorialUIManager > ().UsingKey ();
			}
		}
    }


	void OnTriggerEnter2D(Collider2D order) {		//すり抜け中も取れるようにする(騎士が近くにいても取れる)
		if (order.gameObject == player ) {
			if (_dontGetTime < 0f && player.GetComponent<Controller2D>().scissors == false) {
				Destroy (gameObject);
				player.GetComponent<Controller2D> ().key01 = true;
			}
		}
    }
    

	void OnCollisionEnter2D( Collision2D order ) {
		
		if (order.gameObject == player) {
			if (_dontGetTime < 0f && player.GetComponent<Controller2D>().scissors == false) {
				player.GetComponent<Controller2D> ().key01 = true;
				Destroy (gameObject);
			} else {								//3秒経つまではプレイヤーをすり抜けるようにする
				gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
				gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
			}
		}
		if (order.gameObject == knight_white) {		//騎士はすり抜けるようにする
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		}

	}

	void OnTriggerExit2D( Collider2D order ) {
		if (order.gameObject == knight_white) {		//騎士が離れたときすり抜けを解除する
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
		if (order.gameObject == player) {			//プレイヤーが離れたときすり抜けを解除する(宙に浮かぶバグを防ぐ)
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
	}

	void OnCollisionStay2D( Collision2D order ) {
		if (order.gameObject.tag == "door") {	//ドア上に生成してしまって取れなくなるバグ対策
			player.GetComponent<Controller2D> ().key01 = true;
			Destroy (gameObject);
		}
		if (order.gameObject.name == "WallLeft") {	//左の画面外に生成してカギが宙に浮かんで取れなくなるバグ対策(wallタグで判断するとカエルのいるステージ2-1で不具合が起きるので名前で判定)
			player.GetComponent<Controller2D> ().key01 = true;
			Destroy (gameObject);
		}
	}
}
