using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//機能：プレイヤーとの当たり判定。MemoristManagerにデータのセーブを要求する。
//
//アタッチ：各ステージのメモリストにアタッチ
public class Memo : MonoBehaviour {
	const int MEMODATANUM = 5;																					//メモリストの種類数
	readonly string[] KEY = {																					//保存先のキー(このキーに獲得したメモリストを保存する)
		"Boy'sNote",
		"Girl'sNote",
		"DiaryOfSomeone",
		"Knight'sRecords",
		"PastFragment"
	};																											

	//[SerializeField] private GameObject _memoListManager = null;	//MemoristManagerの参照
	[SerializeField] MemoListDataManagement _memoListDataManagement = null;
	[SerializeField] int _keynumber = 0;					//キーのインデックス番号
	[SerializeField] int _savebitnumber = 0;				//保存する番号
	//00001 → 1
	//00010 → 2
	//00100 → 4
	//01000 → 8
	//10000 → 16

	// Use this for initialization
	void Start () {
		if (_memoListDataManagement.CheckBit_n (PlayerPrefs.GetInt (KEY[_keynumber]), _savebitnumber) == 1) {	//以前にメモリストを取得していたら削除する
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//--プレイヤーがメモに当たったらデータをセーブしてDestroyする関数(OnTriggerEnter2D)
	void OnTriggerEnter2D( Collider2D col ) {
		if (col.gameObject.name == "player" || col.gameObject.name == "player2") {
			_memoListDataManagement.SaveMemoListData ( _keynumber, _savebitnumber );
			Destroy (this.gameObject);
		}
	}

}
