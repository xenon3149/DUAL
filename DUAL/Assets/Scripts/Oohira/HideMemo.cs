using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：メモリストをアイテムの裏に隠すスクリプト
//　　　メモリストを隠しているアイテムが転移または削除された時にメモリストをアクティブにする
//アタッチ：シーン内で常にアクティブなGameObjectにアタッチ
public class HideMemo : MonoBehaviour {

	[SerializeField] GameObject _item = null;		//メモリストを隠しているアイテム
	[SerializeField] GameObject _memo = null;		//隠されたメモリスト

	//------------------------------------------------
	//ゲッター
	//------------------------------------------------
	public GameObject GetItem( ) { return _item; }
	//------------------------------------------------
	//------------------------------------------------

	// Use this for initialization
	void Start () {
		if (_memo) {
			_memo.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


	//------------------------------------------------
	//public関数
	//------------------------------------------------
	//--隠されたメモリストを表示する関数
	public void DisplayMemo( ) {
		if (_memo) {
			_memo.SetActive (true);
			_memo.transform.parent = null;
		}
	}
	//------------------------------------------------
	//------------------------------------------------
}
