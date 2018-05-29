using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {

    public GameObject _buttonUI;
    public GameObject _buttonUI2;

    // Use this for initialization
    void Start()
    {
        _buttonUI.SetActive ( false );
        _buttonUI2.SetActive ( false );  
    }

    // Update is called once per frame
    void Update() {
    }

    void OnTriggerEnter2D(Collider2D order) {
        /*  if (order.gameObject == player)
            {
                GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
            }*/

        _buttonUI.SetActive ( false );
        _buttonUI.SetActive ( true );
        _buttonUI2.SetActive( false );
        _buttonUI2.SetActive( true );

    }
}