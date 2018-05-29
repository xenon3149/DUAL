using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    public GameObject Boss;
    bool Flag = false;
    IEnumerator MainLoop() {

        for (float i = 0; i < 1; i += UnityEngine.Time.deltaTime / 2) {
            Camera.main.orthographicSize = Mathf.Lerp(7,12,i);
            yield return null;
        }
        Boss.SetActive(true);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D a)
    {
        if (a.gameObject.tag == "Player" && !Flag)
        {
            StartCoroutine(MainLoop());
            Flag = true;
        }
    }
}
