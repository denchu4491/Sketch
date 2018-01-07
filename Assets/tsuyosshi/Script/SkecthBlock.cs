using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkecthBlock : BaseSketch {

    private int existBlockCount;
    private float existBlockTime;
    protected int[][] sketchedBlock = new int[rowBlockMaxCount + 1][];
    BoxCollider2D originBlock = new BoxCollider2D();
    BoxCollider2D[][] block = new BoxCollider2D[rowBlockMaxCount + 1][];
    [SerializeField] private GameObject prefab;

    private void InitializeBlock() {
        existBlockCount = 0;
        for(int y = 0; y <= rowBlockMaxCount; ++y) {
            for (int x = 0; x <= columnBlockMaxCount; ++x) {
                sketchedBlock[y][x] = 0;
            }
        }
    }

    private bool CheckCollision(Vector2 position) {
        var isCollision = Physics2D.OverlapBox(position, new Vector2(blockSizeX * 0.8f, blockSizeY * 0.8f), 0);
        return (isCollision != null);
    }

    private void CreateBlock(int blockIndX,int blockIndY) {
        Vector2 topLeft = GetScreenTopLeft();
        Vector2 position = new Vector2(topLeft.x + blockSizeX * blockIndX + (blockSizeX / 2), topLeft.y + blockSizeY * blockIndY + (blockSizeY / 2));
        if (blockIndX < 0 || blockIndX > columnBlockMaxCount || blockIndY < 0 || blockIndY > rowBlockMaxCount) return;
        if (sketchedBlock[blockIndY][blockIndX] == 1) return;
        if (CheckCollision(position)) return;
        sketchedBlock[blockIndY][blockIndX] = 1;
        existBlockCount++;
        block[blockIndY][blockIndX] = Instantiate(originBlock);
        BoxCollider2D box2D = block[blockIndY][blockIndX].GetComponent<BoxCollider2D>();
        box2D.transform.localScale = new Vector2(1.3f * blockSizeX, 1.3f * blockSizeY);
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
        if (existBlockCount >= existBlockMaxCount) return;
        if (mousePosition.x < GetScreenTopLeft().x || mousePosition.x > GetScreenBottomRight().x || mousePosition.y > GetScreenTopLeft().y || mousePosition.y < GetScreenBottomRight().y) return;
            if (Input.GetMouseButton(0)) {
            CreateBlock((int)((mousePosition.x - GetScreenTopLeft().x) / blockSizeX), (int)((mousePosition.y - GetScreenTopLeft().y) / blockSizeY));
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
        for (int i = 0; i <= rowBlockMaxCount; ++i) sketchedBlock[i] = new int[columnBlockMaxCount + 1];
        ResetTime();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if ((Input.GetKeyDown(KeyCode.X) && isSketchable) || existBlockTime >= existBlockMaxTime) {
            DestroyBlock();
            InitializeBlock();
            ResetTime();
        }
        if (isSketchable) Sketch();
        else if (!isSketchable) CountTime();
	}
}
