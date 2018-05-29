using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePanelCreate : MonoBehaviour {

	public GameObject[] _stagePanel;
	//public GameObject _player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if (other.gameObject.name == "StageCreateLineRight") {
			Instantiate (_stagePanel [0],new Vector3 (transform.position.x + 3.2f, 0, 0), Quaternion.identity);
		}
		if (other.gameObject.name == "StageCreateLineLeft") {
			Instantiate (_stagePanel [0],new Vector3 (transform.position.x - 3.2f, 0, 0), Quaternion.identity);
		}
	}


}
