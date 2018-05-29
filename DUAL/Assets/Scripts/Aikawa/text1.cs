using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text1 : MonoBehaviour {

	public GameObject _textUI;

	// Use this for initialization
	void Start () {
		_textUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

void OnTriggerEnter2D(Collider2D order)
{
	/*  if (order.gameObject == player)
        {
            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }*/

		//_textUI.SetActive ( false );
		if (_textUI) {
			_textUI.SetActive (true);
		}
	}
}