using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam2D : MonoBehaviour {
    public Transform target;
    public float ofsetY;
    public float ofsetX;
    // Update is called once per frame
    void Start()
    {
        this.transform.position = new Vector3(target.position.x + ofsetX, target.position.y + ofsetY, this.transform.position.z);
    }
    void LateUpdate () {
        this.transform.position = new Vector3(target.position.x + ofsetX, target.position.y + ofsetY, this.transform.position.z);
        
    }
}
