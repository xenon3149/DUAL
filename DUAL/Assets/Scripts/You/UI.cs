using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour, IPointerDownHandler
{
    public Sprite on;
    public Sprite off;


    public enum UIname
    {
        OrbButton
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {


	}


    public void OnPointerDown(PointerEventData eventData)
    {
		if (GameObject.Find ("Main Camera").GetComponent<Camera2D> ()) {
			if (GameObject.Find ("Main Camera").GetComponent<Camera2D> ().ob == true) {
				GameObject.Find (UIname.OrbButton.ToString ()).GetComponent<Image> ().sprite = off;
				GameObject.Find ("Main Camera").GetComponent<Camera2D> ().ob = false;
			} else {
				GameObject.Find (UIname.OrbButton.ToString ()).GetComponent<Image> ().sprite = on;
				GameObject.Find ("Main Camera").GetComponent<Camera2D> ().ob = true;
			}
		}
		if (GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ()) {
			if (GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ().ob == true) {
				GameObject.Find (UIname.OrbButton.ToString ()).GetComponent<Image> ().sprite = off;
				GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ().ob = false;
			} else {
				GameObject.Find (UIname.OrbButton.ToString ()).GetComponent<Image> ().sprite = on;
				GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ().ob = true;
			}
		}
    }
}

