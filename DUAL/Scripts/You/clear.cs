using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clear : MonoBehaviour {

    GameObject player;
	GameObject _resultUI;
	GameObject _stageUI;

    public enum UIname
    {
        ResultUI,
        StageUI
    }

	void Awake() {
		_resultUI = GameObject.Find ( UIname.ResultUI.ToString() );		
		//ゴールが二か所ある場合、Startで_resultUIに代入するとその後非アクティブにしてしまうので二か所目のゴールに_resultUIが
		//Nullとなるため、Awakeで代入を行う
	}


    // Use this for initialization
    void Start()
    {
		if (_resultUI) {
			_resultUI.SetActive (false);
		}
		_stageUI = GameObject.Find ( UIname.StageUI.ToString() );

    }

    // Update is called once per frame
    void Update()
    {
		if (GameObject.Find ("Main Camera").GetComponent<Camera2D> ()) {
			player = GameObject.Find ("Main Camera").GetComponent<Camera2D> ().player;
		}
		if (GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ()) {
			player = GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ().player;
		}
    }


    void OnTriggerEnter2D(Collider2D order)
    {
      /*  if (order.gameObject == player)
        {
            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }*/
		
		_resultUI.SetActive ( true );
		_stageUI.SetActive ( false );

		GameObject.Find ("UIManager").GetComponent< ResultUIControll > ().ResultDisplay();
    }

	public void RestartScene() {
		if (GameObject.Find ("Main Camera").GetComponent<Camera2D> ()) {
			GameObject.Find ("Main Camera").GetComponent<Camera2D> ().RestartScene ();
		}
		if (GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ()) {
			GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ().RestartScene ();
		}
	}



}
