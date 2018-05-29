using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：タッチ時のパーティクルを管理するスクリプト
//
//アタッチ：タッチ時のパーティクルにアタッチ
public class TouchParticleControll : MonoBehaviour {
	ParticleSystem _particle;

	// Use this for initialization
	void Start () {
		_particle = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_particle.isPlaying == false) {
			Destroy (gameObject);
		}
	}
}
