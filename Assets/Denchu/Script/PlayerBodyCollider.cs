using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour {

    PlayerController playerCtrl;

    void Awake() {
        playerCtrl = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "DeathZone") {
            //Debug.Log("GameOver");
            playerCtrl.Dead();
        }
    }

}
