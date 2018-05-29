using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//機能：クリア時にリザルトUIの表示の有無で時間を止めるクラス
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class ClearStop : MonoBehaviour {

	[SerializeField] GameObject _resultUI = null;

	bool _stopFlag = false;		//一度だけ時間を止めるようにするための変数

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_resultUI.activeSelf) {
			if (!_stopFlag) {
				Camera.main.GetComponent<Camera2D> ().TimeStop ();
				_stopFlag = true;
			}
		}
	}


}
