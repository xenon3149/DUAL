using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：UIをアクティブ・非アクティブにするスクリプト
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class UISetActive : MonoBehaviour {
	GameObject _UI;
	GameObject _pauseUI;


	// Use this for initialization
	void Start () {
		_UI = GameObject.Find ("Canvas");
		if (_UI) {
			_UI.SetActive (false);
		}
		_pauseUI = GameObject.Find ( "PauseUI" );
		if (_pauseUI) {
			_pauseUI.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void SetActive() {
		_UI.SetActive (true);
	}

	void ResetActive() {
		_UI.SetActive (false);
	}


	//------------------------------------------------------------------------
	//public関数
	//------------------------------------------------------------------------
	//SetActive,ResetActive関数を他のUIに使えるようにオーバーライド
	public void SetActive( GameObject x ) {
		x.SetActive (true);
	}

	public void ResetActive( GameObject x ) {
		x.SetActive (false);
	}
	//-------------------------------------------------------------------------
	//-------------------------------------------------------------------------


}
