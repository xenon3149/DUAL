using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：メモリストを取ったらステージクリア判定になり、リザルトUIが表示される
//
//アタッチ：シーン内で常にアクティブなGameObjectにアタッチ
public class BossStageMemo : MonoBehaviour {

	[SerializeField] private GameObject _memo = null;
	[SerializeField] private GameObject _resultUI = null;
	[SerializeField] private GameObject _UIManager = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!_memo) {
			StartCoroutine ( Clear() );
		}
	}


	//--2秒後にリザルトUIを表示する関数（コルーチン）
	IEnumerator Clear() {
		yield return new WaitForSeconds (2);
		_resultUI.SetActive (true);
		_UIManager.GetComponent<ResultUIControll> ().ResultDisplay ();
		Time.timeScale = 0f;
	}
}
