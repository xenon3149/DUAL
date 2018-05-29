using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//機能：リザルトUIを管理するスクリプト。スコアに応じてMemoristManagerにデータのセーブを要求する。
//
//アタッチ：UIManagerにアタッチ
public class ResultUIControll : MonoBehaviour {

	public Text _clearTimeText;			//クリアタイムを表示するためのテキスト
	public Text _withdrawalNumberText;	//撤退回数を表示するためのテキスト
	public Text _transitionNumberText;	//転移回数を表示するためのテキスト
	[SerializeField] GameObject[] _star = new GameObject[3];	//スコア星を格納する変数
	[SerializeField] int _targetClearTime = 300;				//目標クリアタイム
	[SerializeField] int _targetWithdrawalNumber = 3;			//目標撤退回数
	[SerializeField] int _targetTransitionNumber = 3;			//目標転移回数
	[SerializeField] MemoListDataManagement _memolistManager = null;
	[SerializeField] private int _keynumber = 0;				//キーのインデックス番号
	[SerializeField] private int _savebitnumber = 0;				//保存する番号
	GameObject _messagePanel;					//メモリストが追加されたことを示すパネル
	bool _savedMemolistFlag;					//メモリストにセーブされているかどうかのフラグ

	public GameObject _timer;

	static public float _clearTime;			//クリアタイム　※他のスクリプトで使えるようにstaticで宣言(_clearTimeに関してはstaticでなくてもいいかも)
	static public int _withdrawalNumber;	//撤退回数
	static public int _transitionNumber;	//転移回数

	void Awake() {
		if (GameObject.Find ("ClearTimeText")) {
			_clearTimeText = GameObject.Find ("ClearTimeText").GetComponent<Text> ();
		}
		if (GameObject.Find ("WithdrawalText")) {
			_withdrawalNumberText = GameObject.Find ("WithdrawalText").GetComponent<Text> ();
		}
		if (GameObject.Find ("TransitionText")) {
			_transitionNumberText = GameObject.Find ("TransitionText").GetComponent<Text> ();
		}
		if (GameObject.Find ("MessagePanel")) {
			_messagePanel = GameObject.Find ("MessagePanel");
			_messagePanel.SetActive (false);
		}

	}

	// Use this for initialization
	void Start () {
		_timer = GameObject.Find ( "TimeText" );
		_savedMemolistFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	//--リザルトUIを表示する関数
	public void ResultDisplay( ) {
		_clearTime = _timer.GetComponent<time> ().countTime;
		_clearTimeText.text = Convert.ToString ((int)_clearTime) + "/" + _targetClearTime.ToString() + "秒";
		_withdrawalNumberText.text = Convert.ToString (_withdrawalNumber) + "/" + _targetWithdrawalNumber.ToString() + "回";
		_transitionNumberText.text = Convert.ToString (_transitionNumber) + "/" + _targetTransitionNumber + "回";
		if (_memolistManager.CheckBit_n (PlayerPrefs.GetInt (_memolistManager.GetKey (_keynumber)), _savebitnumber) == 1) {
			_savedMemolistFlag = true;
		}
		ScoreDisplay ();
		_clearTime = 0f;
		_withdrawalNumber = 0;
		_transitionNumber = 0;
	}


	//--スコアを表示し、メモリストセーブをする関数
	void ScoreDisplay( ) {
		int score = 3;
		if (_clearTime > _targetClearTime) {
			score--;
		}
		if (_withdrawalNumber > _targetWithdrawalNumber) {
			score--;
		}
		if (_transitionNumber > _targetTransitionNumber) {
			score--;
		}
		for (int i = 0; i < 3 - score; i++) {
			_star [_star.Length - 1 - i].SetActive (false);
		}
		if (score == _star.Length && !_savedMemolistFlag) {
			_memolistManager.SaveMemoListData (_keynumber, _savebitnumber);
			DisplayMessage ();
		}
	}


	//--メモリストが追加されたことを示すテキストを表示する関数
	void DisplayMessage( ) {
		Text messageText = _messagePanel.GetComponentInChildren<Text> ();
		switch (_keynumber) {
		case 0:
			messageText.text = "少年の手記";
			break;
		case 1:
			messageText.text = "少女の手記";
			break;
		case 2:
			messageText.text = "誰かの日記";
			break;
		case 3:
			messageText.text = "騎士の記録書";
			break;
		case 4:
			messageText.text = "過去の断片";
			break;
		default:
			break;
		}
		messageText.text += "が追加されました！";
		_messagePanel.SetActive (true);
	}
}
