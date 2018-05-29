using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：チュートリアルステージ２のテキスト表示機能
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class TutorialTextControll : MonoBehaviour {
	const int WORDS = 100;										//一文の最大文字数
	const int SENTENCE_NUMBER = 6;								//文章の数
	const int CHAR_SLOW_RATE = 2;								//文字を表示する速さ

	GameObject _player_white;									//白のプレイヤー（黒の世界）
	Camera2D _camera2D;
	Text _tutorialText;											//文章を表示するテキスト
	struct Sentence {
		public char[] _string;
	};
	Sentence[] _sentence = new Sentence[ SENTENCE_NUMBER ];		//表示する文章
	
	bool _nextSentenceDisplayFlag;								//次の文章を表示させるかどうかのフラグ
	bool _chageWorldFlag;										//世界転移時に一度だけ行う処理に使う変数(true:処理前、false:処理後)
	int _readedSentenceCount = 0;								//表示した最後の文章の配列番号
	int i = 0;													//現在の表示文字数
	//	[SerializeField] float _time;							//時間による表示に使う変数


	// Use this for initialization
	void Start () {
		_player_white = GameObject.Find ("player");
		_camera2D = Camera.main.GetComponent<Camera2D> ();

		if (GameObject.Find ("TutorialText")) {
			_tutorialText = GameObject.Find ("TutorialText").GetComponent<Text> ();
			_tutorialText.text = "";
		}
	
		_sentence[0]._string = "この世界には主人公の敵が存在します。".ToCharArray();
		_sentence[1]._string = "しかしそれらは主人公だけの力では倒せません。".ToCharArray ();
		_sentence [2]._string = "このままでは進めませんが、道は一つではありません。\n画面をズームしてください。".ToCharArray ();
		_sentence [3]._string = "敵にぶつかったり攻撃されると、ステージの\n最初まで戻されます。\n主人公たちが倒れることはありません。".ToCharArray ();
		_sentence [4]._string = "画面をズームしてください。".ToCharArray ();
		_sentence [5]._string = "この世界は２つの世界に分かれています。\nあなたは２人の主人公を使い分けて先に進みましょう。".ToCharArray ();

		_nextSentenceDisplayFlag = true;
		_chageWorldFlag = true;
		if (ResultUIControll._withdrawalNumber > 0) {
			_readedSentenceCount = 3;
		}
		_camera2D.World_Lock ();	//転移できないようにする


		//_time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		//DisplayForTime ();
		Display();
	}


//	//--時間によって表示する関数
//	void DisplayForTime() {
//		_time += Time.deltaTime;
//		if (GameObject.Find ("Main Camera").GetComponent<Camera2D> ().player == _player_white) {
//			if (_time >= 1f && _time <= 4f) {
//				_tutorialText.text = "この世界には主人公の敵が存在します。";
//			} else if (_time >= 5f && _time <= 8f) {
//				_tutorialText.text = "しかしそれらは主人公だけの力では倒せません。";
//			} else if (_time >= 9f && _time <= 12f) {
//				_tutorialText.text = "このままでは進めませんが、道は一つではありません。\n画面をズームしてください。";
//			} else if (_time >= 13f && _time <= 16f) {
//				_tutorialText.fontSize = 30;
//				_tutorialText.text = "画面をズームしてください。";
//			} else if (_time >= 17f) {
//				_tutorialText.fontSize = 40;
//				_tutorialText.text = "画面をズームして!!";
//				if (_time >= 19f) {
//					_time = 16.1f;
//				}
//			} else {
//				_tutorialText.text = "";
//			}
//		} else {
//			if (_chageWorldFlag) {
//				_time = 0f;
//				_tutorialText.color = new Color (0f, 0, 0);
//				_chageWorldFlag = false;
//			}
//			if (_time >= 1f) {
//				_tutorialText.fontSize = 14;
//				_tutorialText.text = "この世界は２つの世界に分かれています。\nあなたは２人の主人公を使い分けて先に進みましょう。";
//			} else {
//				_tutorialText.text = "";
//			}
//		}
//	}



	//--タップによって表示する関数
	void Display() {
		//黒の世界の処理--------------------------------------------------------------------------------------------------------------------
		if (_camera2D.player == _player_white) {
			_tutorialText.color = new Color (255f, 255f, 255f);
			//死んだ時の処理-----------------------------------------------------------------------------------------------
			if (ResultUIControll._withdrawalNumber > 0) {
				if (_readedSentenceCount < 5) {		//読んでない文があるとき
					if (_nextSentenceDisplayFlag) {
						AppearCharSlowly();
					}
				} else {
					DestroyTutoButton();		//移動できるようにする処理
				}
			//--------------------------------------------------------------------------------------------------------------
			} else {
				if (_readedSentenceCount < 3) {		//読んでない文があるとき
					if (_nextSentenceDisplayFlag) {
						AppearCharSlowly();
					}
				} else {
					DestroyTutoButton();	//移動できるようにする処理
				}
			}
		//-------------------------------------------------------------------------------------------------------------------------------
		//白の世界の処理------------------------------------------------------------------------------------------------------------------
		} else {
			_tutorialText.color = new Color (0f, 0f, 0f);
			if (_chageWorldFlag) {									//世界が変わったらボタンを消して次の文章へ
				_chageWorldFlag = false;
				_readedSentenceCount = 5;
			}
			AppearCharSlowly();
		}
		//-------------------------------------------------------------------------------------------------------------------------------
	}


	//--文字を少しずつ表示する関数
	void AppearCharSlowly() {
		if (_readedSentenceCount >= SENTENCE_NUMBER) return;		//全文読んでいたら処理をしない

		if (i == 0) {	//"前文が読み終わっている"とき、次の文章を表示するための初期化
			_tutorialText.text = "";
		}
		if (Time.frameCount % CHAR_SLOW_RATE == 0 && i < _sentence [_readedSentenceCount]._string.Length) {	//文字の表示
			_tutorialText.text += _sentence [_readedSentenceCount]._string [i++];
		}
		if (i == _sentence [_readedSentenceCount]._string.Length) {		//読み終わったら
			_readedSentenceCount++;
			_nextSentenceDisplayFlag = false;
			i = 0;
		}
	}


	//--チュートリアルボタンを消し、転移可能にする関数
	void DestroyTutoButton() {
		GameObject tutorialButton = GameObject.Find ("TutorialButton");
		if (tutorialButton) {
			tutorialButton.SetActive (false);
			_camera2D.World_Lock ();
		}
	}




	//---------------------------------------------------------------------
	//public関数
	//---------------------------------------------------------------------
	//--次の文章を表示させるためのフラグをオンにする関数
	public void TutorialButtonClicked() {
		_nextSentenceDisplayFlag = true;
	}
	//---------------------------------------------------------------------
	//---------------------------------------------------------------------
}
