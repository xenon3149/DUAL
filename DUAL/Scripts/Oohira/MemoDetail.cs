using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：メモボタンのアニメーションを管理
//　　　メモ詳細UIの表示・非表示を管理
//アタッチ：各メモボタンにアタッチする
//※StageDetail.csとほとんど同じ機能
public class MemoDetail : MonoBehaviour {

	private Animator _anim;					//メモボタンのアニメーション
	private GameObject　_buttonText;			//メモボタンの文字
	private int _siblingIndex;				//メモボタンのTransformリストのインデックス番号

	[SerializeField] private GameObject _memoDetail = null;						//メモ詳細UI

		
	//-----------------------------------------------------------------------------------------------------------------
	//ゲッター
	//-----------------------------------------------------------------------------------------------------------------
	public bool GetMemoDetailActive() { return _memoDetail.activeSelf; }		//メモ詳細UIがActiveかどうかを返す関数
	//-----------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------


	//BackgrondにアタッチされているUISetActive.csによってCanvasが非アクティブになってしまうので、Start関数より前に処理する
	void Awake() {
		_anim = GetComponent<Animator> ();
		_buttonText = transform.GetChild(0).gameObject;
		_siblingIndex = transform.GetSiblingIndex();
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//--------------------------------------------------------------------------------------
	//public関数
	//--------------------------------------------------------------------------------------
	#region アニメーションイベント関数
	//--_animのTapをtrue / falseにする関数(外部からの操作用)
	public void Change_Tap( ) {
		if (_anim.GetBool ("Tap")) {
			_anim.SetBool ("Tap", false);
		} else {
			_anim.SetBool ("Tap", true);
		}
	}
		

	//--_animのReturnTapをtrue / falseにする関数(外部からの操作用)
	public void Change_ReturnTap( ) {
		if (_anim.GetBool ("ReturnTap")) {
			_anim.SetBool ("ReturnTap", false);
		} else {
			_anim.SetBool ("ReturnTap", true);
		}
	}


	//--Textをアクティブ / 非アクティブにする関数(外部からの操作用)
	public void ChangeTextActive( ) {
		if (_buttonText.activeSelf) {
			_buttonText.SetActive (false);
		} else {
			_buttonText.SetActive (true);
		}
	}


	//--ローカルのtransformリストで一番最後の順番になるように移動する関数(外部からの操作用)
	public void MoveLastSibling( ) {
		transform.SetAsLastSibling();
	}


	//--初期のtransformのインデックスに戻る関数(外部からの操作用)
	public void MoveFirstIndex() {
		transform.SetSiblingIndex (_siblingIndex);
	}


	#region メモ詳細UIの表示・非表示
	//--メモ詳細UIの表示・非表示をする関数
	public void SetActive_MemoListDetail() {
		if (_memoDetail.activeSelf) {
			_memoDetail.SetActive (false);
		} else {
			_memoDetail.SetActive (true);
		}
	}
	#endregion

	#endregion
	//----------------------------------------------------------------------------------------
	//----------------------------------------------------------------------------------------


}
