using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Linq;
using UnityEditor;


public class PingPongMove : MonoBehaviour {

    //コースを保存する配列  100ラリーが限界とする
    int[,] Cose = new int[100,3];
    //何ラリー目かを記録する変数.
    int RallyNumber = 0;
    //何ラリー目かを表示するテキスト
    public Text RallyNumberText;
    //ボタンなどを含んでいるcanvas全体
    public GameObject UICanvas;
    //動画再生のためのタイムライン
    public PlayableDirector MovieMode;

    //ゲーム内のカメラ(画面を切り替えたら移動させるので一つなのよ)
    public GameObject Camera;

    //どっちのカメラなのかを表示するためのobject
    public GameObject NowCamera1;
    public GameObject NowCamera2;


    //vector3に変更前の座標たち
    int[] numbers_A =   { 1140, 400, 540 };
    int[] numbers_B =   { 1140, 400, 0 };
    int[] numbers_C =   { 1140, 400, -540 };
    int[] numbers_D =   { 684, 400, 540 };
    int[] numbers_E =   { 684, 400, 0 };
    int[] numbers_F =   { 684, 400, -540 };
    int[] numbers_G =   { 228, 400, 540 };
    int[] numbers_H =   { 228, 400, 0 };
    int[] numbers_I =   { 228, 400, -540 };
    int[] numbers_1 =   { -1140, 400, -540 };
    int[] numbers_2 =   { -1140, 400, 0 };
    int[] numbers_3 =   { -1140, 400, 540 };
    int[] numbers_4 =   { -684, 400, -540 };
    int[] numbers_5 =   { -684, 400, 0 };
    int[] numbers_6 =   { -684, 400, 540 };
    int[] numbers_7 =   { -228, 400, -540 };
    int[] numbers_8 =   { -228, 400, 0 };
    int[] numbers_9 =   { -228, 400, 540 };


    //gameObjectのballの定義
    public GameObject Ball;

    Vector3 pos;
    float startTime;
    //アニメーションにかける時間
    float time = 2;
    //アニメーションの比率を作るためのfirst
    bool first = true;
    //アニメーション実行の際に何ラリーめなのかを認識する際の変数
    int num = 0;
    //アニメーションを開始するためのフラグ
    bool AnimationStart = false;
    //アニメーションが前半か後半かのフラグ
    bool harf = true;
    //現在の位置を保存している変数
    int PX = 1140;
    int PZ = 540;



	void Start () 
    {

        //startTime = Time.timeSinceLevelLoad;
        //StartCoroutine("RallyMove");

        //表示の初期設定
        //NowCamera2.SetActive(false);
        //カメラのスクリーンショット
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/savedata2.png");

            
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (AnimationStart == true)
        {
            //この行は最初だけ
            if (first == true)
            {
                startTime = Time.timeSinceLevelLoad;
                first = false;
            }
            //アニメの進行具合を比率で表す
            float diff = Time.timeSinceLevelLoad - startTime;
            float rate = diff / time;

            //バウンドまでのアニメーション
            if(harf == true)
            {
                Vector3 PointNow = new Vector3((int)(PX *1.5f), 800, (int)(PZ * 1.5f));//現在の場所の座標をvector3に変更
                Vector3 PointNext = new Vector3(Cose[num, 0], 40, Cose[num, 2]);//次の場所の座標をvector3に変更
                pos = Vector3.Lerp(PointNow, PointNext, rate);
                Ball.transform.position = pos;
              
            }
            //バウンド後のアニメーション
            else if(harf == false)
            {
                Vector3 PointNow = new Vector3(Cose[num, 0], 40, Cose[num, 2]);//現在の場所の座標をvector3に変更
                Vector3 PointNext = new Vector3(-PX * 4, 800, -PZ * 4);//次の場所の座標をvector3に変更
                pos = Vector3.Lerp(PointNow, PointNext, rate);
                Ball.transform.position = pos;
            }

            //前半のアニメーションの終わり
            if(rate >= 1.0f && harf == true)
            {
                //後半のアニメーションに移動
                harf = false;
                //アニメーションの進み具合のリセット
                rate = 0;
                startTime = Time.timeSinceLevelLoad;

            }
            //後半のアニメーションの終わり
            if (rate >= 1.0f && harf == false)
            {
                //アニメーションの進み具合のリセット
                rate = 0;
                startTime = Time.timeSinceLevelLoad;
                //前半のアニメーションへの準備
                harf = true;
                //今のポジション保存
                PX = -PX; 
                PZ = -PZ;
                //次のアニメーションに進める
                num++;

                if(Cose[num,0] == 0 && Cose[num, 2] == 0) //ラリーが終わっていたらunityの実行を停止する.
                {
                    EditorApplication.isPlaying = false;
                }
            }
        }
	}

    ////////////////////////////////////////////////入力部分
    //押したボタンの座標をcoseに記録する
    public void Push(int[] numbers)
    {
        //押した番号の座標をコースを保存する
        for (int i = 0; i < 3; i++)
        {
            Cose[RallyNumber, i] = numbers[i];
        }
        //ラリー番号の追加
        RallyNumber++;
        RallyNumberText.text = (RallyNumber).ToString()+"ラリー";

    }

    public void Push1() { Push(numbers_1); }
    public void Push2() { Push(numbers_2); }
    public void Push3() { Push(numbers_3); }
    public void Push4() { Push(numbers_4); }
    public void Push5() { Push(numbers_5); }
    public void Push6() { Push(numbers_6); }
    public void Push7() { Push(numbers_7); }
    public void Push8() { Push(numbers_8); }
    public void Push9() { Push(numbers_9); }

    public void PushA() { Push(numbers_A); }
    public void PushB() { Push(numbers_B); }
    public void PushC() { Push(numbers_C); }
    public void PushD() { Push(numbers_D); }
    public void PushE() { Push(numbers_E); }
    public void PushF() { Push(numbers_F); }
    public void PushG() { Push(numbers_G); }
    public void PushH() { Push(numbers_H); }
    public void PushI() { Push(numbers_I); }
    /////////////////////////////////////////////////////


    ///////////////////////////////////////////////出力部分
    public void RallyStart()
    {
        //動作が始まったのでcanvasを非表示にする
        UICanvas.SetActive(false);
        //Animationを開始する
        AnimationStart = true;
        //撮影の開始
        //MovieMode.Play();
    }

    public void ChangeCamera()
    {
        if(NowCamera1.activeSelf)
        {
            //カメラの位置の変更
            Camera.transform.position = new Vector3(1300f, 150f, -1300f);
            Camera.transform.rotation = Quaternion.Euler(0.0f, -45f, 0.0f);

            //表示の変更
            NowCamera1.SetActive(false);
            NowCamera2.SetActive(true);
        }
        else if(NowCamera2.activeSelf)
        {
            //カメラの位置の変更
            Camera.transform.position = new Vector3(-1300f, 150f, -1300f);
            Camera.transform.rotation = Quaternion.Euler(0.0f, 45f, 0.0f);

            //表示の変更
            NowCamera1.SetActive(true);
            NowCamera2.SetActive(false);
        }

    }

    public void DataCollect()
    {
        
    }
        
}

