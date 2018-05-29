using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HPcont : MonoBehaviour {

    Text tx;
	public GameObject _gameOverUI;
	public bool _gameOverFlag;		//timeScaleの処理を一回のみ行うための変数(Updateで毎フレーム行うとボタンの処理後もtimeScaleの処理を行ってしまうため

	// Use this for initialization
	void Start () {
        tx = GetComponent<Text>();
		ResultUIControll._withdrawalNumber = 0;		//_withdrawalNumberはstaticなのでここで初期化。シーン再読み込み前にやるとダンゴムシの挙動がおかしくなる
		_gameOverFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        tx.text = ( 5 - ResultUIControll._withdrawalNumber).ToString();
		if ( int.Parse(tx.text) <= 0 && !_gameOverFlag ) {
			_gameOverUI.SetActive (true);
			Time.timeScale = 0;
			_gameOverFlag = true;
		}

	}
}
