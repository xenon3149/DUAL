using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2D : MonoBehaviour {

	public bool WallHave = false;
    public float modelwidth = 1;//モデル太さ
    public float modelhigh = 1;//モデル高さ
    public float Get_range = 1f;
    private Vector2 trans_left;//モデル左
    private Vector2 trans_right;//モデル右
	private Vector2 Old_Position;


    
    private Rigidbody2D rb2d;

    //player 
    FixedJoint2D fj2d;//鎖
    GameObject player;
    GameObject player_old;
    Rigidbody2D playerbody;

    public enum Layer
    {
        Water
    }
	public enum Tag
	{
		wall
	}


    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        fj2d = GetComponent<FixedJoint2D>();
        fj2d.connectedBody = rb2d;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        player = GameObject.Find("Main Camera").GetComponent<Camera2D>().player;
        playerbody = player.GetComponent<Rigidbody2D>();
		transform.localEulerAngles = new Vector3(0, 0, 0);//回転禁止
        //地面探索
        trans_left = transform.position;
        trans_right = transform.position;
        trans_left.x = transform.position.x - modelwidth / 2;
        trans_right.x = transform.position.x + modelwidth / 2;

        RaycastHit2D Ghit = Physics2D.Raycast(trans_left, Vector2.down, modelhigh / 2 + 0.2f, 1 << LayerMask.NameToLayer(Layer.Water.ToString()));
        RaycastHit2D Ghit2 = Physics2D.Raycast(trans_right, Vector2.down, modelhigh / 2 + 0.2f, 1 << LayerMask.NameToLayer(Layer.Water.ToString()));


		if (player_old != null && fj2d.connectedBody == playerbody) {
			if (player_old.GetComponent<Controller2D> ().GhitNull == false) {				
				Old_Position = transform.position;
			} else {
				transform.position = Old_Position;
			}
			if (Ghit.collider == null && Ghit2.collider == null)
			{
				Fj_off();
			}
		}


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit2.collider != null)
            {
				if (hit2.collider.gameObject == transform.gameObject && Vector2.Distance(new Vector2(0f,transform.position.y), new Vector2(0f,player.transform.position.y)) <= modelhigh / 2 && Vector3.Distance(transform.position, player.transform.position) <= modelwidth / 2 + player.GetComponent<Controller2D>().modelwidth / 2 + Get_range)
                {

                    playerbody.velocity = player.transform.localRotation * new Vector2(0, 0); ;
                    if (fj2d.connectedBody == rb2d && player.GetComponent<Controller2D>().have == false)
                    {
                        player.GetComponent<Controller2D>().have = true;
                        fj2d.connectedBody = playerbody;
                        rb2d.constraints = RigidbodyConstraints2D.None;
                        rb2d.freezeRotation = true;
                        player_old = player; //持つ時のplayerを記録        
                    }
                    else
                    {
                        player.GetComponent<Controller2D>().have = false;
                        fj2d.connectedBody = rb2d;
                        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
                    }
                }
            }
        }
    }

    private void Fj_off()
    {
        
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;//回転禁止
        fj2d.connectedBody = rb2d;
        player_old.GetComponent<Controller2D>().have = false;
        player_old.GetComponent<Controller2D>().move = false;
    }


	void OnTriggerEnter2D(Collider2D wall) {
		if (wall != null) {
			Debug.Log (wall.name);	
			if (wall.tag == Tag.wall.ToString()) {
				WallHave = true;
			}
		}
	}
}
