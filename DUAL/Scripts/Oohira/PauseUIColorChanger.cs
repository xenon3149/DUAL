using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：PauseUIのアイコンを背景の色（プレイヤーの色）によって変えるスクリプト
//
//アタッチ：PauseUIにアタッチ
public class PauseUIColorChanger : MonoBehaviour {
	[SerializeField] Image _startUI = null;
	[SerializeField] Image _optionUI = null;
	[SerializeField] Sprite[] _startUISprites = new Sprite[2];
	[SerializeField] Sprite[] _optionUISprites = new Sprite[2];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ChangeColor ();
	}

	void ChangeColor( ) {
		if (Camera.main.GetComponent<Camera2D> ()) {
			if (Camera.main.GetComponent<Camera2D> ().player.name == "player") {	//黒の世界の時
				_startUI.sprite = _startUISprites [0];
				_optionUI.sprite = _optionUISprites [0];
			} else {																//白の世界の時
				_startUI.sprite = _startUISprites [1];
				_optionUI.sprite = _optionUISprites [1];
			}
		}
		if (Camera.main.GetComponent<Camera2DToku> ()) {
			if (Camera.main.GetComponent<Camera2DToku> ().player.name == "player") {	//黒の世界の時
				_startUI.sprite = _startUISprites [0];
				_optionUI.sprite = _optionUISprites [0];
			} else {																//白の世界の時
				_startUI.sprite = _startUISprites [1];
				_optionUI.sprite = _optionUISprites [1];
			}
		}
	}
}
