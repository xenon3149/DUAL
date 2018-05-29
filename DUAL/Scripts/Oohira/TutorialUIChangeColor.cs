using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：TutorialStageUIのアイコンを背景の色（プレイヤーの色）によって変えるスクリプト
//
//アタッチ：TutorialStageUIにアタッチ
public class TutorialUIChangeColor : MonoBehaviour {
	[SerializeField] Image _itemUI = null;
	[SerializeField] Image _pauseUI = null;
	[SerializeField] Sprite[] _itemUISprites = new Sprite[2];
	[SerializeField] Sprite[] _pauseUISprites = new Sprite[2];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ChangeColor ();
	}


	//--色を変える関数
	void ChangeColor( ) {
		Camera2D camera2D = Camera.main.GetComponent<Camera2D> ();
		if ( camera2D ) {
			if ( camera2D.player.name == "player" ) {	//黒の世界の時
				_itemUI.sprite = _itemUISprites[0];
				_pauseUI.sprite = _pauseUISprites [0];
			} else {									//白の世界の時
				_itemUI.sprite = _itemUISprites[1];
				_pauseUI.sprite = _pauseUISprites [1];
			}
		}
		Camera2DToku camera2DToku = Camera.main.GetComponent<Camera2DToku> ();
		if ( camera2DToku ) {
			if ( camera2DToku.player.name == "player" ) {	//黒の世界の時
				_itemUI.sprite = _itemUISprites[0];
				_pauseUI.sprite = _pauseUISprites [0];
			} else {										//白の世界の時
				_itemUI.sprite = _itemUISprites[1];
				_pauseUI.sprite = _pauseUISprites [1];
			}
		}
	}

}
