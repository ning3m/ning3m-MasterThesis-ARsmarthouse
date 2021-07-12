﻿using GoogleARCore;
using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using System.Web.Script.Serialization;

public class AppController : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public GameObject squirrel;
    public GameObject appleY;
    public GameObject appleB;
    public GameObject kotori;
    public GameObject frog;
    public GameObject snowman;
    public Button button1;
    public Button button2;
    public GameObject text;

    public float temperature;
    public int illuminate;
    public int humidity;
    private bool isHot = false;
    private bool isCold = false;
    private int tempChange = 0;

    private HttpOperation Device = new HttpOperation();
    private JsonOp JsonOp = new JsonOp();
    private const float mModelRotation = 180.0f;
    public int isModelExist = 0;
    private bool _isQuitting = false;

    private bool isAPIAccessed = false;
    private bool isAsynvContentAccessed = false;
    private bool isAsyncOver = false;
    //private bool ifTouch = false;

    /*private bool isAPIAccessed2 = false;
    private bool isAsynvContentAccessed2 = false;
    private bool isAsyncOver2 = false;*/

    protected string responsedHTTPHead;

    protected readonly HttpClient httpclient = new HttpClient();
    protected string responseData;

    private float positionX = 0;
    private float positionY = 0;


    //  将其用于初始化
    
    async void Awake()
    {
        text.GetComponent<TMP_Text>().text = "Click on detected plane to check temperature";
    }
    async void Start()
    {
        //button1.onClick.AddListener(Button1Click);
        //button2.onClick.AddListener(Button2Click);
        //button1.gameObject.SetActive(false);
        //button2.gameObject.SetActive(false);
        OnCheckDevice();
    }
    private void OnCheckDevice()
    {
        throw new NotImplementedException();
    }

    /*void Button1Click()
    {
        if (isAsyncOver)
        {
            if (temperature > 20)
            {
                isModelExist = 1;
            }
            else if(temperature <= 20)
            {
                isModelExist = 2;
            }
        }
        
    }*/
    /*void Button2Click()
    {
        isModelExist = 3;
    }*/

    //  每帧调用一次 Update

    private void timeLoop()
    {
        // run every 2 minute
        /*isAPIAccessed = false;
        isAsynvContentAccessed = false;
        isAsyncOver = false;*/
    }


    private void Control()
    {
        //访问一次API（兼异步结束判断）
        if (!isAPIAccessed)
        {
            // 如果没有访问过API，则访问
            Device.getDevice();
            isAPIAccessed = true;
        }
        // 每帧取一次异步完成情况
        isAsyncOver = Device.isAsyncOver;

        if (isAsyncOver && !isAsynvContentAccessed)
        {
            // 异步完成且异步得到的内容尚未访问
            responseData = Device.responseData;
            temperature = JsonOp.getTemp(responseData);
            //illuminate = JsonOp.getIlluminate(responseData);
            //humidity = JsonOp.getHumidity(responseData);
            Debug.Log(temperature);
            if (temperature < 20) {
                isModelExist = 1;
                isCold = true;
                tempChange = 22 - (int)temperature;
            } 
            else if (temperature > 26)
            {
                isModelExist = 2;
                isHot = true;
                tempChange = (int)temperature - 24;
            }
                
            /*Debug.Log(illuminate);
            Debug.Log(humidity);*/

            isAsynvContentAccessed = true;
        }
        
    }



    void Update()
    {
        UpdateApplicationLifecycle();
        Control();

        Touch touch;

        // 如果没有触屏或有触屏但触屏不等于刚按下；且没有单次的鼠标点击
        if ((Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) && !Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        TrackableHit hit;
        // TrackableHit类保存的是发生碰撞检测时的检测到相关信息。包括Distance，Flags, Pose, Trackable

        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
        // TrackableHitFlags（ARcore组件）用来过滤需要进行碰撞检测的对象类型。
        // PlaneWithinPolygon是与已检测平面内的凸边界多边形进行碰撞检测；
        // PlaneWithinBounds与当前帧中已检测平面内的包围盒进行碰撞检测。

        
        if (Input.touchCount > 0 && !Input.GetMouseButtonDown(0))
        {
            // 如果是触屏
            touch = Input.GetTouch(0);
            positionX = touch.position.x;
            positionY = touch.position.y;
        }
        else if (Input.touchCount < 1 && Input.GetMouseButtonDown(0))
        {
            //如果是鼠标
            positionX = Input.mousePosition.x;
            positionY = Input.mousePosition.y;
        }

        // 触摸触发平面
        if (Frame.Raycast(positionX, positionY, raycastFilter, out hit))
        // Frame包含有关ARCore状态的信息，包括相机相对于世界的姿势，估计的照明参数，以及有关ARCore跟踪的对象（如飞机或点云）更新的信息。
        // 发出射线Raycast(float x, float y, TrackableHitFlags filter, out TrackableHit hitResult)
        // 对Arcore跟踪的物理对象执行光线投射，参考1，2为屏幕坐标点，一旦发生碰撞则返回，返回值为Bool型，true表示发生碰撞,false表示未发生碰撞。
        {
            Debug.Log(isModelExist);
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
            //if (!Input.GetMouseButton(0))
            {
                // Use hit pose and camera pose to check if hittest is from the back of the plane, if it is, no need to create the anchor. 
                Debug.Log("射线击中了DetectedPlane的背面！");
            }
            else if (isModelExist == 1/*雪人*/)
            {
                var gameObject = Instantiate(snowman, hit.Pose.position, Quaternion.Euler(90.0f, 0.0f, 150.0f));
                text.GetComponent<TMP_Text>().text = "Oh, it`s cold. \n Do you want to check air conditioner?";
                //isModelExist += 1;
                // Compensate for the hitPose rotation facing away from the raycast (i.e.camera).
                gameObject.transform.Rotate(0, mModelRotation, 0, Space.Self);
                // 生成一个anchor，并将我们的Prefab挂载到这个anchor上
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                gameObject.transform.parent = anchor.transform;
                isModelExist = 3;
                
            }
            else if (isModelExist == 2/*青蛙*/)
            {
                var gameObject = Instantiate(frog, hit.Pose.position, Quaternion.Euler(90.0f, 0.0f, 150.0f));
                //isModelExist += 1;
                text.GetComponent<TMP_Text>().text = "Oh, it`s hot. \n Do you want to check air conditioner?";
                // Compensate for the hitPose rotation facing away from the raycast (i.e.camera).
                gameObject.transform.Rotate(0, mModelRotation, 0, Space.Self);
                // 生成一个anchor，并将我们的Prefab挂载到这个anchor上
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                gameObject.transform.parent = anchor.transform;
                isModelExist = 3;
            }

            else if(isModelExist == 3/*松鼠*/)
            {
                // 如果是击中了检测到的平面并且又不是平面的背面，我们则实例化我们的Prefab，
                text.GetComponent<TMP_Text>().text = "Touch the squirrel to turn on/off the air conditioner.";
                var gameObject = Instantiate(squirrel, hit.Pose.position, hit.Pose.rotation);
                //isModelExist += 1;
                // Compensate for the hitPose rotation facing away from the raycast (i.e.camera).
                gameObject.transform.Rotate(0, mModelRotation, 0, Space.Self);
                // 生成一个anchor，并将我们的Prefab挂载到这个anchor上
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                gameObject.transform.parent = anchor.transform;
                isModelExist = 4;
                //todo 提示创建苹果
            }
            else if (isModelExist == 4/*苹果*/)
            {
                if (isHot)
                {
                    text.GetComponent<TMP_Text>().text = "It`s hot. \n We recommend decreasing the temperature.";
                    var gameObject = Instantiate(appleB, hit.Pose.position, hit.Pose.rotation);
                    gameObject.transform.Rotate(0, mModelRotation, 0, Space.Self);
                    // 生成一个anchor，并将我们的Prefab挂载到这个anchor上
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    gameObject.transform.parent = anchor.transform;
                    tempChange = tempChange - 1;
                    if (tempChange == 0)
                    {
                        isModelExist = 5;
                    }
                }
               
                else if (isCold)
                {
                    text.GetComponent<TMP_Text>().text = "It`s cold. \n We recommend increasing the temperature.";
                    var gameObject = Instantiate(appleY, hit.Pose.position, hit.Pose.rotation);
                    gameObject.transform.Rotate(0, mModelRotation, 0, Space.Self);
                    // 生成一个anchor，并将我们的Prefab挂载到这个anchor上
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    gameObject.transform.parent = anchor.transform;
                    tempChange = tempChange - 1;
                    if (tempChange == 0)
                    {
                        isModelExist = 5;
                    }
                }
            }
            else if (isModelExist == 5)
            {
                text.GetComponent<TMP_Text>().text = "Please wait, \n the right temperature will be reached soon.";
                isModelExist = 6;
            }
            //todo1 创建颜色不同的苹果，代表不同的操作
            //todo2 实现睡觉和向前走的动作
            else if (isModelExist > 5)
            {
                //Debug.Log(isModelExist);
                //Debug.Log("已有模型");
                return;

            }
        }
    }





    /// <summary>
    /// Check and update the application lifecycle.
    /// </summary>

    private void UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (_isQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to
        // appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            ShowAndroidToastMessage("Camera permission is needed to run this application.");
            _isQuitting = true;
            Invoke("DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            _isQuitting = true;
            Invoke("DoQuit", 0.5f);
        }
    }
 
    
    private void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
