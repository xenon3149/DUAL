using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：ボタンをタップしたら他のボタンを反応しなくなるスクリプト
//
//アタッチ：全てのボタンの親のゲームオブジェクトにアタッチ
public class ButtonEnable : MonoBehaviour {
	[SerializeField] Button[] _buttons;

	// Use this for initialization
	void Start () {
		_buttons = GetComponentsInChildren<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//-----------------------------------------------------
	//public関数
	//-----------------------------------------------------
	//--ボタンをタップしたら他のボタンが反応しなくする関数
	public void ButtonTap( ) {
		for (int i = 0; i < _buttons.Length; i++) {
			if (_buttons [i].IsActive ()) {
				_buttons [i].enabled = false;
			} else {
				_buttons [i].enabled = true;
			}
		}
	}
	//------------------------------------------------------
	//------------------------------------------------------


}
