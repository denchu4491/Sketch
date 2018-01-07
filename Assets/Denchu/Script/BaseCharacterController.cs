using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour {

    protected float hp = 10.0f;
    protected float hpMax = 10.0f;
    protected float dir;
    protected float speed = 1.0f;
    protected float basScaleX = 1.0f;
    protected float speedVx = 0.0f;
    protected Rigidbody2D rb;
    protected bool jumped = false;
    protected bool grounded = false;
    protected bool groundedPrev = false;
    protected bool activeSts = false;

    protected Animator animator;
    protected Transform groundCheck_L;
    protected Transform groundCheck_C;
    protected Transform groundCheck_R;

    protected float jumpStartTime = 0.0f;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck_L = transform.Find("GroundCheck_L");
        groundCheck_C = transform.Find("GroundCheck_C");
        groundCheck_R = transform.Find("GroundCheck_R");

        dir = (transform.localScale.x > 0.0f) ? 1 : -1;
        basScaleX = Mathf.Abs(transform.localScale.x * dir);
        transform.localScale = new Vector3(basScaleX, transform.localScale.y, transform.localScale.z);

        activeSts = true;
    }

	protected virtual void Start () {
		
	}
	
	protected virtual void Update () {
		
	}

    protected virtual void FixedUpdate() {
        // 地面チェック
        groundedPrev = grounded;
        grounded = false;
        Collider2D[][] groundCheckCollider = new Collider2D[3][];

        groundCheckCollider[0] = Physics2D.OverlapPointAll(groundCheck_C.position);
        groundCheckCollider[1] = Physics2D.OverlapPointAll(groundCheck_L.position);
        groundCheckCollider[2] = Physics2D.OverlapPointAll(groundCheck_R.position);
        // こっちのが高速だけど配列の取り方によっては怖い
        /*
        for (int i = 0; i < 3; i++) {
            groundCheckCollider[i] = new Collider2D[10];
        }
        Physics2D.OverlapPointNonAlloc(groundCheck_L.position, groundCheckCollider[0]);
        Physics2D.OverlapPointNonAlloc(groundCheck_C.position, groundCheckCollider[1]);
        Physics2D.OverlapPointNonAlloc(groundCheck_R.position, groundCheckCollider[2]);
        */

        foreach (Collider2D[] groundCheckList in groundCheckCollider) {
            if (groundCheckList != null) {
                foreach (Collider2D groundCheck in groundCheckList) {
                    if (groundCheck != null) {
                        if (!groundCheck.isTrigger) {
                            grounded = true;
                        }
                    }
                }
            }
        }

        FixedUpdateCharacter();

        rb.velocity = new Vector2(speedVx, rb.velocity.y);
        //Debug.Log(rb.velocity);
    }

    protected virtual void FixedUpdateCharacter() {
        
    }

    public virtual void ActionMove(float n) {
        if (n != 0.0f){
            dir = Mathf.Sign(n);
            speedVx = speed * n;
            animator.SetTrigger("Walk");
        } else {
            speedVx = 0;
            animator.SetTrigger("Idle");
        }
    }

    public virtual bool SetHp(float _hp, float _hpMax) {
        hp = _hp;
        hpMax = _hpMax;
        return (hp <= 0);
    }

    public virtual void Dead() {
        if (!activeSts) {
            return;
        }
        rb.velocity = Vector2.zero;
        rb.Sleep();
        activeSts = false;
    }

}
