using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BoxSponer : MonoBehaviour {

    public GameObject SponeObj;
    public static int BoxCount = 0;

    void OnTriggerEnter2D(Collider2D a)
    {
        if (a.gameObject.tag == "Player")
        {
            if (a.GetComponent<Controller2DToku>().HaveItemInstanse == null && BoxCount < 3)
            {
                BoxCount += 1;
                GameObject b = Instantiate(SponeObj);
                b.transform.position = a.transform.position;
            }

        }
    }


}
