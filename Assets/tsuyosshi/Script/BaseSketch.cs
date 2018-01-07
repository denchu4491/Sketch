using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSketch : MonoBehaviour {

    public static int rowBlockMaxCount = 10;
    public static int columnBlockMaxCount = 10;
    public static int existBlockMaxCount = 10;
    public static float existBlockMaxTime = 5.0f;

    public Camera mainCamera;
    protected float blockSizeX, blockSizeY;
    public bool isSketchable;

    protected Vector3 GetScreenTopLeft() {
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    protected Vector3 GetScreenBottomRight() {
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }

    // Use this for initialization

    protected virtual void Start () {
        GameObject obj = GameObject.Find("Main Camera");
        mainCamera = obj.GetComponent<Camera>();
        Vector2 topLeft = GetScreenTopLeft(), bottomRight = GetScreenBottomRight();
        float screenWidth = bottomRight.x - topLeft.x;
        float screenHight = bottomRight.y - topLeft.y;
        blockSizeX = screenWidth / columnBlockMaxCount;
        blockSizeY = screenHight / rowBlockMaxCount;
	}

	// Update is called once per frame
	protected virtual void Update () {
        if (Input.GetKeyDown(KeyCode.X) && !isSketchable) isSketchable = true;
        else if (Input.GetKeyDown(KeyCode.Z)) isSketchable = false;
	}
}
