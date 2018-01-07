using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkecthBlock : BaseSketch {

    private int existBlockCount;
    protected int[][] sketchedBlock = new int[blockCount + 1][];
    BoxCollider2D originBlock = new BoxCollider2D();
    BoxCollider2D[][] block = new BoxCollider2D[blockCount + 1][];
    [SerializeField] private GameObject prefab;

    private void Initialize() {
        existBlockCount = 0;
        for(int y = 0; y <= blockCount; ++y) {
            for (int x = 0; x <= blockCount; ++x) {
                sketchedBlock[y][x] = 0;
            }
        }
    }

    private bool CheckCollision(Vector2 position) {
        bool isCollision = Physics2D.BoxCast(position, new Vector2(blockSizeX, blockSizeX), 0f, Vector2.zero);
        return isCollision;
    }

    private void CreateBlock(int blockIndX,int blockIndY) {
        Vector2 topLeft = GetScreenTopLeft();
        Vector2 position = new Vector2(topLeft.x + blockSizeX * blockIndX + (blockSizeX / 2), topLeft.y + blockSizeY * blockIndY + (blockSizeY / 2));
        if (sketchedBlock[blockIndY][blockIndX] == 1) return;
        if (CheckCollision(position)) return;
        sketchedBlock[blockIndY][blockIndX] = 1;
        existBlockCount++;
        //3倍するとなんかよくなる
        block[blockIndY][blockIndX] = Instantiate(originBlock);
        BoxCollider2D box2D = block[blockIndY][blockIndX].GetComponent<BoxCollider2D>();
        box2D.transform.localScale = new Vector2(3 * blockSizeX, 3 * blockSizeY);
        box2D.transform.position = new Vector2(topLeft.x + blockSizeX * blockIndX + (blockSizeX / 2), topLeft.y + blockSizeY * blockIndY + (blockSizeY / 2));
    }


    private void DestroyBlock() {
        var clones = GameObject.FindGameObjectsWithTag("block");
        foreach(var clone in clones) {
            Destroy(clone);
        }
    }


    public void Sketch() {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (existBlockCount >= existMaxBlockCount) return;
        if (mousePosition.x < GetScreenTopLeft().x || mousePosition.x > GetScreenBottomRight().x || mousePosition.y > GetScreenTopLeft().y || mousePosition.y < GetScreenBottomRight().y) return;
            if (Input.GetMouseButton(0)) {
            CreateBlock((int)((mousePosition.x - GetScreenTopLeft().x) / blockSizeX), (int)((mousePosition.y - GetScreenTopLeft().y) / blockSizeY));
        }
    }

	// Use this for initialization
	protected override void Start () {
        base.Start();
        originBlock = prefab.GetComponent<BoxCollider2D>();
        for (int i = 0; i <= blockCount; ++i) block[i] = new BoxCollider2D[blockCount + 1];
        for (int i = 0; i <= blockCount; ++i) sketchedBlock[i] = new int[blockCount + 1];
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && isSketchable) {
            DestroyBlock();
            Initialize();
        }
        if(isSketchable)Sketch();
	}
}
