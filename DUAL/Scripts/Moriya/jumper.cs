using UnityEngine;
using System.Collections;

public class jumper : MonoBehaviour
{

    public float Player_Jump;
    public bool Jump;

    GameObject player;

    void Update()  {

        player = GameObject.Find("Player");

    }

    void OnTriggerEnter2D(Collider2D col){

        if (col.gameObject.tag == "Player")  {
            if (Jump) {
                player.GetComponent<Rigidbody2D>().AddForce(transform.up * Player_Jump, ForceMode2D.Impulse);
            }
        }
    }
}