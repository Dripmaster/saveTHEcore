using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    Transform targetPos;
    float bulletSpeed= 10;
    bool trail  = false;
    guardianFSM gFSM;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != null) {
            transform.position = Vector3.MoveTowards(transform.position ,targetPos.transform.position,bulletSpeed*Time.deltaTime);
        }
    }
    public void bulletInit(Transform t, guardianFSM g) {
        targetPos = t;
        gFSM = g;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (targetPos == null || other == null)
            return;
        if (GameObject.ReferenceEquals(targetPos.gameObject, other.gameObject)) {
            gFSM.attackCommand();
            Destroy(gameObject);
        }
    }
}
