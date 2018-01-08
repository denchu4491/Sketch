using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkecthBlock : BaseSketch {

    public Vector2 topLeftSketchPos, BottomRightSketchPos;
    private int existBlockCount;
    private float existBlockTime;
    BoxCollider2D originBlock = new BoxCollider2D();
    BoxCollider2D[][] block = new BoxCollider2D[rowBlockMaxCount + 1][];
    [SerializeField] private GameObject prefab;

    private void Initialize() {
        existBlockCount = 0;
        SetSketchMode();
        DestroyBlock();
        ResetTime();
    }

    private bool CheckCollision(Vector2 position) {
        var isCollision = Physics2D.OverlapBox(position, new Vector2(blockSizeX * 0.9f, blockSizeY * 0.9f), 0);
        return (isCollision != null);
    }

    private void CreateBlock(int blockIndX,int blockIndY) {
        Vector2 position = new Vector2(topLeftSketchPos.x + blockSizeX * blockIndX + (blockSizeX / 2), topLeftSketchPos.y + blockSizeY * blockIndY + (blockSizeY / 2));
        if (blockIndX < 0 || blockIndX > columnBlockMaxCount || blockIndY < 0 || blockIndY > rowBlockMaxCount) return;
        if (CheckCollision(position)) return;
        existBlockCount++;
        block[blockIndY][blockIndX] = Instantiate(originBlock);
        BoxCollider2D box2D = block[blockIndY][blockIndX].GetComponent<BoxCollider2D>();
        box2D.transform.localScale = new Vector2(1.25f * blockSizeX, 1.25f * blockSizeY);
        box2D.transform.position = position;
    }


    private void DestroyBlock() {
        var clones = GameObject.FindGameObjectsWithTag("block");
        foreach(var clone in clones) {
            Destroy(clone);
        }
    }


    private void SetSketchMode() {
        topLeftSketchPos = GetScreenTopLeft(mainCamera);
        BottomRightSketchPos = GetScreenBottomRight(mainCamera);
    }

    public void Sketch() {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (existBlockCount >= existBlockMaxCount) return;
        if (mousePosition.x < topLeftSketchPos.x || mousePosition.x > BottomRightSketchPos.x || mousePosition.y < topLeftSketchPos.y || mousePosition.y > BottomRightSketchPos.y) return;
        if (Input.GetMouseButton(0)) {
            CreateBlock((int)((mousePosition.x - topLeftSketchPos.x) / blockSizeX), (int)((mousePosition.y - topLeftSketchPos.y) / blockSizeY));
        }
    }

    private void CountTime() {
        existBlockTime += Time.deltaTime;
    }

    private void ResetTime() {
        existBlockTime = 0f;
    }

	// Use this for initialization
	protected override void Start () {
        base.Start();
        originBlock = prefab.GetComponent<BoxCollider2D>();
        for (int i = 0; i <= rowBlockMaxCount; ++i) block[i] = new BoxCollider2D[columnBlockMaxCount + 1];
        ResetTime();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if ((Input.GetKeyDown(KeyCode.X)) || existBlockTime >= existBlockMaxTime) {
            Debug.Log(isSketchable);
            Initialize();
        }
        if (isSketchable) {
            Sketch();
        }
        else if (!isSketchable) CountTime();
	}
}
