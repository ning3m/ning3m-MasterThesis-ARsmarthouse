using GoogleARCore;
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

public class AppControlLigh : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public GameObject flower;

    public float temperature;
    public int illuminate;
    public int humidity;

    private HttpOperation Device = new HttpOperation();
    private JsonOp JsonOp = new JsonOp();
    private const float mModelRotation = 180.0f;
    public int isModelExist = 0;
    private bool _isQuitting = false;

    private bool isAPIAccessed = false;
    private bool isAsynvContentAccessed = false;
    private bool isAsyncOver = false;
    private bool ifTouch = false;

    protected string responsedHTTPHead;

    protected readonly HttpClient httpclient = new HttpClient();
    protected string responseData;
    
    private float positionX = 0;
    private float positionY = 0;

    // Start is called before the first frame update
    void Start()
    {
        OnCheckDevice();
    }
    private void OnCheckDevice()
    {
        throw new NotImplementedException();
    }

    private void getStatus()
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
            /*Debug.Log(illuminate);
            Debug.Log(humidity);*/

            isAsynvContentAccessed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateApplicationLifecycle();
        //getStatus();

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
        {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
            //if (!Input.GetMouseButton(0))
            {

                Debug.Log("射线击中了DetectedPlane的背面！");
            }

            else if (isModelExist == 0/*花瓶*/)
            {
                // 如果是击中了检测到的平面并且又不是平面的背面，我们则实例化我们的Prefab，
                var gameObject = Instantiate(flower, hit.Pose.position, hit.Pose.rotation);
                isModelExist += 1;
                // Compensate for the hitPose rotation facing away from the raycast (i.e.camera).
                gameObject.transform.Rotate(0, mModelRotation, 0, Space.Self);
                // 生成一个anchor，并将我们的Prefab挂载到这个anchor上
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                gameObject.transform.parent = anchor.transform;
            }
            else if (isModelExist > 0)
            {
                //Debug.Log(isModelExist);
                //Debug.Log("已有模型");
                return;

            }
        }
    }


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
