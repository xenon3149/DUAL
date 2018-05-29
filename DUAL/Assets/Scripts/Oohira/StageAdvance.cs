using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//機能：クリアしたステージを保存する関数
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class StageAdvance : MonoBehaviour {
	const string SAVE_KEY = "StageClearNumber";							//保存先のキー(このキーにクリアしたステージ数を保存する)

	static public int _clearStageNumber = 0;							//クリアステージの通し番号
	[SerializeField] private Button[ ] _stageSelectButton = null;		//ステージボタン(StageSelectシーンのみ利用)


	// Use this for initialization
	void Start () {
		if (gameObject.scene.name == "StageSelect") {
			InitializeStageSelect ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//デバックキー(セーブデータを削除する)-------------------------------
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.T)) {
			PlayerPrefs.DeleteKey(SAVE_KEY);
			Debug.Log ("SaveDataDelete!");
		}
		//-----------------------------------------------------------------

		//デバックキー(現在の最新のステージまで公開する　※随時更新)---------------
		if ( Input.GetKey( KeyCode.S ) && Input.GetKey( KeyCode.L ) ) {
			PlayerPrefs.SetInt (SAVE_KEY, 7);
			PlayerPrefs.Save ();
			Debug.Log ( "stage2-3までクリアしたデータが保存されました" );
		}
		//----------------------------------------------------------------------
		
	}


	//----------------------------------------------------------------------------------------------
	//public関数
	//----------------------------------------------------------------------------------------------
	//--クリアしたステージ数を保存する関数
	public void SaveStageClearNumber ( GameObject player ) {
		switch (player.gameObject.scene.name) {
		case "TutorialStage3":
			_clearStageNumber = 1;
			break;
		case "stage1-1":
			_clearStageNumber = 2;
			break;
		case "stage1-2":
			_clearStageNumber = 3;
			break;
		case "stage1-3":
			_clearStageNumber = 4;
			break;
		case "Boss":
			_clearStageNumber = 5;
			break;
		case "stage2-1":
			_clearStageNumber = 6;
			break;
		case "stage2-2":
			_clearStageNumber = 7;
			break;
		case "stage2-3":
			_clearStageNumber = 8;
			break;
		default :
			Debug.LogError ("想定していたステージシーン以外でSaveStageNumber関数が呼ばれました");
			break;
		}
		if (_clearStageNumber > PlayerPrefs.GetInt (SAVE_KEY)) {
			PlayerPrefs.SetInt (SAVE_KEY, _clearStageNumber);
			PlayerPrefs.Save ();
			Debug.Log ( "クリアしたステージ数を保存しました！" );
		}
	}
	//-------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------


	//--クリアしたステージ数に応じてボタンを初期化する関数
	void InitializeStageSelect() {	
		for (int i = 0; i < _stageSelectButton.Length; i++) {
			if (i > PlayerPrefs.GetInt (SAVE_KEY)) {
				_stageSelectButton [i].gameObject.GetComponent<Image> ().color = new Color(1f,1f,1f, 85f / 255);
				_stageSelectButton [i].enabled = false;
			} else {
				_stageSelectButton [i].enabled = true;
			}
		}
	}

}
