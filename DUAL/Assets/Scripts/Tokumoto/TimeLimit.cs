using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeLimit : MonoBehaviour {
    public int Time = 120;
    float Nowtime;
    Text Tx;
	// Use this for initialization
	void Start () {
        Nowtime = Time;
        Tx = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        Nowtime -= UnityEngine.Time.deltaTime;
        if (Nowtime < 0)
        {
            Result();
        }
        else {
            Tx.text = (((int)Nowtime / 60)).ToString("0") + ":" + ((int)Nowtime % 60).ToString("0");
        }
	}

    void Result() {
       Instantiate( Resources.Load("UIPrefab/ResultUI"));
        Destroy(this);
        
    }

}
