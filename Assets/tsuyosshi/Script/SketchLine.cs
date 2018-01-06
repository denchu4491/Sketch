using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchLine : BaseSketch {

    GameObject[] line;
    LineRenderer[] lineRendererX;
    LineRenderer[] lineRendererY;
    [SerializeField] private GameObject prefab;
    private LineRenderer _renderer;

    public void RenderingSketchSquears() {
        Vector2 topLeft = GetScreenTopLeft(), bottomRight = GetScreenBottomRight();
        for (int x = 0; x <= blockCount; x++) {
            var line = lineRendererX[x];
            line.SetWidth(0.1f, 0.1f);
            line.SetVertexCount(2);
            line.SetPosition(0, new Vector3(topLeft.x + x * blockSizeX, topLeft.y, 0f));
            line.SetPosition(1, new Vector3(topLeft.x + x * blockSizeX, bottomRight.y, 0f));
        }
        for (int y = 0; y <= blockCount; y++) {
            var line = lineRendererY[y];
            line.SetWidth(0.1f, 0.1f);
            line.SetVertexCount(2);
            line.SetPosition(0, new Vector3(topLeft.x, topLeft.y + y * blockSizeY, 0f));
            line.SetPosition(1, new Vector3(bottomRight.x, topLeft.y + y * blockSizeY, 0f));
        }
    }

    private void SetLine() {
        for (int x = 0; x <= blockCount; x++) {
            lineRendererX[x].enabled = true;
        }
        for (int y = 0; y <= blockCount; y++) {
            lineRendererY[y].enabled = true;
        }
    }

    private void RemoveLine() {
        for (int x = 0; x <= blockCount; x++) {
            lineRendererX[x].enabled = false;
        }
        for (int y = 0; y <= blockCount; y++) {
            lineRendererY[y].enabled = false;
        }
    }

    // Use this for initialization
    protected virtual void Start() {
        base.Start();
        lineRendererX = new LineRenderer[blockCount + 1];
        lineRendererY = new LineRenderer[blockCount + 1];
        _renderer = prefab.GetComponent<LineRenderer>();
        for (int i = 0; i <= 10; ++i) {
            lineRendererX[i] = Instantiate(_renderer);
            lineRendererY[i] = Instantiate(_renderer);
        }
    }

    // Update is called once per frame
    protected override void Update() {

        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            RenderingSketchSquears();
            SetLine();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            RemoveLine();
        }
    }
}
