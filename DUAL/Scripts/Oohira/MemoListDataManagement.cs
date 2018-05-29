using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//機能：メモリストのデータ(MemoListData)を端末に保存・管理する
//　　　メモリストのデータの進行状況によってメモ詳細UIを表示・非表示する
//※獲得したメモリストを2進数で管理
//　00000(2)～11111(2)の数で管理(つまり、0～31の数で管理)
//アタッチ：シーン内で常にアクティブなGameObjectにアタッチ
public class MemoListDataManagement : MonoBehaviour {
	const int MEMODATANUM = 5;									//メモリストの種類数
	readonly string[] KEY = new string[MEMODATANUM];			//保存先のキー(このキーに獲得したメモリストをを保存する)

	[SerializeField] Button[] _memoButton = new Button[5];		//メモリストボタン



	//--------------------------------------------
	//ゲッター
	//--------------------------------------------
	public string GetKey( int index ) {
		return KEY [index];
	}
	//--------------------------------------------
	//--------------------------------------------


	//----------------------------------------------------------------------------------------------
	//bit演算系関数
	//----------------------------------------------------------------------------------------------
	//--- 自然数xのnビット目(n=0,1,2,...)がセットされているか確認する関数( 0：false / 1：trueで返す)
	public int CheckBit_n( int x, int n ) {
		return ( ( x >> n ) % 2 );
		//return ( ( x >> n ) & 1 );
	}
	//--- 自然数xのnビット目(n=0,1,2,...)をセットした値を返す関数
	public int SetBit_n( int x, int n ) {		
		return x | (1 << n);
	}
	//-----------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------


	// Use this for initialization
	void Start () {
		KEY [0] = "Boy'sNote";
		KEY [1] = "Girl'sNote";
		KEY [2] = "DiaryOfSomeone";
		KEY [3] = "Knight'sRecords";
		KEY [4] = "PastFragment";
			
		InitializeMemoDetail ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.Alpha0)) {		//デバックキー(セーブデータを削除する)
			PlayerPrefs.DeleteKey( KEY [0] );
			Debug.Log ("SaveDataDelete" + KEY [0] );
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.Alpha1)) {		//デバックキー(セーブデータを削除する)
			PlayerPrefs.DeleteKey( KEY [1] );
			Debug.Log ("SaveDataDelete" + KEY [1] );
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.Alpha2)) {		//デバックキー(セーブデータを削除する)
			PlayerPrefs.DeleteKey( KEY [2] );
			Debug.Log ("SaveDataDelete" + KEY [2] );
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.Alpha3)) {		//デバックキー(セーブデータを削除する)
			PlayerPrefs.DeleteKey( KEY [3] );
			Debug.Log ("SaveDataDelete" + KEY [3] );
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.Alpha4)) {		//デバックキー(セーブデータを削除する)
			PlayerPrefs.DeleteKey( KEY [4] );
			Debug.Log ("SaveDataDelete" + KEY [4] );
		}
		if ( Input.GetKey( KeyCode.S ) && Input.GetKey( KeyCode.Alpha0 ) ) {	//デバックキー(現在の最新のステージまで公開する　※随時更新)
			PlayerPrefs.SetInt (KEY [0], 31);
			PlayerPrefs.Save ();
			Debug.Log ( KEY [0] + "をコンプしたデータが保存されました" );
		}
		if ( Input.GetKey( KeyCode.S ) && Input.GetKey( KeyCode.Alpha1 ) ) {	//デバックキー(現在の最新のステージまで公開する　※随時更新)
			PlayerPrefs.SetInt (KEY [1], 31);
			PlayerPrefs.Save ();
			Debug.Log ( KEY [1] + "をコンプしたデータが保存されました" );
		}
		if ( Input.GetKey( KeyCode.S ) && Input.GetKey( KeyCode.Alpha2 ) ) {	//デバックキー(現在の最新のステージまで公開する　※随時更新)
			PlayerPrefs.SetInt (KEY [2], 31);
			PlayerPrefs.Save ();
			Debug.Log ( KEY [2] + "をコンプしたデータが保存されました" );
		}
		if ( Input.GetKey( KeyCode.S ) && Input.GetKey( KeyCode.Alpha3 ) ) {	//デバックキー(現在の最新のステージまで公開する　※随時更新)
			PlayerPrefs.SetInt (KEY [3], 31);
			PlayerPrefs.Save ();
			Debug.Log ( KEY [3] + "をコンプしたデータが保存されました" );
		}
		if ( Input.GetKey( KeyCode.S ) && Input.GetKey( KeyCode.Alpha4 ) ) {	//デバックキー(現在の最新のステージまで公開する　※随時更新)
			PlayerPrefs.SetInt (KEY [4], 31);
			PlayerPrefs.Save ();
			Debug.Log ( KEY [4] + "をコンプしたデータが保存されました" );
		}
	}



	//----------------------------------------------------------------------------------------------------------------------------------------------------------
	//public関数
	//----------------------------------------------------------------------------------------------------------------------------------------------------------
	//---_key[i]のデータのnビット目をセットしてセーブする関数
	public void SaveMemoListData( int i, int n ) {
		PlayerPrefs.SetInt ( KEY[i], SetBit_n( PlayerPrefs.GetInt( KEY[i] ), n ) );
		PlayerPrefs.Save ();
		Debug.Log ( KEY[i] + "の" + (n + 1) +"番目のメモリストが更新されました！" );
	}
		

	//---メモ詳細UIを表示・非表示にする関数
	public void InitializeMemoDetail(  ) {
		switch (gameObject.scene.name) {
		case "MemoryList1":
			for (int i = 0; i < _memoButton.Length; i++) {
				if (CheckBit_n( PlayerPrefs.GetInt( KEY[0] ), i ) == 1) {	//指定のビットがセットされているか確認(つまり、メモリストを取得しているか確認)
					_memoButton [i].enabled = true;
				} else {
					_memoButton [i].gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 85f / 255);
					_memoButton [i].enabled = false;
				}
			}
			break;
		case "MemoryList2":
			for (int i = 0; i < _memoButton.Length; i++) {
				if (CheckBit_n( PlayerPrefs.GetInt( KEY[1] ), i ) == 1) {	//指定のビットがセットされているか確認(つまり、メモリストを取得しているか確認)
					_memoButton [i].enabled = true;
				} else {
					_memoButton [i].gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 85f / 255);
					_memoButton [i].enabled = false;
				}
			}
			break;
		case "MemoryList3":
			for (int i = 0; i < _memoButton.Length; i++) {
				if (CheckBit_n( PlayerPrefs.GetInt( KEY[2] ), i ) == 1) {	//指定のビットがセットされているか確認(つまり、メモリストを取得しているか確認)
					_memoButton [i].enabled = true;
				} else {
					_memoButton [i].gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 85f / 255);
					_memoButton [i].enabled = false;
				}
			}
			break;
		case "MemoryList4":
			for (int i = 0; i < _memoButton.Length; i++) {
				if (CheckBit_n( PlayerPrefs.GetInt( KEY[3] ), i ) == 1) {	//指定のビットがセットされているか確認(つまり、メモリストを取得しているか確認)
					_memoButton [i].enabled = true;
				} else {
					_memoButton [i].gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 85f / 255);
					_memoButton [i].enabled = false;
				}
			}
			break;
		case "MemoryList5":
			for (int i = 0; i < _memoButton.Length; i++) {
				if (CheckBit_n( PlayerPrefs.GetInt( KEY[4] ), i ) == 1) {	//指定のビットがセットされているか確認(つまり、メモリストを取得しているか確認)
					_memoButton [i].enabled = true;
				} else {
					_memoButton [i].gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 85f / 255);
					_memoButton [i].enabled = false;
				}
			}
			break;
		default :
			Debug.Log ("ここはメモリストシーンではありません");
			break;
		}
	}
	//----------------------------------------------------------------------------------------------------------------------------------------------------------
	//----------------------------------------------------------------------------------------------------------------------------------------------------------
}
