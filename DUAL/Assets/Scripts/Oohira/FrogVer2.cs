using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//機能：カエルの機能を持つスクリプト(Frog.csの修正版)
//
//アタッチ：Frog.csでは対応できないカエルにアタッチ
//
public class FrogVer2 : MonoBehaviour {

	const int WAIT_TIME = 120;		//ジャンプするまでの間隔
	int _waitTime = WAIT_TIME;
	Rigidbody2D _rig;
	Animator _anim;
	[SerializeField] GameObject _item = null;		//カエルの子のオブジェクト(消えたときに出現するもの)
	[SerializeField] Vector2[] _jumpSpeed = new Vector2[6];
	[SerializeField] Vector2[] _jumpEvent = new Vector2[1];
	int _jumpSpeedIndex = 0;
	int _jumpEventIndex = 0;
	bool _eventFlag = true;		//Jump_EventをJump_Speedに代入できるかどうかを判定(OnCollisionEnter2Dが複数回同時に呼ばれてしまうため用意)
	[SerializeField] bool _faceLeft = true;
	[SerializeField] bool _jump = false;

	public enum Animator_Forg
	{
		face_left,
		jump
	}


	// Use this for initialization
	void Start () {
		/*Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();*/
		_rig = GetComponent<Rigidbody2D> ();
		_anim = GetComponent<Animator> ();
	}


	// Update is called once per frame
	void Update () {
		_waitTime--;
		player_animator ();
		switch (_waitTime) {
		case 30:	//ジャンプをする30フレーム前からアニメーション開始
			_jump = true;
			break;
		case 0:
			_rig.velocity = _jumpSpeed [_jumpSpeedIndex];
			_eventFlag = true;
			_jumpSpeedIndex = (_jumpSpeedIndex + 1) % _jumpSpeed.Length;
			_waitTime = WAIT_TIME;
			break;
		default:
			break;
		}
		if (_jumpSpeed[_jumpSpeedIndex].x > 0f) {
			_faceLeft = false;
		} else {
			_faceLeft = true;
		}
	}


	void OnCollisionEnter2D(Collision2D wall) {
		if (wall.gameObject.tag == "wall") {
			_rig.velocity = new Vector2 (0,0);
			_jump = false;
		}
	}

	void OnTriggerEnter2D(Collider2D Event) {
		if (Event.name == "Frog_Event" && _eventFlag) {
			_jumpSpeed[_jumpSpeedIndex] = _jumpEvent[_jumpEventIndex];
			_jumpEventIndex = (_jumpEventIndex + 1) % _jumpEvent.Length;
			_eventFlag = false;
			Debug.Log ("EventJump!");
		}
		if (Event.name == "Frog_Event_Delete") {
			_item.SetActive (true);
			_item.transform.parent = null;
			Destroy (gameObject);
		}
		if (Event.name == "Frog_velocityZero") {
			_rig.velocity = new Vector2 (0, 0);
			_jump = false;
		}
	}

	void player_animator() {
		if (_faceLeft) {
			_anim.SetBool(Animator_Forg.face_left.ToString(), true);
		} else {
			_anim.SetBool(Animator_Forg.face_left.ToString(), false);
		}
		if (_jump) {
			_anim.SetBool(Animator_Forg.jump.ToString(), true);
		} else {
			_anim.SetBool(Animator_Forg.jump.ToString(), false);
		}
	}

}
