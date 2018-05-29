using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera2D : MonoBehaviour {
    public GameObject player;
    public Rigidbody2D playerRigidbody2D;
    public GameObject NowStory;
    private GameObject specially;//アニメーション
    private GameObject specially_cloo;
    private GameObject specially_cloo2;
    private GameObject item;//アイテム
    public  GameObject world_white;//白世界
    public  GameObject world_black;//黒世界
    public Camera mainCamera;
    public int Restart = 0;
    public int Teleport = 0;
    //public int Restartnum { get{ return Restart; } }
    

    public bool anima = false;
    public bool anima2 = false;
    public bool ob = false;
    public bool World_lock = false;

    public float smoothRate = 0.5f;
    public float border_x = 15f;
    public float border_y = 5f;


    private Transform thisTransform;
    private Vector2 velocity;
    private Vector2 item_Point;
    private Vector2 touchpos1;
    private Vector2 touchpos2;
    private Vector2 endpos1;
    private Vector2 endpos2;
	Vector2 OldPosition;
    private Vector3 world_distance;//黒白世界の距離
    private Vector3 camera_position;
    //private Vector3 localScale_speed;
    private float time = 0;
    private float time2 = 0;
    //private float distance;
    private float time_add = 0;


    static int Restart_static;
    static int Teleport_static;
    static float Count_time_static;


    public enum Player
    {
        player,
        player2
    }

    public enum Effect
    {
        white,
        black

    }

    public enum Tag
    {
        item
    }

    public enum UiButton
    {
        OrbButton,
        TimeText
    }


    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        world_distance = world_black.transform.position - world_white.transform.position;
        player = GameObject.Find(Player.player.ToString());
        specially = GameObject.Find(Effect.white.ToString());
        Input.multiTouchEnabled = true;// 複数の
        thisTransform = transform;
        velocity = new Vector2(0.5f, 0.5f);

        //=====================================
        //クリアDataを記録
        Restart = Restart_static;
        Teleport = Teleport_static;
        //GameObject.Find(UiButton.TimeText.ToString()).GetComponent<time>().countTime = Count_time_static;


    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム停止(パソコン用)
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    void FixedUpdate() {
        //キャメラ移動
        Vector2 newPos2D = Vector2.zero;
        camera_position = transform.position;
        camera_position.z = -4;
        newPos2D.x = Mathf.SmoothDamp(thisTransform.position.x, player.transform.position.x, ref velocity.x, smoothRate);

        newPos2D.y = Mathf.SmoothDamp(thisTransform.position.y, player.transform.position.y + 2, ref velocity.y, smoothRate);

        Vector3 newPos = new Vector3(newPos2D.x, newPos2D.y, transform.position.z);

        //キャメラ制限
        if (newPos.x <= -border_x)
        {
            newPos = new Vector3(-border_x, newPos.y, newPos.z);
        } else if (newPos.x >= border_x)
        {
            newPos = new Vector3(border_x, newPos.y, newPos.z);
        }

        if (player == GameObject.Find(Player.player.ToString()))
        {
            if (newPos.y >= border_y)
            {
                newPos = new Vector3(newPos.x, border_y, newPos.z);
            }
        }else
        {
            if (newPos.y >= border_y - world_distance.y)
            {
                newPos = new Vector3(newPos.x, border_y - world_distance.y, newPos.z);
            }
        }


        transform.position = Vector3.Slerp(transform.position, newPos, Time.time);
        //==============================


        if (anima == false)
        {
            MoblieInput();
            //テスト
            //世界を変わる(パソコンTest用)
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (player == GameObject.Find(Player.player.ToString()))
                {
                    player = GameObject.Find(Player.player2.ToString());
                    specially = GameObject.Find(Effect.black.ToString());
                }
                else
                {
                    player = GameObject.Find(Player.player.ToString());
                    specially = GameObject.Find(Effect.white.ToString());
                }
            }
            if (Input.GetKeyDown(KeyCode.X) && specially_cloo == null)
            {
                specially_cloo = Instantiate(specially, camera_position, transform.rotation) as GameObject;
                specially_cloo.transform.localScale = new Vector3(0.01f, 0.01f, 0);
                time_add += 1f;
                specially_cloo.transform.parent = transform;
                if (World_lock == false)
                {
                    anima = true;
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                RestartScene();
            }


            //itemを判断する

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (ob == true && hit.collider.tag == Tag.item.ToString() && anima2 == false)
                {
                    item = hit.collider.gameObject;
                    item_Point = hit.collider.transform.position;
                    specially_cloo2 = Instantiate(specially, item_Point, transform.rotation) as GameObject;
                    specially_cloo2.transform.localScale = new Vector3(0.01f, 0.01f, 1);
                    anima2 = true;
                    Teleport++;
                }
            }

        }





        //アニメーション処理
        //世界変換
        if (anima)
        {
            if (time == 0f && player.GetComponent<Controller2D>().air == false)
            {
                player.GetComponent<Controller2D>().rb2D.velocity = new Vector2(0, 0);
            }			
            time += 1f;
            if (time <= 32.5f)
            {
                specially_cloo.transform.localScale += new Vector3(0.4f, 0.4f, 0);
            }
            else if (time <= 65f + time_add / 4)
            {
                specially_cloo.transform.localScale -= new Vector3(0.4f, 0.4f, 0);
            }
            if (time == 25f)
            {

                if (player == GameObject.Find(Player.player.ToString()))
                {
                    player = GameObject.Find(Player.player2.ToString());
                    specially = GameObject.Find(Effect.black.ToString());
                }
                else
                {
                    player = GameObject.Find(Player.player.ToString());
                    specially = GameObject.Find(Effect.white.ToString());
                }
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5f);
            }
            if (time >= 65f + time_add / 4)
            {
                Destroy(specially_cloo);
                time = 0;
                time_add = 0;
                anima = false;
            }
        }
        //アイテム転移
        if (anima2)
        {

            time2 += 1f;
			if (time2 == 1f) {
				OldPosition = item.transform.position;
			}
            if (time2 <= 5f)
            {
                specially_cloo2.transform.localScale += new Vector3(0.2f, 0.2f, 0);
            } else
            {
                specially_cloo2.transform.localScale -= new Vector3(0.2f, 0.2f, 0);
            }
            if ( time2 == 5f)
            {
				ResultUIControll._transitionNumber++;


				HideMemo hidememo = GameObject.Find ("MemoListManager").GetComponent<HideMemo> ();
				if ( hidememo ) {	//隠されたメモリストが存在するときに行う処理
					if ( hidememo.GetItem() == item ) {
						hidememo.DisplayMemo ();
					}
				}


                if (player == GameObject.Find(Player.player.ToString()))
                {
                    item.transform.position -= world_distance;
                    ob = false;
                    GameObject.Find(UiButton.OrbButton.ToString()).GetComponent<Image>().sprite = GameObject.Find(UiButton.OrbButton.ToString()).GetComponent<UI>().off;


                }
                else
                {
                    item.transform.position += world_distance;
                    ob = false;
                    GameObject.Find(UiButton.OrbButton.ToString()).GetComponent<Image>().sprite = GameObject.Find(UiButton.OrbButton.ToString()).GetComponent<UI>().off;
                }



            }
			if (time2 == 6) {
				if (item.GetComponent<Box2D> ()) {	//itemが箱の場合の処理
					if (item.GetComponent<Box2D> ().WallHave == true) {
						item.transform.position = OldPosition;
						item.GetComponent<Box2D> ().WallHave = false;
					}
				}
				if (item.GetComponent<Scissors> ()) {	//itemがハサミの場合の処理
					item.GetComponent<Scissors> ().ChangeScissorsColor ();
				}
			}

            if (time2 >= 10f)
            {
                Destroy(specially_cloo2);
				time2 = 0f;
				anima2 = false;
				item = null;
            }

        }


    }

    //スマホコントローラー
    void MoblieInput()
    {
        //===============================
        //anima削除

        if (Input.touchCount == 0  )
        {
            if (anima == false && specially_cloo != null)
            {
                Destroy(specially_cloo);
            }
            if (anima2 == false && specially_cloo2 != null)
            {
                Destroy(specially_cloo2);
            }

        }
        //====================================================
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchpos1 = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                endpos1 = Input.GetTouch(0).deltaPosition;
            } 
        }

            if (Input.touchCount > 1 && anima == false)
        {
            
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchpos1 = Input.GetTouch(0).position;               
            }
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                touchpos2 = Input.GetTouch(1).position;
                if (player.GetComponent<Controller2D>().air == false)
                {
                    player.GetComponent<Controller2D>().rb2D.velocity = new Vector2(0, 0);
                }
                if (specially_cloo == null && World_lock == false) {
					specially_cloo = Instantiate (specially, camera_position, transform.rotation) as GameObject;
				}
                if (specially_cloo != null)
                {
                    specially_cloo.transform.localScale = new Vector3(0.01f, 0.01f, 0);
                    specially_cloo.transform.parent = transform;
                }
            }
          
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                endpos1 = Input.GetTouch(0).position;
                endpos2 = Input.GetTouch(1).position;
                if (Vector2.Distance(endpos2, endpos1) - Vector2.Distance(touchpos2, touchpos1) > 0)
                {
                    specially_cloo.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                if (Vector2.Distance(endpos2, endpos1) - Vector2.Distance(touchpos2, touchpos1) >= 3f)
                {

                    if (World_lock == false)
                    {
                        anima = true;
                    }
                }
                else
                {
                    Destroy(specially_cloo);
                }
                touchpos1 = Vector2.zero;
                endpos1 = Vector2.zero;
                touchpos2 = Vector2.zero;
                endpos2 = Vector2.zero; 
            }
        }


    }

    public void RestartScene()
    {
        Restart++;
        Restart_static = Restart;
        Teleport_static = Teleport;
        //Count_time_static = GameObject.Find(UiButton.TimeText.ToString()).GetComponent<time>().countTime;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void TimeStop()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void World_Lock()
    {
        if (World_lock)
        {
            World_lock = false;
        }else
        {
            World_lock = true;
        }
    }

}
