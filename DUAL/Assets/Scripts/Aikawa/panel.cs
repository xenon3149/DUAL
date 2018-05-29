using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel : MonoBehaviour {
	
	public GameObject _panelUI;

	// Use this for initialization
	void Start () {
		_panelUI.SetActive ( false );
	}
	
	// Update is called once per frame
	void Update () {
	}

void OnTriggerEnter2D(Collider2D order) {
	/*  if (order.gameObject == player)
        {
            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }*/

		_panelUI.SetActive ( false );
        _panelUI.SetActive ( true );

	}
}