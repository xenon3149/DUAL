using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：ステージ詳細UIをアクティブ・非アクティブを管理するスクリプト
//
//アタッチ：ステージ詳細UIオンオフ時アクティブなゲームオブジェクトにアタッチ
public class StageDetailSetActive : MonoBehaviour {
	const int STAGE_NUMBER = 13;
	
	static public bool[] _StageDetail_Flag = new bool[STAGE_NUMBER];	//StageDetail.csで値を確認するためstaticで宣言
	[SerializeField] GameObject[] _stageDetail = new GameObject[STAGE_NUMBER];

	// Use this for initialization
	void Start () {
		//_StageDetail_Flag []の初期化　※_stageDetail[]の初期化はInspector上で
		for (int i = 0; i < _StageDetail_Flag.Length; i++) {
			_StageDetail_Flag [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//_stageDetailのアクティブ・非アクティブ化----------------------------------------------------
		for (int i = 0; i < _StageDetail_Flag.Length; i++) {
			if (_stageDetail [i]) {		//_stageDetailをInspector上で初期化してないものは処理しない
				if (_StageDetail_Flag [i]) {
					_stageDetail [i].SetActive (true);
				} else {
					_stageDetail [i].SetActive (false);
				}
			}
		}
		//--------------------------------------------------------------------------------------------
	}

}