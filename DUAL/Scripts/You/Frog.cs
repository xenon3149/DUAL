using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

    int Ai_Time = 0;
    Rigidbody2D Rigidbody2D;
	Animator animator;
	public GameObject Item;
    public Vector2 Jump_Speed; 
    public int Jump_Time = 120;
    public int Jump_Count = 3;
	public int Switchanima_Time = 0;
	public bool face_left = true;
	public bool jump = false;

    public Vector2 Jump_Event;
    public bool Event_left = false;
    public bool Event_right = false;

    Vector2 Old_Speed;
	bool Switchanima_Event = false;
    int Old_Time;
    int Old_Count;
    public enum Animator_Forg
	{
		face_left,
		jump
	}

	// Use this for initialization
	void Start () {
        Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        Old_Speed = Jump_Speed;
		Old_Time = Switchanima_Time;
        Old_Count = Jump_Count;
    }
	
	// Update is called once per frame
	void Update () {
		player_animator( );

		if (Switchanima_Event){
			Switchanima_Time--;
		}else{
        	Ai_Time++;
		}

		if (Switchanima_Time <= 0){
			Switchanima_Event = false;
			Switchanima_Time = Old_Time;
		}

		if ( Ai_Time % Jump_Time == Jump_Time - 30 ) {
			jump = true;
		}
        if (Ai_Time % Jump_Time == 0)
        {
            Rigidbody2D.velocity = Jump_Speed;
            Jump_Count--;
        }
		if ( Jump_Speed.x > 0f ) {
			face_left = false;
		} else {
			face_left = true;
		}
	}



    void OnCollisionEnter2D(Collision2D wall)
    {
        if (wall.gameObject.layer == 4)
        {
            Rigidbody2D.velocity = new Vector2(0, 0);
			jump = false;
            if (Jump_Count <= 0)
            {
                Jump_Speed.x *= -1;
                Old_Speed.x *= -1;
                Jump_Count = Old_Count;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D Event)
    {
        if (Event.name == "Frog_Event")
        {
            
            
            if (Event_left == true && face_left == true) {
                Jump_Speed = Jump_Event;
				Rigidbody2D.velocity = new Vector2(0, 0);
                
            }
            if(Event_right == true && face_left == false )
            {
                Jump_Speed = Jump_Event;
				Rigidbody2D.velocity = new Vector2(0, 0);
            }
			
        }

        if (Event.name == "Frog_Event_Delete")
        {
			Item.SetActive( true );
			Item.transform.parent = null;
            Destroy(gameObject);
        }

		if (Event.name == "switch_white" || Event.name == "switch_black"){
			Switchanima_Event = true;
		}

    }

    void OnTriggerExit2D(Collider2D Event)
    {
        if (Event.name == "Frog_Event")
        {
            Jump_Speed = Old_Speed;
        }

    }


    private void player_animator() {
		if (face_left)
		{
			animator.SetBool(Animator_Forg.face_left.ToString(), true);
		}
		else
		{
			animator.SetBool(Animator_Forg.face_left.ToString(), false);
		}
		if (jump)
		{
			animator.SetBool(Animator_Forg.jump.ToString(), true);
		}
		else
		{
			animator.SetBool(Animator_Forg.jump.ToString(), false);
		}


	}

}
