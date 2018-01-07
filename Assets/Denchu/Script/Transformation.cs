using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour {

    public GameObject[] prefab;
    public BoxCollider2D setCol, releaseCol;
    private float createdTime;
    //private int createCnt = 0;
    private bool canCreate;

    void Awake() {
        createdTime = Time.fixedTime;
        canCreate = true;
    }

    void OnTriggerEnter2D(Collider2D col) {
        //if (createCnt != 0) return;

        if (col.tag == "Trans" && setCol.IsTouching(col)) {
            if (createdTime + 1.0f > Time.fixedTime) {
                canCreate = false;
                return;
            }

            if (prefab[0] != null && canCreate) {
                //createCnt++;
                PlayerController.DirChange(Mathf.Sign(transform.localScale.x));
                Instantiate(prefab[0], transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Trans" && !releaseCol.IsTouching(col)) {
            canCreate = true;
        }
    }

}
