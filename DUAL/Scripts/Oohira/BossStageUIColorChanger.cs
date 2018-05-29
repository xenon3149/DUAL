using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：BossStageUIのアイコンを背景の色（プレイヤーの色）によって変えるスクリプト
//
//アタッチ：BossStageUIにアタッチ
public class BossStageUIColorChanger : MonoBehaviour {
	[SerializeField] Image _itemUI = null;
	[SerializeField] Image _pauseUI = null;
	[SerializeField] Image _timebarUI = null;
	[SerializeField] Image _timeUI = null;
	[SerializeField] Text _timeText = null;
	[SerializeField] Image _lifeUI = null;
	[SerializeField] Text _lifeText = null;
	[SerializeField] Sprite[] _itemUISprites = new Sprite[2];
	[SerializeField] Sprite[] _pauseUISprites = new Sprite[2];
	[SerializeField] Sprite[] _timebarUISprites = new Sprite[2];
	[SerializeField] Sprite[] _timeUISprites = new Sprite[2];
	[SerializeField] Sprite[] _lifeUISprites = new Sprite[2];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ChangeColor ();
	}



	//--色を変える関数
	void ChangeColor( ) {
		if ( Camera.main.GetComponent<Camera2D>() ) {
			if ( Camera.main.GetComponent<Camera2D>().player.name == "player" ) {	//黒の世界の時
				_itemUI.sprite = _itemUISprites[0];
				_pauseUI.sprite = _pauseUISprites [0];
				_timebarUI.sprite = _timebarUISprites [0];
				_timeUI.sprite = _timeUISprites [0];
				_timeText.color = new Color (1f, 1f, 1f);
				_lifeUI.sprite = _lifeUISprites [0];
				_lifeText.color = new Color (1f, 1f, 1f);
			} else {																//白の世界の時
				_itemUI.sprite = _itemUISprites[1];
				_pauseUI.sprite = _pauseUISprites [1];
				_timebarUI.sprite = _timebarUISprites [1];
				_timeUI.sprite = _timeUISprites [1];
				_timeText.color = new Color (0f, 0f, 0f);
				_lifeUI.sprite = _lifeUISprites [1];
				_lifeText.color = new Color (0f, 0f, 0f);
			}
		}
		if ( Camera.main.GetComponent<Camera2DToku>() ) {
			if ( Camera.main.GetComponent<Camera2DToku>().player.name == "player" ) {	//黒の世界の時
				_itemUI.sprite = _itemUISprites[0];
				_pauseUI.sprite = _pauseUISprites [0];
				_timebarUI.sprite = _timebarUISprites [0];
				_timeUI.sprite = _timeUISprites [0];
				_timeText.color = new Color (1f, 1f, 1f);
				_lifeUI.sprite = _lifeUISprites [0];
				_lifeText.color = new Color (1f, 1f, 1f);
			} else {																//白の世界の時
				_itemUI.sprite = _itemUISprites[1];
				_pauseUI.sprite = _pauseUISprites [1];
				_timebarUI.sprite = _timebarUISprites [1];
				_timeUI.sprite = _timeUISprites [1];
				_timeText.color = new Color (0f, 0f, 0f);
				_lifeUI.sprite = _lifeUISprites [1];
				_lifeText.color = new Color (0f, 0f, 0f);
			}
		}
	}
}