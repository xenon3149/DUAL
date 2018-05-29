using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour {

    public bool isWhite;
    // Use this for initialization
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sp;
    public float Speed = 5;
    bool isRight;
    protected bool ActiveFlag;
    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected IEnumerator WaitTime(float time)
    {
        while (time > 0)
        {
            if ( isWhite == WorldColor.isWhite())
            {
                time -= UnityEngine.Time.deltaTime;
            }
            yield return new WaitForSeconds(UnityEngine.Time.deltaTime);
        }
    }

    public Vector3 PlayerPos()
    {
        Vector3 pos = Vector3.zero;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject a in players)
        {
            if (a.GetComponent<Rigidbody2D>().simulated )
            {
                pos = a.transform.position;
            }
        }
            return pos;
        }

    void Move()
    {

    }

}
