using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController {

    public float initHpMax = 20.0f;
    public float initSpeed = 12.0f;

    protected override void Awake() {
        base.Awake();

        // パラメータ初期化
        speed = initSpeed;
        SetHp(initHpMax, initHpMax);
    }

    protected override void Update() {
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
    }

    protected override void FixedUpdateCharacter() {
        transform.localScale = new Vector3(basScaleX * dir, transform.localScale.y, transform.localScale.z);

        // 試験的
        
    }

    public override void ActionMove(float n) {
        base.ActionMove(n);
    }
}
