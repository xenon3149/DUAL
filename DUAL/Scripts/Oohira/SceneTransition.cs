using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//機能：シーン遷移を行うスクリプト
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class SceneTransition : MonoBehaviour {
	const float _background_lastPosition = 20f;			//アニメーション終了位置(x座標)

	[SerializeField] Animator _anim;					//遷移アニメーション（あるなら）
	[SerializeField] string _next_scene_name;			//次のシーン名



	// Use this for initialization
	void Start () {
		if (GameObject.Find ("BackGround")) {
			_anim = GameObject.Find ("BackGround").GetComponent<Animator> ();
		}
	}

	// Update is called once per frame
	void Update () {
		//_anim(アニメーション)がある場合行う処理-----------------------------------------------------------------
		if (_anim) {
			if (_anim.rootPosition.x >= _background_lastPosition) {		//アニメーションが終了したときに行う処理
				SceneTransitionButtonClicked (_next_scene_name);
			}
		}
		//-------------------------------------------------------------------------------------------------------
	}




	//--------------------------------------------------------------------------------------------------------------------------------------
	//public関数
	//--------------------------------------------------------------------------------------------------------------------------------------
	public void SceneTransitionButtonClicked( string _scene_name ) {
		if (_anim && _anim.rootPosition.x < _background_lastPosition ) {		//アニメーションがあり、終了していないとき
			_anim.SetBool ("ButtonTap", true);
			_next_scene_name = _scene_name;
		} else {																//アニメーションがない、またはアニメーションが終了しているとき
			SceneManager.LoadScene (_scene_name);
		}
	}
	//---------------------------------------------------------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------------------------------------------------------

}

