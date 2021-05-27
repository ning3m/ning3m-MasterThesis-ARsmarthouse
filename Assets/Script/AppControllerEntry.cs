using GoogleARCore;
using System;
using UnityEngine;
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

    private const float mModelRotation = 180.0f;
    private bool _isQuitting = false;


    protected string responsedHTTPHead;

    protected readonly HttpClient httpclient = new HttpClient();
    protected string responseData;

    private float positionX = 0;
    private float positionY = 0;


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




    void Update()
    {
        UpdateApplicationLifecycle();

        if (false/*温度*/)
        {
            /*显示模型*/
        }


        if (false/*光照*/)
        {
            /*显示模型*/
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
