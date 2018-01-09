using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBodyCollider : MonoBehaviour {

    PlayerController playerCtrl;

    void Awake() {
        playerCtrl = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "DeathZone") {
            //Debug.Log("GameOver");
            playerCtrl.Dead();
            SceneManager.LoadScene("GameOver");
        }
        else if(col.tag == "Clear") {
            SceneManager.LoadScene("Clear");
        }
    }

}
