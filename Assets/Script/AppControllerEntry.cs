using GoogleARCore;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

//using System.Web.Script.Serialization;

public class AppControllerEntry : MonoBehaviour
{
    public Camera FirstPersonCamera;

    public Button temp;
    public Button light;

    private const float mModelRotation = 180.0f;
    private bool _isQuitting = false;


    protected string responsedHTTPHead;

    protected readonly HttpClient httpclient = new HttpClient();
    protected string responseData;

    private float positionX = 0;
    private float positionY = 0;


    public float temperature;
    public int illuminate;

    private bool isAPIAccessed = false;
    private bool isAsynvContentAccessed = false;
    private bool isAsyncOver = false;

    private HttpOperation Device = new HttpOperation();
    private JsonOp JsonOp = new JsonOp();



    //  将其用于初始化


    async void Start()
    {
        //SceneManager.LoadScene("myScenes");
        OnCheckDevice();


    }


    private void OnCheckDevice()
    {
        throw new NotImplementedException();
    }


    private void Control()
    {
        //访问一次API（兼异步结束判断）
        

    }



    void Update()
    {
        UpdateApplicationLifecycle();



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
            illuminate = JsonOp.getIlluminate(responseData);
            //humidity = JsonOp.getHumidity(responseData);
            Debug.Log(temperature);
            if (temperature < 20 || temperature > 24  /*温度*/)
            {
                temp.GetComponent<Image>().color = Color.yellow;
            }

            Debug.Log(illuminate);
            if (illuminate < 153 /*光照*/)
            {
                float a = illuminate * 0.0065f;
                
                
                Color color = Color.white;
                Debug.Log(a);
                color.r = a;
                color.g = a;
                color.b = a;
                
                light.GetComponent<Image>().color = color;
            }
            /*Debug.Log(humidity);*/

            isAsynvContentAccessed = true;
        }


        if (false/*上次扫地时间大于xx*/)
        {
            /*显示模型*/
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
