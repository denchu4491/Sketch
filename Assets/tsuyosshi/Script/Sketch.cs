using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sketch : MonoBehaviour {

    public static int blockCount = 10;
    private int[][] SketchBlock = new int[blockCount][];

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 10; ++i) {
            SketchBlock[i] = new int[blockCount];
        }
	}

	// Update is called once per frame
	void Update () {
	}
}
