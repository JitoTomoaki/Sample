﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Linq;
using UnityEditor;

public class ExperimentShotMove : MonoBehaviour {

    public GameObject Camera;//カメラの定義

    Vector3 Camera1_Point = new Vector3(-3000,959,-3000); //カメラ1の座標
    Vector3 Camera2_Point = new Vector3(3000,959, -3000);//カメラ2の座標

    public InputField inputField_X; //X座標のテキスト
    public InputField inputField_Z; //Z座標のテキスト

    public GameObject ball;

    public int RallyCount = 0; //ラリーの回数をカウントする変数
    public Text RallyCountText;
    public GameObject Canvas;

    //ボールの座標データ
    int[,] ballPoint = {{ -1370, -762 }, { -1370, -254 }, { -1370, 254 }, { -1370, 762 } , { -913, -762 }, { -913, -254 }, { -913, 254 }, { -913, 762 }, { -455, -762 }, { -455, -254 }, { -455, 254 }, { -455, 762 }, { 0, -762 }, { 0, -254 }, { 0, 254 }, { 0, 762 }, { 456, -762 }, { 456, -254 }, { 456, 254 }, { 456, 762 }, { 913, -762 }, { 913, -254 }, { 913, 254 }, { 913, 762 }, { 1370, -762 }, { 1370, -254 }, { 1370, 254 }, { 1370, 762 }, { 6000000, 6000000 }};

    // バウンド位置を格納する配列
    int[,] BoundPoint = new int[100,100];

	void Start () 
    {

	}

    // Update is called once per frame

    /*
    IEnumerator Capture()
    {
        for (int i = 0; i < 29; i++)
        {
            //ボールの配置
            ball.transform.position = new Vector3(ballPoint[i, 0], 25f, ballPoint[i, 1]);

            //カメラ1の撮影
            Camera.transform.position = Camera1_Point;//カメラ1の位置にカメラ移動
            Camera.transform.rotation = Quaternion.Euler(11, 45, 0);//カメラ1の位置にカメラを回転させる
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/ExperimentData/Point" + (i + 1).ToString() + "_Camera1.png");
            yield return new WaitForSeconds(0.1f);

            //カメラ2の撮影
            Camera.transform.position = Camera2_Point;//カメラ2の位置にカメラ移動
            Camera.transform.rotation = Quaternion.Euler(11, -45, 0);//カメラ2の位置にカメラを回転させる
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/ExperimentData/Point" + (i + 1).ToString() + "_Camera2.png");

            yield return new WaitForSeconds(0.1f);
        }
    }*/

    /// <summary>
    /// ////////////////////////////////////////////////////////ここからが新システム
    /// </summary>
    /// 
    /// 

    public void GetBound_Data()
    {
        
        //テキストボックスからデータを取得
        int InputData_X = int.Parse(inputField_X.text);
        int InputData_Z = int.Parse(inputField_Z.text);



        //配列にデータを格納
        BoundPoint[RallyCount,0] = InputData_X;
        BoundPoint[RallyCount,1] = InputData_Z;



        Debug.Log(BoundPoint[RallyCount, 0]);
        Debug.Log(BoundPoint[RallyCount, 1]);



        RallyCount++;//ラリーカウントをプラス1する
        RallyCountText.text = "バウンド数 : " + RallyCount.ToString();

        //テキストボックスの内容をリセット
        inputField_X.text = "";
        inputField_Z.text = "";


    }

    Vector3 StartPosition = new Vector3(-1370, 400, 0);
    Vector3 EndPosition =  new Vector3(1370, 400, 0);
    Vector3 NextPos;
    int NowBound = 0; //今のバウンドを記録する変数
    bool StartFlug = false;


    GameObject Canvas1;

    public void RallyStart()
    {
        Canvas.SetActive(false);
        ball.SetActive(true);

    }

    void Update()
    {
        NextPos = new Vector3(-1370, 40,  500);
        ball.transform.position = Vector3.MoveTowards(StartPosition, NextPos, 6);

    }





















    IEnumerator NewCapture()
    {

        Canvas.SetActive(false);//キャンバスを一旦消去


        for (int i = 0; i < 29; i++)
        {
            //ボールの配置
            ball.transform.position = new Vector3(ballPoint[i, 0], 25f, ballPoint[i, 1]);

            //カメラ1の撮影
            Camera.transform.position = Camera1_Point;//カメラ1の位置にカメラ移動
            Camera.transform.rotation = Quaternion.Euler(11, 45, 0);//カメラ1の位置にカメラを回転させる
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/ExperimentData/Point" + (i + 1).ToString() + "_Camera1.png");
            yield return new WaitForSeconds(0.1f);

            //カメラ2の撮影
            Camera.transform.position = Camera2_Point;//カメラ2の位置にカメラ移動
            Camera.transform.rotation = Quaternion.Euler(11, -45, 0);//カメラ2の位置にカメラを回転させる
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/ExperimentData/Point" + (i + 1).ToString() + "_Camera2.png");

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < RallyCount; i++)
        {
            //ボールの配置
            ball.transform.position = new Vector3(BoundPoint[i, 0], 25f, BoundPoint[i, 1]);

            //カメラ1の撮影
            Camera.transform.position = Camera1_Point;//カメラ1の位置にカメラ移動
            Camera.transform.rotation = Quaternion.Euler(11, 45, 0);//カメラ1の位置にカメラを回転させる
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/ExperimentData/Point" + (i + 30).ToString() + "_Camera1.png");
            yield return new WaitForSeconds(0.1f);

            //カメラ2の撮影
            Camera.transform.position = Camera2_Point;//カメラ2の位置にカメラ移動
            Camera.transform.rotation = Quaternion.Euler(11, -45, 0);//カメラ2の位置にカメラを回転させる
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/ExperimentData/Point" + (i + 30).ToString() + "_Camera2.png");

            yield return new WaitForSeconds(0.1f);
        }

        Canvas.SetActive(true);//キャンバスを再配置
    }


}