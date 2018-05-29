using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager2 : MonoBehaviour {

	public  Image _keyUI_Image;

	GameObject _camera;
	public GameObject _player;
	public Animator _anim;
	public GameObject _key;
    Controller2DToku Cont;
	// Use this for initialization
	void Start () {
		//UIの初期化
		_keyUI_Image = GameObject.Find ( "KeyButton" ).GetComponent<Image>( );
		_keyUI_Image.gameObject.SetActive ( false );

		//playerの初期化
		_camera = GameObject.Find ( "Main Camera" );
	    _player = _camera.GetComponent<Camera2DToku>().player;
		if ( _player ) {
			_anim = _player.GetComponent< Animator > ();
		}
		if (_player) {
			Cont = _player.GetComponent<Controller2DToku> ();
		}
		//アイテムの初期化
		_key = ( GameObject )Resources.Load( "Prefab/white_key" );
	}
	
	// Update is called once per frame
	void Update () {
		_player = _camera.GetComponent<Camera2DToku>().player;
        _anim = _player.GetComponent<Animator>();
		Cont = _player.GetComponent<Controller2DToku>();
        ItemSet();
	}



	//今後アイテムが増える場合GetItem,UsingItemとして汎用性のある関数として書き換えた方がいいかも
	void SetKeyUI( ) {
		if (_player.GetComponent<Controller2DToku> ().key01) {
			_keyUI_Image.gameObject.SetActive (true);
		} else {
			_keyUI_Image.gameObject.SetActive (false);
		}
	}

	public void UsingKey( ) {
		if (_anim.GetBool ("face_left")) {
			Instantiate (_key, _player.transform.position - transform.right, Quaternion.identity);
		} else {
			Instantiate (_key, _player.transform.position + transform.right, Quaternion.identity);
		}
		_player.GetComponent<Controller2DToku> ().key01 = false;
	}

    public void ItemSet( ) {

        if (Cont.HaveItemInstanse != null&& !_keyUI_Image.gameObject.activeSelf ) {      
            _keyUI_Image.gameObject.SetActive(true);
            _keyUI_Image.sprite = Cont.HaveItemInstanse.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ItemReset() {
        _keyUI_Image.gameObject.SetActive(false);
    }

}
