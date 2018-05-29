using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：タイトルシーンのパーティクルを変えるスクリプト
//
//アタッチ：常にアクティブなゲームオブジェクトにアタッチ
public class ParticleControll : MonoBehaviour {

	private GameObject _particles;
	private ParticleSystem[] _titleParticles;
	private GameObject _canvas;
	[SerializeField] private GameObject _background = null;


	void Awake() {	//シーン遷移時に非アクティブになっている（UISetActive.csで非アクティブにしてます）のでその前に参照取得
		_canvas = GameObject.Find ("Canvas");
	}
		
	// Use this for initialization
	void Start () {
		_particles = GameObject.Find( "FireflyEffects" );
		_particles.SetActive ( false );
		_titleParticles = _particles.GetComponentsInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_canvas.activeSelf) {
			_particles.SetActive (true);
		} else {
			_particles.SetActive (false);
		}
		if (_background.GetComponent<BackgroundRandom> ().GetBackgroundColorChange ()) {
			for (int i = 0; i < _titleParticles.Length; i++) {
				//_titleParticles [i].startColor = new Color (0, 0, 0);		//古い形式みたいです…。
				ParticleSystem.MainModule mainmodule = _titleParticles[i].main;
				mainmodule.startColor = new Color(0, 0, 0);
			}
		}

	}
}
