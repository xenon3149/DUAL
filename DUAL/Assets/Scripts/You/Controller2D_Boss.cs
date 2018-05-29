using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller2D_Boss : MonoBehaviour
{

    public float modelwidth = 1;//モデル広さ
    public float modelhigh = 1;//モデル高さ   
    public float speed_set = 6;
    
    private Vector2 trans_left;//player左
    private Vector2 trans_right;//player右
    private Vector2 moveDirection;//
    private Vector2 startpos;//
    private Vector2 Point;//タッチ座標
	private Vector2 oldtranspos;
	private Vector2 Old_Position;
	ItemData HaveItem;
	public GameObject HaveItemInstanse;

    //================
    //player状態
	public bool air_int = false;
	public bool air = true;
    public bool face_left = true;
    public bool move = false;//移動中
    public bool jump = false;
    public bool have = false;//物持つ
    public bool key01 = false;
	public bool GhitNull = false;
	public bool scissors = false;
	public bool isWhite;
    //==================================

    public Rigidbody2D rb2D;
    public Animator animator;
	public Camera mainCamera;

    float speed = 6;//速度
    float movestoptime = 0f;
	float Air_time = 0f;

	//パーティクル情報
	GameObject[] _particlePrefab = new GameObject[2];
	public Vector3 _particleRotation;


    public enum Animator_player
    {
        face_left,
        move,
        jump
    }

    public enum Tag
    {
        item,
        door,
        JUMP
    }

    public enum Layer
    {
        Water
    }



    // Use this for initialization
    void Start()
    {
        speed = speed_set;
		mainCamera = Camera.main;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.freezeRotation = true;
        animator = GetComponent<Animator>();
        Input.multiTouchEnabled = true;


		//タッチ時のパーティクルプレハブの初期化
		_particlePrefab[0] = (GameObject)Resources.Load("prefabs/TouchParticleWhite");
		_particlePrefab[1] = (GameObject)Resources.Load("prefabs/TouchParticleBlack");
    }
    
    void Update()
    {
		if( isWhite == WorldColor.isWhite() )
		{
			rb2D.simulated = true;
		}
		else
		{
			rb2D.simulated = false;
		}

    }

    void FixedUpdate()
    {
        
        //タッチ座標探索
		if (GameObject.Find("Main Camera").GetComponent<Camera2DToku>().player == transform.gameObject && GameObject.Find("Main Camera").GetComponent<Camera2DToku>().anima == false)
        {
            player_animator();//animator処理
            startpos = this.transform.position;
            trans_left = new Vector2( transform.position.x - modelwidth / 2, transform.position.y);
			trans_right = new Vector2(transform.position.x + modelwidth / 2, transform.position.y);

            

            //==========================
			//タッチ移動
            //========================
			if (Input.GetMouseButtonDown(0) && Input.touchCount <= 1 )
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null )
                {
                    
					//=====================
					//パーティクルの生成
					//=====================
					if (GameObject.Find ("Main Camera").GetComponent<Camera2DToku> ().player.name == "player") {
						Instantiate (_particlePrefab [0], hit.point, Quaternion.Euler (_particleRotation));
					} else {
						Instantiate (_particlePrefab [1], hit.point, Quaternion.Euler (_particleRotation));
					}
					//==================
					//===================

					if (jump == false && !IsPointerOverGameObject() && mainCamera.GetComponent<Camera2DToku>().ob == false)
                    {
                        if (hit.point.x > transform.position.x)
                        {
                            face_left = false;
							moveDirection = new Vector2( 1f, 0f );
                        }
                        else
                        {
                            face_left = true;
							moveDirection = new Vector2( -1f, 0f );
                        }
						if (jump == false) {
							if (move) {	
								move = false;


							} else {
								move = true;
							}
						}
                        
                        Point = hit.point;
                        Point.y = this.transform.position.y;
                        //moveDirection = Point - startpos;
                        //moveDirection.Normalize();
                    } 
                }


                //==========================
                //doorを開ける
                //============================
                if (hit.collider.tag == Tag.door.ToString() && Vector2.Distance(hit.transform.position,transform.position) <= 2f)
                {
                    if (key01)
                    {
                        hit.collider.GetComponent<door>().door_open = true;
						key01 = false;
                    }
                }


            }
			//============================================
			//移動処理
			//============================================


			if ( move ) {
				rb2D.velocity = transform.localRotation * new Vector2( moveDirection.x * speed, -1f );
				//壁をぶつけた
				if ( movestoptime == 0f ) {
					oldtranspos = transform.position;
				}
				movestoptime++;
				if ( movestoptime == 10f && Vector2.Distance( transform.position, oldtranspos ) == 0f ) {
					move = false;
				}
				if ( movestoptime >= 11f ) {
					movestoptime = 0f;

				}
			} else if (!jump){
				movestoptime = 0f;
				rb2D.velocity = new Vector2 (0f, rb2D.velocity.y);
			}

			//OBの移動停止
			if ( mainCamera.GetComponent<Camera2DToku>().ob == true)
			{

				if (jump == false)
				{
					move = false;
					rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
				}

			}
            //地面探索(Ray部分)
            RaycastHit2D Ghit = Physics2D.Raycast(trans_left, Vector2.down, modelhigh / 2 + 2f, 1 << LayerMask.NameToLayer(Layer.Water.ToString()));
			RaycastHit2D Ghit1 = Physics2D.Raycast(transform.position, Vector2.down, modelhigh / 2 + 2f, 1 << LayerMask.NameToLayer(Layer.Water.ToString()));
            RaycastHit2D Ghit2 = Physics2D.Raycast(trans_right, Vector2.down, modelhigh / 2 + 2f, 1 << LayerMask.NameToLayer(Layer.Water.ToString()));
			if (Ghit1.collider != null)
			{
				rb2D.velocity = Ghit1.collider.transform.localRotation * rb2D.velocity;
				/*if (Ghit.normal == Ghit1.normal &&  Ghit1.normal == Ghit2.normal ){
					if (Ghit1.normal.x > 0)
					{
						transform.localEulerAngles = new Vector3(0, 0, -Vector2.Angle(Ghit1.normal, new Vector2(0, 1)));
					} else
					{
						transform.localEulerAngles = new Vector3(0, 0, Vector2.Angle(Ghit1.normal, new Vector2(0, 1)));
					}
				}*/
			}
			if (Ghit1.distance <= modelhigh / 2 + 0.1f && Ghit1.distance != 0f) {
				air_int = true;
			} else {
				air_int = false;
			}

			//空中にいるなら立ち状態に回復
			if (air)
            {
				Air_time++;
                if (jump == false) {
					move = false;
				}
            }

            //============================================
            //箱を持つspeed
            if (have)
            {
				if (Ghit.collider == null || Ghit2.collider == null) {
					transform.position = Old_Position;
					GhitNull = true;
					//move = false;
				} else {
					GhitNull = false;
					Old_Position = transform.position;
				}
                speed = speed_set / 3;
            }
            else
            {
                speed = speed_set;
            }
        }
        //Debug.Log (airtime);

    }
    //================================================
    //地面探索(collider)
    void OnCollisionStay2D(Collision2D water) {
		if (water.gameObject.layer == 4) {
			
			air = false;
			Air_time = 0f;
		}
	}
	void OnCollisionExit2D(Collision2D water) {

        
        if (water.gameObject.layer == 4 && air_int == false) {
			air = true;
		}
	}

	void ItemGet(GameObject Item)
	{
		ItemData id = Item.GetComponentInParent<ItemData>();
		if (id.GetItem && HaveItem == null)
		{
			HaveItem = id;
			HaveItemInstanse = id.gameObject;
			id.gameObject.SetActive(false);

		}
	}

	public void ItemRelease()
	{
		if (HaveItem != null)
		{
			HaveItemInstanse.SetActive(true);
			float posx = -1.5f;
			if (!face_left)
			{
				posx *= -1;
			}
			HaveItemInstanse.transform.position = new Vector3(posx + this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
			HaveItem = null;
			HaveItemInstanse = null;
		}
	}


    //==============================================
    //JUMP
    //============================================
    void OnCollisionEnter2D(Collision2D water)
    {
        if (water.gameObject.layer == 4)
        {
            jump = false;
            air = false;
			Air_time = 0f;
        }
    }
    
        void OnTriggerEnter2D(Collider2D order)
    {
		
		if (order.gameObject.tag == "Damege")
		{
			move = false;
			ResultUIControll._withdrawalNumber += 1;
			//float x = 5;
			if (face_left)
			{
				//x *= -1;
			}
			rb2D.velocity = new Vector2(0, 5);
		}

		if (order.gameObject.tag == "item")
		{
			ItemGet(order.gameObject);
		}        



		if (order.tag == Tag.JUMP.ToString() && air == false && have == false && jump == false)
        {
            jump = true;
            animator.SetBool(Animator_player.jump.ToString(), true);
            move = false;
            rb2D.velocity = new Vector2(0f , 0f);
            rb2D.velocity = new Vector2(order.gameObject.GetComponent<JUMP>().xspeed, order.gameObject.GetComponent<JUMP>().yspeed);
            if (order.gameObject.GetComponent<JUMP>().xspeed >= 0f)
            {
                face_left = false;
            }
            else
            {
                face_left = true;
            }
        }
        //============================================
    }

    //===========================================
    //player_animator
    //===========================================
    private void player_animator()
    {
        if (move)
        {
            animator.SetBool(Animator_player.move.ToString(), true);
        }
        else
        {
            animator.SetBool(Animator_player.move.ToString(), false);
        }

        if (face_left)
        {
            animator.SetBool(Animator_player.face_left.ToString(), true);
        }
        else
        {
            animator.SetBool(Animator_player.face_left.ToString(), false);
        }

        if (jump)
        {
            animator.SetBool(Animator_player.jump.ToString(), true);
        }
        else
        {
            animator.SetBool(Animator_player.jump.ToString(), false);
        }
    }
	// RayはUIの後ろに届けない
	public static bool IsPointerOverGameObject() {  
		PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);  
		eventData.pressPosition = Input.mousePosition;  
		eventData.position = Input.mousePosition;  

		List<RaycastResult> list = new List<RaycastResult>();  
		UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);  
		return list.Count > 0;  
	}  
}