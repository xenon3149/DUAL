using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：ハサミの機能を持つスクリプト
//
//アタッチ：ハサミにアタッチ
public class Scissors : MonoBehaviour {
	[SerializeField] Camera2D _camera2D;
	float _dontGetTime;		//3秒経つまではプレイヤーをすり抜けるようにする変数
	[SerializeField] GameObject[] _enemies = new GameObject[2];		//ハサミをすり抜けるエネミー
	GameObject[] _scissors = new GameObject[2];		//色違いのハサミを格納する変数(転移したときに色を変える処理を行うために使用)

	// Use this for initialization
	void Start () {
		_camera2D = Camera.main.GetComponent<Camera2D> ();
		_dontGetTime = 3f;
		_scissors [0] = (GameObject)Resources.Load ( "Prefabs/ScissorsWhite" );
		_scissors [1] = (GameObject)Resources.Load ( "Prefabs/ScissorsBlack" );
	}
	
	// Update is called once per frame
	void Update () {
		if (_dontGetTime > 0) {
			_dontGetTime -= Time.deltaTime;
		}
	}


	//---画面外に落ちたら位置をリセットする関数
	void ResetPosition( ) {
		if (transform.position.y < -508f) {
			Destroy (this.gameObject);
			if (GameObject.Find ("StageUI")) {
				GameObject.Find ("StageUI").GetComponent< StageUIManager > ().UsingKey ();
			}
		}
	}


	//--すり抜け中も取れるようにする関数(エネミーが近くにいても取れる)(OnTriggerEnter2D)
	void OnTriggerEnter2D(Collider2D x) {
		if (!_camera2D) return;		//転移時_camera2Dの参照を取る前に関数が呼ばれてDebug.LogErrorか出るのを防ぐ処理
		//プレイヤーの時の処理-------------------------------------------------------------------
		if (x.gameObject == _camera2D.player) {
			Controller2D controller2D = _camera2D.player.GetComponent<Controller2D> ();
			if (_dontGetTime < 0f && controller2D.key01 == false) {
				Destroy (gameObject);
				controller2D.scissors = true;
			}
		}
		//--------------------------------------------------------------------------------------
	}


	//--プレイヤーどうかを判断しハサミを取得する関数(OnCollisionEnter2D)
	void OnCollisionEnter2D( Collision2D x ) {
		BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D> ();
		Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		//プレイヤーの時の処理-------------------------------------------------------------------
		if (x.gameObject == _camera2D.player) {
			Controller2D controller2D = _camera2D.player.GetComponent<Controller2D> ();
			if (_dontGetTime < 0f && controller2D.key01 == false) {
				Destroy (gameObject);
				controller2D.scissors = true;
			} else {								//3秒経つまではプレイヤーをすり抜けるようにする
				boxCollider2D.isTrigger = true;
				rigidbody2D.gravityScale = 0;
			}
		}
		//--------------------------------------------------------------------------------------
		//エネミーの時の処理---------------------------------------------------------------------
		for ( int i = 0; i < _enemies.Length; i++ ) {
			if (x.gameObject == _enemies [i]) {		//エネミーはすり抜けるようにする
				boxCollider2D.isTrigger = true;
				rigidbody2D.gravityScale = 0;
			}
		}
		//---------------------------------------------------------------------------------------

	}


	//--トリガー（すり抜け状態）を解除する関数(OnTriggerExit2D)
	void OnTriggerExit2D( Collider2D x ) {
		BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D> ();
		Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		//エネミーの時の処理---------------------------------------------------------------------
		for ( int i = 0; i < _enemies.Length; i++ ) {
			if (x.gameObject == _enemies [i]) {		//エネミーが離れたときすり抜けを解除する
				boxCollider2D.isTrigger = false;
				rigidbody2D.gravityScale = 1;
			}
		}
		//---------------------------------------------------------------------------------------
		//プレイヤーの時の処理-----------------------------------------------------------------------------------
		if (x.gameObject == _camera2D.player) {			//プレイヤーが離れたときすり抜けを解除する(宙に浮かぶバグを防ぐ)
			boxCollider2D.isTrigger = false;
			rigidbody2D.gravityScale = 1;
		}
		//------------------------------------------------------------------------------------------------------
	}


	//--ドア・壁上に生成してしまったときにリセットする関数(OnCollisionStay2D)
	void OnCollisionStay2D( Collision2D x ) {
		Controller2D controller2D = _camera2D.player.GetComponent<Controller2D> ();
		if (x.gameObject.tag == "door") {	//ドア上に生成してしまって取れなくなるバグ対策
			controller2D.scissors = true;
			Destroy (gameObject);
		}
		if (x.gameObject.name == "WallLeft") {	//左の画面外に生成してカギが宙に浮かんで取れなくなるバグ対策(wallタグで判断するとカエルのいるステージ2-1で不具合が起きるので名前で判定)
			controller2D.scissors = true;
			Destroy (gameObject);
		}
	}

	//--ハサミの色を変える関数(転移の時に使用する)
	public void ChangeScissorsColor( ) {
		if (this.gameObject.name == "ScissorsWhite" || this.gameObject.name == "ScissorsWhite(Clone)") {
			Instantiate (_scissors [1], transform.position + new Vector3 (0, 2, 0), Quaternion.identity);
		} else {
			Instantiate (_scissors [0], transform.position + new Vector3 (0, 2, 0), Quaternion.identity);
		}
		Destroy (this.gameObject);
	}
}
