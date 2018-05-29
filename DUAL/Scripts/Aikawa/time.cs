using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class time : MonoBehaviour
{

    public float countTime;
	//public string _scene_name;

    // Use this for initialization
    void Start()
    {
		countTime = ResultUIControll._clearTime;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime; //スタートしてからの秒数を格納
		//GetComponent<Text>().text = countTime.ToString("F0"); //小数2桁にして表示
		GetComponent<Text>().text = countTime.ToString("000");
        //GetComponent<Text>().text = (((int)countTime / 60)).ToString("00") + ":" + ((int)countTime % 60).ToString("00");

        /*
		if (countTime < 0.0f) {
			SceneManager.LoadScene (_scene_name);
		}*/
    }

}