using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBoundPosition : MonoBehaviour {

    // Use this for initialization

    Vector3 BoundPoint;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
            // Vector3でマウスがクリックした位置座標を取得する
            BoundPoint = Input.mousePosition;
            Debug.Log(BoundPoint);
        }
	}
}
