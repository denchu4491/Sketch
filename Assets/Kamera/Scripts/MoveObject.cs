using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
    public int verticalMoveDirection,moveSpeed;
    private float originPosition;
    public GameObject moveObject;
    private bool up, down;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        originPosition = moveObject.transform.position.y;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        VerticalMove();
	}

    void VerticalMove() {
        if(moveObject.transform.position.y <= originPosition - verticalMoveDirection) {
            up = true;
            down = false;
        }
        else if(moveObject.transform.position.y >= originPosition) {
            down = true;
            up = false;
        }
        if (up) MoveUp();
        if (down) MoveDown();
    }

    void MoveUp() {
        rb.velocity = Vector3.up * moveSpeed;
    }

    void MoveDown() {
        rb.velocity = Vector3.down * moveSpeed;
    }
}
