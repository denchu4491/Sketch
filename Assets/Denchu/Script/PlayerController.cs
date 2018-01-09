using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController {

    public float initHpMax = 20.0f;
    public float initSpeed = 12.0f;
    public float jumpPower = 10.0f;
    private static float createdDir = 0.0f;
    private bool breakEnabled = true;
    private float groundFriction = 0.0f;
    private bool isSketchStoped = false;
    
    //public readonly static int ANIST_Walk = Animator.StringToHash("Base layer.Player_Walk");
    public readonly static int ANITAG_Walk = Animator.StringToHash("Walk");

    protected override void Awake() {
        base.Awake();

        if(createdDir != 0.0f) {
            dir = createdDir;
        }
        
        // パラメータ初期化
        speed = initSpeed;
        SetHp(initHpMax, initHpMax);
    }

    protected override void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            if (isSketchStoped) {
                isSketchStoped = false;
                activeSts = true;
            } else {
                isSketchStoped = true;
                ActionStop();
            }
        }

        if (!activeSts) {
            return;
        }
        // 矢印の左右の入力検出
        //float joyMv = Input.GetAxis("Horizontal");
        float vx = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            vx -= 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            vx += 1.0f;
        }
        ActionMove(vx);

        if (Input.GetKeyDown(KeyCode.Space)) {
            ActionJump();
        }
    }

    protected override void FixedUpdateCharacter() {
        // 着地チェック
        if (jumped) {
            if((grounded && !groundedPrev) ||(grounded && Time.fixedTime > jumpStartTime + 1.0f)) {
                jumped = false;
            }
        }

        transform.localScale = new Vector3(basScaleX * dir, transform.localScale.y, transform.localScale.z);

        //移動停止処理
        if (breakEnabled) {
            speedVx *= groundFriction;
        }

        // 試験的
        Camera.main.transform.position = transform.position - Vector3.forward;
    }

    public override void ActionMove(float n) {

        if (!activeSts) {
            return;
        }

        float dirOld = dir;
        breakEnabled = false;

        if(n!= 0.0f) {
            dir = Mathf.Sign(n);
            speedVx = initSpeed * n;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //bool hoge = stateInfo.IsName("Base Layer.Player_Walk");
            if (stateInfo.tagHash != ANITAG_Walk) {
                animator.SetTrigger("Walk");
            }
        } else {
            breakEnabled = true;
            animator.SetTrigger("Idle");
        }

        if(dirOld != dir) {
            breakEnabled = true;
        }
    }

    public void ActionJump() {
        if (!grounded) return;
        jumped = true;
        jumpStartTime = Time.fixedTime;
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    public static void DirChange(float d) {
        createdDir = d;
    }

    public void ActionStop() {
        activeSts = false;
        breakEnabled = true;
        animator.SetTrigger("Idle");
    }
}
