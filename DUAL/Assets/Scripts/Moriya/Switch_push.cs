using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_push : MonoBehaviour {

    public GameObject wall2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay2D( Collision2D col ) {
        if( col.gameObject.tag == "Player" || col.gameObject.tag == "Box" ) {
            wall2.SetActive(false);
        }
    }

    void OnCollisionExit2D( Collision2D col ) {
            wall2.SetActive( true );
    }
}