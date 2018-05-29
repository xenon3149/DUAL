using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_1 : EnemyData {
    int HP = 3;
    Animator anim;
    bool TarnFlag;

	//public GameObject _resultUI;	仕様変更：ボス討伐後、リザルトUIでなくメモリストが表示されるようになりました
	//public GameObject _UIManager;

	[SerializeField] private GameObject _memo = null;

    void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine(Routine());
    }
     public IEnumerator Routine() {
        while (true)
        {
            yield return new WaitForSeconds(2);
            while (true)
            {

                Vector3 PPos = PlayerPos();
                if ( Mathf.Abs(PPos.x - transform.position.x) < 15 && WorldColor.isWhite() == isWhite)
                {
                    float x = 10f;
                    if (!sp.flipX)
                    {
                        x *= -1;
                    }
                    rb.velocity = new Vector2(x, 20);
                    break;
                }
                yield return null;
            }

            anim.SetInteger("State",1);
            yield return WaitTime(4f);
            Vector3 CheckPos = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            TarnFlag = false;
            while (true)
            {
				if (WorldColor.isWhite() == isWhite && Time.timeScale == 1 )
                {
                    float x = 0.2f;
                    if (sp.flipX)
                    {
                        x *= -1;
                    }
                    transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
                    if (Mathf.Abs(CheckPos.x - transform.position.x) < 0.1f)
                    {
                        if (HP == 1 && !TarnFlag)
                        {
                            sp.flipX = !sp.flipX;
                            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                            rb.velocity = new Vector2(0, 20);
                            TarnFlag = true;
                            yield return WaitTime(1f);
                            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                        }
                        else
                        {
                            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                            rb.velocity = new Vector2(0, 10);
                            anim.SetInteger("State", 2);
                            break;
                        }

                    }
                    CheckPos = transform.position;
                }
                else
                {
                    Debug.Log(rb.velocity);
                }
                yield return null;
            }
            sp.flipX = !sp.flipX;
            yield return WaitTime(4);
            anim.SetInteger("State", 0);
            yield return WaitTime(1);
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(6);
		//_resultUI.SetActive (true);
		_memo.SetActive(true);
		//Time.timeScale = 0f;
		//_UIManager.GetComponent<ResultUIControll> ().ResultDisplay ();
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D( Collision2D a )
    {
        
        if ( a.gameObject.tag == "item")
        {
            if( ItemData.ItemType.SmallBox == a.gameObject.GetComponent< ItemData >().Type)
            {
                if (anim.GetInteger("State") == 2)
                {
                    HP -= 1;
                    if (HP == 0)
                    {
                        StopAllCoroutines();
                        rb.velocity = new Vector2(0, 10);
                        rb.constraints = rb.constraints = RigidbodyConstraints2D.FreezeRotation;
						_memo.transform.parent = null;
                        anim.SetInteger("State", 3);
                        StartCoroutine(Death());
                    }
                    else
                    {
                        rb.constraints = rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rb.velocity = new Vector2(0, 10);
                        anim.SetInteger("State", 4);
                    }
                }
                BoxSponer.BoxCount -= 1;
                Destroy(a.gameObject);
            }
        }
    }

}
