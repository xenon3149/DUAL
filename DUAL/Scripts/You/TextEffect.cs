using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextEffect : MonoBehaviour {

    public float fSpeed = 0.1f;
    public float EndTime = 3f;
	public bool World_open = false;
	public bool Continue = false;
    Camera2D CameraSC;

	public GameObject Continue_text;

    float time;
    Text Showtext;
    string sContent;
    int curPos;
    void Start()
    {
        CameraSC = GameObject.Find("Main Camera").GetComponent<Camera2D>();
        Showtext = GetComponent<Text>();
        SetContent();
		if (World_open) {
			GameObject.Find ("Main Camera").GetComponent<Camera2D> ().World_lock = false;
		}
    }
    void SetContent()
    {
        curPos = 0;
        sContent = Showtext.text;
        //Debug.Log("lenth++" + sContent.Length);
        Showtext.text = string.Empty;
        InvokeRepeating("Typing", 0, fSpeed);
    }

    void Update()
    {
		if (CameraSC.player == GameObject.Find ("player")) {
			gameObject.GetComponent<Text> ().color = new Color (255f, 255f, 255f);
		} else {
			gameObject.GetComponent<Text> ().color = new Color (0f, 0f, 0f);
		}
			
        time += Time.deltaTime;
        if (!sContent.Contains(Showtext.text))
        {
            //Debug.Log("typing");
            CancelInvoke("Typing");
            SetContent();
        }

        if (time >= EndTime)
        {
			if (Continue) {
				Continue_text.SetActive (true);
                CameraSC.NowStory = Continue_text;
            }
            else
            {
                CameraSC.NowStory = null;
            }
			   
            Destroy(gameObject);
        }

    }

    void Typing()
    {
        if (sContent.Length - 1 == curPos)
            CancelInvoke("Typing");

        Showtext.text += sContent.Substring(curPos, 1);
        curPos++;
    }

}

