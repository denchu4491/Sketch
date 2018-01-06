using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchRendere : Sketch {

    GameObject[] line;
    LineRenderer[] lineRendererX;
    LineRenderer[] lineRendererY;
    [SerializeField] private GameObject prefab;
    private LineRenderer _renderer;


    private Vector3 GetScreenTopLeft(Camera mainCamera) {
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 GetScreenBottomRight(Camera mainCamera) {
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }


    public void RenderingSketchSquears() {
        Camera mainCamera;
        GameObject obj = GameObject.Find("Main Camera");
        mainCamera = obj.GetComponent<Camera>();
        Vector2 topLeft = GetScreenTopLeft(mainCamera), bottomRight = GetScreenBottomRight(mainCamera);
        float screenWidth = bottomRight.x - topLeft.x;
        float screenHight = bottomRight.y - topLeft.y;
        float blockSizeX = screenWidth / blockCount;
        float blockSizeY = screenHight / blockCount;
        for (int x = 0; x <= blockCount; x++) {
            lineRendererX[x].SetWidth(0.1f, 0.1f);
            lineRendererX[x].SetVertexCount(2);
            lineRendererX[x].SetPosition(0, new Vector3(topLeft.x + x * blockSizeX, topLeft.y, 0f));
            lineRendererX[x].SetPosition(1, new Vector3(topLeft.x + x * blockSizeX, bottomRight.y, 0f));
        }
        for (int y = 0; y <= blockCount; y++) {
            lineRendererY[y].SetWidth(0.1f, 0.1f);
            lineRendererY[y].SetVertexCount(2);
            lineRendererY[y].SetPosition(0, new Vector3(topLeft.x, topLeft.y + y * blockSizeY, 0f));
            lineRendererY[y].SetPosition(1, new Vector3(bottomRight.x, topLeft.y + y * blockSizeY, 0f));
        }
    }

    // Use this for initialization
    void Start() {
        lineRendererX = new LineRenderer[blockCount + 1];
        lineRendererY = new LineRenderer[blockCount + 1];
        _renderer = prefab.GetComponent<LineRenderer>();
        for (int i = 0; i <= 10; ++i) {
            lineRendererX[i] = Instantiate(_renderer);
            lineRendererY[i] = Instantiate(_renderer);
        }
    }

    // Update is called once per frame
    void Update() {
        RenderingSketchSquears();
    }
}
