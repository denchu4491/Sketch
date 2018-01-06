using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour {

    protected float hp = 10.0f;
    protected float hpMax = 10.0f;
    protected float dir = 1.0f;
    protected float speed = 1.0f;
    protected float basScaleX = 1.0f;
    protected float speedVx = 0.0f;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = (transform.localScale.x > 0.0f) ? 1 : -1;
        basScaleX = transform.localScale.x * dir;
        transform.localScale = new Vector3(basScaleX, transform.localScale.y, transform.localScale.z);
    }

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    protected virtual void FixedUpdate() {
        FixedUpdateCharacter();

        rb.velocity = new Vector2(speedVx, rb.velocity.y);
        Debug.Log(rb.velocity);
    }

    protected virtual void FixedUpdateCharacter() {
        
    }

    public virtual void ActionMove(float n)
    {
        if (n != 0.0f){
            dir = Mathf.Sign(n);
            speedVx = speed * n;
        } else {
            speedVx = 0;
        }
    }

    public virtual bool SetHp(float _hp, float _hpMax) {
        hp = _hp;
        hpMax = _hpMax;
        return (hp <= 0);
    }

}
