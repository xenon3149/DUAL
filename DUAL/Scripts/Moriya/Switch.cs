using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    public GameObject wall;

    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update(){

    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player") {
            wall.SetActive(false);
            StartCoroutine( Break_wall() );
        }
    }

    private IEnumerator Break_wall() {
        yield return new WaitForSeconds(3f);
        wall.SetActive(true);
    }
}