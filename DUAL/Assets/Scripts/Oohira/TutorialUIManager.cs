using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：TutorialstageUIを管理するスクリプト
//
//アタッチ：TutorialstageUIにアタッチ
//※StageUIのアイコンを背景の色（プレイヤーの色）によって変えるスクリプトはStageUIColorChanger.cs
public class TutorialUIManager : MonoBehaviour {
	GameObject[] _keysUI = new GameObject[2];
	Camera2D _camera2D;
	Camera2DToku _camera2DToku;
	GameObject _player;
	GameObject[] _key = new GameObject[2];

	// Use this for initialization
	void Start () {
		//UIの初期化----------------------------------------------
		_keysUI[0] = GameObject.Find ( "KeyButtonWhite" );
		_keysUI[0].SetActive ( false );
		_keysUI [1] = GameObject.Find ("KeyButtonBlack");
		_keysUI[1].SetActive ( false );
		//-------------------------------------------------------
		//playerの初期化-------------------------------------------------
		_camera2D = Camera.main.GetComponent<Camera2D> ();
		_camera2DToku = Camera.main.GetComponent<Camera2DToku> ();
		//---------------------------------------------------------------
		//アイテムの初期化
		_key[0] = ( GameObject )Resources.Load( "Prefabs/white_key" );
		_key[1] = (GameObject)Resources.Load ( "Prefabs/black_key" );
	}
	
	// Update is called once per frame
	void Update () {
		//カメラが注目しているプレイヤーを毎フレーム探す-------------
		if (_camera2D) {
			_player = _camera2D.player;
		}
		if ( _camera2DToku ) {
			_player = _camera2DToku.player;
		}
		//---------------------------------------------------------

		//UIの表示・非表示を管理
		SetKeyUI ();
	}


	//--カギUIを表示関数
	void SetKeyUI( ) {
		Controller2D controller2D = _player.GetComponent<Controller2D> ();
		if (controller2D) {
			if (controller2D.key01) {
				switch (_player.name) {
				case "player":		//白の男の子
					_keysUI [0].SetActive (true);
					break;
				case "player2":	//黒の女の子
					_keysUI [1].SetActive (true);
					break;
				default :
					break;
				}
			} else {
				switch (_player.name) {
				case "player":		//白の男の子
					_keysUI [0].SetActive (false);
					_keysUI [1].SetActive (false);
					break;
				case "player2":	//黒の女の子
					_keysUI [0].SetActive (false);
					_keysUI [1].SetActive (false);
					break;
				default :
					break;
				}
			}
		}
		Controller2DToku controller2DToku = _player.GetComponent<Controller2DToku> ();
		if (controller2DToku) {
			if (controller2DToku.key01) {
				switch (_player.name) {
				case "player":		//白の男の子
					_keysUI [0].SetActive (true);
					break;
				case "player2":	//黒の女の子
					_keysUI [1].SetActive (true);
					break;
				default :
					break;
				}
			} else {
				switch (_player.name) {
				case "player":		//白の男の子
					_keysUI [0].SetActive (false);
					break;
				case "player2":	//黒の女の子
					_keysUI [1].SetActive (false);
					break;
				default :
					break;
				}
			}
		}

	}


	//-------------------------------------------------------------------------------------------------------------------
	//public関数
	//-------------------------------------------------------------------------------------------------------------------
	//--カギを使う関数
	public void UsingKey( ) {
		Animator anim = _player.GetComponent<Animator> ();
		if (anim.GetBool ("face_left")) {
			switch (_player.name) {
			case "player":		//白の男の子
				Instantiate (_key [0], _player.transform.position - transform.right, Quaternion.identity);
				break;
			case "player2":		//黒の女の子
				Instantiate (_key [1], _player.transform.position - transform.right, Quaternion.identity);
				break;
			default :
				break;
			}
		} else {
			switch (_player.name) {
			case "player":		//白の男の子
				Instantiate (_key [0], _player.transform.position + transform.right, Quaternion.identity);
				break;
			case "player2":		//黒の女の子
				Instantiate (_key [1], _player.transform.position + transform.right, Quaternion.identity);
				break;
			default :
				break;
			}
		}
		Controller2D controller2D = _player.GetComponent<Controller2D> ();
		if (controller2D) {
			controller2D.key01 = false;
		}
		Controller2DToku controller2DToku = _player.GetComponent<Controller2DToku> ();
		if (controller2DToku) {
			controller2DToku.key01 = false;
		}
	}
	//-------------------------------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------------------

}
