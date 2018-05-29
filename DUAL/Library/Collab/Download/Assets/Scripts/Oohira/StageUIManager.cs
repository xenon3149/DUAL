using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour {

	public Image _keyUI_Image;
	public Image _box_Image;

	GameObject _camera;
	public GameObject _player;
	public Animator _anim;

	public GameObject _key;
	public GameObject _box;

	// Use this for initialization
	void Start () {
		//UIの初期化
		_keyUI_Image = GameObject.Find ( "KeyButton" ).GetComponent<Image>( );
		_keyUI_Image.gameObject.SetActive ( false );
		if (GameObject.Find ("BoxButton")) {	//チュートリアルステージではKeyButtonのみ使用するのでif文で囲った
			_box_Image = GameObject.Find ("BoxButton").GetComponent<Image> ();
			_box_Image.gameObject.SetActive (false);
		}

		//playerの初期化
		_camera = GameObject.Find ( "Main Camera" );
	//	_player = _camera.GetComponent<Camera2D>().player;
	//	_anim = _player.GetComponent< Animator > ();
	
		//アイテムの初期化
		_key = ( GameObject )Resources.Load( "Prefab/white_key" );
		_box = (GameObject)Resources.Load ("Items/BlackKey");
	}
	
	// Update is called once per frame
	void Update () {
		//カメラが注目しているプレイヤーを毎フレーム探す
		if (_camera.GetComponent<Camera2D> ()) {
			_player = _camera.GetComponent<Camera2D> ().player;
		}
		if ( _camera.GetComponent<Camera2DToku> () ) {
			_player = _camera.GetComponent<Camera2DToku> ().player;
		}
		_anim = _player.GetComponent<Animator> ();

		//UIの表示・非表示を管理
		SetKeyUI ();
		//SetBoxUI ();
	}



	//今後アイテムが増える場合GetItem,UsingItemとして汎用性のある関数として書き換えた方がいいかも
	void SetKeyUI( ) {
		if (_player.GetComponent<Controller2D> ()) {
			if (_player.GetComponent<Controller2D> ().key01) {
				_keyUI_Image.gameObject.SetActive (true);
			} else {
				_keyUI_Image.gameObject.SetActive (false);
			}
		}
		if (_player.GetComponent<Controller2DToku> ()) {
			if (_player.GetComponent<Controller2DToku> ().key01) {
				_keyUI_Image.gameObject.SetActive (true);
			} else {
				_keyUI_Image.gameObject.SetActive (false);
			}
		}

	}

	/*
	void SetBoxUI( ) {
		if (_player.GetComponent<Controller2DToku> ().BoxFlag) {
			_box_Image.gameObject.SetActive (true);
		} else {
			_box_Image.gameObject.SetActive (false);
		}
	}
*/

	public void UsingKey( ) {
		if (_anim.GetBool ("face_left")) {
			Instantiate (_key, _player.transform.position - transform.right, Quaternion.identity);
		} else {
			Instantiate (_key, _player.transform.position + transform.right, Quaternion.identity);
		}
		if (_player.GetComponent<Controller2D> ()) {
			_player.GetComponent<Controller2D> ().key01 = false;
		}
		if (_player.GetComponent<Controller2DToku> ()) {
			_player.GetComponent<Controller2DToku> ().key01 = false;
		}
	}

	/*
	public void UsingBox( ) {
		if (_anim.GetBool ("face_left")) {
			Instantiate (_box, _player.transform.position - transform.right, Quaternion.identity);
		} else {
			Instantiate (_box, _player.transform.position + transform.right, Quaternion.identity);
		}
		_player.GetComponent<Controller2DToku> ().BoxFlag = false;
	}*/
}
