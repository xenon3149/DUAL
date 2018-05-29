using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour {

public GameObject _textUI;

// Use this for initialization
void Start () {

	}
		
// Update is called once per frame
void Update () {

}

void OnTriggerEnter2D(Collider2D order) {
	/*  if (order.gameObject == player)
        {
            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }*/
		if (_textUI) {
			_textUI.SetActive (false);
		}
	}
}