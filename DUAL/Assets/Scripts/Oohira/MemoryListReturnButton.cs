using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//機能：メモ詳細UIの有無でReturnButtonの仕様を変える(シーン遷移 or メモ詳細UI非表示)
//
//アタッチ：MemoryListシーンのReturnButtonにアタッチ
public class MemoryListReturnButton : MonoBehaviour {
	private GameObject　_sceneManager;												//シーン遷移するためにScenaManagerを参照
	[SerializeField] private GameObject[] _memoButton = new GameObject[5];	//メモ詳細UI非表示を行うためにメモボタンを参照



	// Use this for initialization
	void Start () {
		_sceneManager = GameObject.Find ("SceneManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//---------------------------------------------------------------------------------------------------------------
	//public関数
	//---------------------------------------------------------------------------------------------------------------
	public void Tap() {
		for( int i = 0; i < _memoButton.Length; i++ ) {
			MemoDetail memoDetail = _memoButton [i].GetComponent<MemoDetail> ();
			if (memoDetail.GetMemoDetailActive ()) {
				memoDetail.Change_ReturnTap ();
				break;
			}
			if (i == _memoButton.Length - 1) {
				_sceneManager.GetComponent<SceneTransition> ().SceneTransitionButtonClicked ("MemoryListMain");
			}
		}
	}
	//-----------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------
}
