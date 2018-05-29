using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight : MonoBehaviour {

    GameObject player;
    public GameObject atk_decision;
    public float speed = 3f;
    public float range = 10f;
    public Rigidbody2D rb2D;
    public Animator animator;
    public bool atk = false;
    float atk_time = 0f;
    public bool move = false;
    public bool face_left = true;
    //private float direction = 1f;
    //float time = 0f;
    Vector2 setposition;


    public enum knight_Animator
    {
        atk,
        move,
        face_left

    }

    public enum Layer
    {
        Water = 4
    }


    // Use this for initialization
    void Start () {
        setposition = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.Find("Main Camera").GetComponent<Camera2D>().player;
		transform.localEulerAngles = new Vector3(0, 0, 0);//回転禁止
        if (Vector2.Distance(player.transform.position, transform.position) <= 4f)
        {
            atk = true;
        }
    }

    void FixedUpdate()
    {



        if (atk)
        {

            if (player.transform.position.x - transform.position.x >= 0f)
            {
                face_left = false;
            }
            else
            {
                face_left = true;
            }

            move = false;
            atk_time++;
            //Debug.Log(atk_time);
            if (atk_time == 50f)
            {
                atk_decision.SetActive(true);
            }
            if (atk_time >= 60f)
            {
                atk = false;
                //move = true;
                atk_time = 0f;
                atk_decision.SetActive(false);
            }

        }



        if (move)
        {
            if (Vector2.Distance(transform.position, setposition) >= range)
            {
				if (transform.position.x < setposition.x )
                {
                    face_left = false;
                }
                else
                {
                    face_left = true;
                }
            }
            if (face_left)
            {
				rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
            }
            else
            {
				rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            }
        }
        anima();
    }



    void anima()
    {
        if (move)
        {
            animator.SetBool(knight_Animator.move.ToString(), true);
        }else
        {
            animator.SetBool(knight_Animator.move.ToString(), false);
        }
        if (atk)
        {
            animator.SetBool(knight_Animator.atk.ToString(), true);
        }else
        {
            animator.SetBool(knight_Animator.atk.ToString(), false);
        }
        if (face_left)
        {
            animator.SetBool(knight_Animator.face_left.ToString(), true);
        }else
        {
            animator.SetBool(knight_Animator.face_left.ToString(), false);
        }

    }


    void OnCollisionStay2D(Collision2D water)
    {
        if (water.gameObject.layer == 4)
        {
            if (atk == false)
            {
                move = true;
            }
            //Debug.Log("move_knight");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject == player)
        {
			ResultUIControll._withdrawalNumber++;
			if (GameObject.Find ("TimeText")) {
				ResultUIControll._clearTime = GameObject.Find ("TimeText").GetComponent<time> ().countTime;
			}

            GameObject.Find("Main Camera").GetComponent<Camera2D>().RestartScene();
        }
    }
}
