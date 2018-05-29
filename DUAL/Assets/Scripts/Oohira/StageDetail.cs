using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//機能：ステージ詳細UIのスクリプト
//
//アタッチ：ステージ詳細UI
public class StageDetail : MonoBehaviour {

	[SerializeField] Animator _anim;
	[SerializeField] GameObject _stage_button_text;
	[SerializeField] int _siblingIndex;


	//BackgrondにアタッチされているUISetActive.csによってCanvasが非アクティブになってしまうので、Start関数より前に処理する
	void Awake() {
		_anim = GetComponent<Animator> ();
		_stage_button_text = transform.GetChild(0).gameObject;
		_siblingIndex = transform.GetSiblingIndex();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//-------------------------------------------------------------------------------------------
	//public関数
	//-------------------------------------------------------------------------------------------
	//_animのStageButton_Tapをtrueにする関数(外部からの操作用)
	public void SetStageButton_Tap( ) {
		_anim.SetBool ( "StageButton_Tap", true );
	}


	//_animのStageButton_Tapをfalseにする関数(外部からの操作用)
	public void ResetStageButton_Tap( ) {
		_anim.SetBool ( "StageButton_Tap", false );
	}


	//_animのStageDetail_ReturnButton_Tapをtrueにする関数(外部からの操作用)
	public void SetStageDetail_ReturnButton_Tap( ) {
		_anim.SetBool ( "StageDetail_ReturnButton_Tap", true );
	}


	//_animのStageDetail_ReturnButton_Tapをfalseにする関数(外部からの操作用)
	public void ResetStageDetail_ReturnButton_Tap( ) {
		_anim.SetBool ( "StageDetail_ReturnButton_Tap", false );
	}

	//Textを非アクティブにする関数(外部からの操作用)
	public void TextResetActive( ) {
		_stage_button_text.SetActive ( false );
	}


	//Textを非アクティブにする関数(外部からの操作用)
	public void TextsetActive( ) {
		_stage_button_text.SetActive ( true );
	}
		

	//ローカルのtransformリストで一番最後の順番になるように移動する関数(外部からの操作用)
	public void MoveLastSibling( ) {
		transform.SetAsLastSibling();
	}


	//初期のtransformのインデックスに戻る関数(外部からの操作用)
	public void MoveFirstIndex() {
		transform.SetSiblingIndex (_siblingIndex);
	}


	//
	//以下StageDetailSetActive.csと連携する関数たち
	//

	//_StageDetail_Flag[i]をtrueにする関数(外部からの操作用)
	public void Set_StageDetail_Flag_i( int i ) {
		StageDetailSetActive._StageDetail_Flag[i] = true;
	}


	//_StageDetail_Flag[i]をfalseにする関数(外部からの操作用)
	public void Reset_StageDetail_Flag_i( int i ) {
		StageDetailSetActive._StageDetail_Flag[i] = false;
	}
	//-------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------

}
