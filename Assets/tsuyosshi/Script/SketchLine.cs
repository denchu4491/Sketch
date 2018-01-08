using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchLine : BaseSketch {

    GameObject[] line;
    LineRenderer[] lineRendererX;
    LineRenderer[] lineRendererY;
    [SerializeField] private GameObject prefab;
    private LineRenderer _renderer;
    private Vector2 topLeftLinePos, BottomRightLinePos; 

    public void RenderingSketchSquears() {
        topLeftLinePos = GetScreenTopLeft(mainCamera);
        BottomRightLinePos = GetScreenBottomRight(mainCamera);
        for (int x = 0; x <= columnBlockMaxCount; x++) {
            var line = lineRendererX[x];
            line.SetWidth(0.1f, 0.1f);
            line.SetVertexCount(2);
            line.SetPosition(0, new Vector3(topLeftLinePos.x + x * blockSizeX, topLeftLinePos.y, 0f));
            line.SetPosition(1, new Vector3(topLeftLinePos.x + x * blockSizeX, BottomRightLinePos.y, 0f));
        }
        for (int y = 0; y <= rowBlockMaxCount; y++) {
            var line = lineRendererY[y];
            line.SetWidth(0.1f, 0.1f);
            line.SetVertexCount(2);
            line.SetPosition(0, new Vector3(topLeftLinePos.x, topLeftLinePos.y + y * blockSizeY, 0f));
            line.SetPosition(1, new Vector3(BottomRightLinePos.x, topLeftLinePos.y + y * blockSizeY, 0f));
        }
    }

    private void SetLine() {
        for (int x = 0; x <= columnBlockMaxCount; x++) {
            lineRendererX[x].enabled = true;
        }
        for (int y = 0; y <= rowBlockMaxCount; y++) {
            lineRendererY[y].enabled = true;
        }
    }

    private void RemoveLine() {
        for (int x = 0; x <= columnBlockMaxCount; x++) {
            lineRendererX[x].enabled = false;
        }
        for (int y = 0; y <= rowBlockMaxCount; y++) {
            lineRendererY[y].enabled = false;
        }
    }

    // Use this for initialization
    protected virtual void Start() {
        base.Start();
        lineRendererX = new LineRenderer[columnBlockMaxCount + 1];
        lineRendererY = new LineRenderer[rowBlockMaxCount + 1];
        _renderer = prefab.GetComponent<LineRenderer>();
        for (int x = 0; x <= columnBlockMaxCount; ++x) {
            lineRendererX[x] = Instantiate(_renderer);
        }
        for (int y = 0; y <= rowBlockMaxCount; ++y) {
            lineRendererY[y] = Instantiate(_renderer);
        }
    }

    // Update is called once per frame
    protected override void Update() {

        base.Update();

        if (Input.GetKeyDown(KeyCode.X)) {
            SetLine();
            RenderingSketchSquears();
        }
        else if(Input.GetKeyDown(KeyCode.Z)) {
            RemoveLine();
        }
    }
}
