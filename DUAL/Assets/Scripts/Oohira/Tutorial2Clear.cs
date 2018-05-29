using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：チュートリアルステージ２のクリア機能を付けるスクリプト
//
//アタッチ：クリアのコライダーの付いたゲームオブジェクトにアタッチ
public class Tutorial2Clear : MonoBehaviour {
	GameObject _tutorial2UI;
	GameObject _tutorial2ResultUI;

	// Use this for initialization
	void Start () {
		_tutorial2ResultUI = GameObject.Find ( "Tutorial2ResultUI" );
		if (_tutorial2ResultUI) {
			_tutorial2ResultUI.SetActive (false);
		}
		_tutorial2UI = GameObject.Find ( "Tutorial2UI" );
	}
	
	// Update is called once per frame
	void Update () {
	}


	//--チュートリアルステージ２UIのを表示する関数(OnTriggerEnter2D)
	void OnTriggerEnter2D(Collider2D other ) {
		_tutorial2ResultUI.SetActive (true);
		_tutorial2UI.SetActive (false);
		ResultUIControll._clearTime = 0f;
		ResultUIControll._transitionNumber = 0;
		ResultUIControll._withdrawalNumber = 0;
	}
}
