using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {
	
	public GameObject _player;
	float _value;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find ( "Player" );
	}
	
	// Update is called once per frame
	void Update () {
		_value = transform.position.z;
		transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _value);
	}
}
