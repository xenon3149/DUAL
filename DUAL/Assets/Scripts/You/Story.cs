using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour {


    public GameObject Text;
    Camera2D Camera2D;



    public enum Tag
    {
        Player
    }

	// Use this for initialization
	void Start () {
		Camera2D = GameObject.Find("Main Camera").GetComponent<Camera2D>();
    }
	
	// Update is called once per frame
	void Update () {


	}

    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject == Camera2D.player)
        {
            if (Camera2D.NowStory != null)
            {
                Destroy(Camera2D.NowStory);
                Camera2D.NowStory = null;
            }
            Camera2D.NowStory = Text;
            Text.SetActive(true);          
            Destroy(gameObject);
        }
    }

}
