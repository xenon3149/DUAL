using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：音楽を管理するスクリプト
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class SoundManager : MonoBehaviour {
	[SerializeField] GameObject _main;
	[SerializeField] GameObject _sub;
	[SerializeField] AudioSource _main_BGM;
	[SerializeField] AudioSource _sub_BGM;
	[SerializeField] GameObject _SE;

	[SerializeField] GameObject _stage;
	[SerializeField] AudioSource _stage_BGM;

	[SerializeField] bool SEChange = true;					//SEを使うかどうかのフラグ

	// Use this for initialization
	void Start () {
		//BGMの初期化-----------------------------------------------
		_main = GameObject.FindWithTag ( "MainBGM" );
		_sub = GameObject.FindWithTag ( "SubBGM" );
		_stage = GameObject.FindWithTag ( "StageBGM" );
		if (_main) {
			_main_BGM = _main.GetComponent<AudioSource> ();
		}
		if (_sub) {
			_sub_BGM = _sub.GetComponent<AudioSource> ();
		}
		if (_stage) {
			_stage_BGM = _stage.GetComponent<AudioSource> ();
		}
		//----------------------------------------------------------

		//BGMのセッティング------------------------------------------------------------------
		if (_main_BGM) {	//_stageBGM > _mainBGM > _subBGMの優先度でAudioSourceを残す処理
			if (_sub_BGM) {
				Destroy (_sub_BGM.gameObject);
			} else if (_stage_BGM) {
				Destroy (_main_BGM.gameObject);
			} else {
				Debug.Log ("SoundManagerの例外が起きました！Scriptを修正してください。");
			}
		} else {
			if (_sub_BGM) {	//_subBGMを_mainBGMに昇格する処理
				_sub_BGM.gameObject.tag = "MainBGM";
				_main_BGM = _sub_BGM;
				DontDestroyOnLoad (_main_BGM.gameObject);
			} else if (_stage_BGM) {
				;
			} else {
				Debug.Log ("SoundManagerの例外が起きました！Scriptを修正してください。");
			}
		}
		//------------------------------------------------------------------------------------
			
		//SEの初期化---------------------
		_SE = GameObject.Find( "SE" );
		//-------------------------------
	}
	
	// Update is called once per frame
	void Update () {
		if (SEChange) {
			_SE.SetActive (true);
		} else {
			_SE.SetActive (false);
		}
	}


}
