using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk : MonoBehaviour {


    GameObject player;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.Find("Main Camera").GetComponent<Camera2D>().player;

    }



    void OnTriggerStay2D(Collider2D order)
    {
        if (order.gameObject == player)
        {
			ResultUIControll._withdrawalNumber++;
			if (GameObject.Find ("TimeText")) {
				ResultUIControll._clearTime = GameObject.Find ("TimeText").GetComponent<time> ().countTime;
			}

            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }
    }
}
