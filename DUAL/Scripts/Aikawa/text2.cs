using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text2 : MonoBehaviour {
	
	public GameObject _textUI;
	public GameObject _textUI2;

	// Use this for initialization
	void Start () {
		_textUI.SetActive (false);
		_textUI2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

void OnTriggerEnter2D(Collider2D order) {
	/*  if (order.gameObject == player)
        {
            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }*/

		//_textUI.SetActive ( false );
		if (_textUI2) {
			_textUI2.SetActive (true);
		}
	}
}