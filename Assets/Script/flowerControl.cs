using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerControl : MonoBehaviour
{
    private HttpOperation Device = new HttpOperation();
    private JsonOp JsonOp = new JsonOp();

    private bool isAPIAccessed = false;
    private bool isAsynvContentAccessed = false;
    private bool isAsyncOver = false;
    private bool isGetIll = false;

    private string responseData;

    public float temperature;
    public int illuminate;
    public int humidity;

    private GameObject flower;
    private GameObject sun;
    private GameObject lightGameObject;

    private bool isflowerExist = false;
    private bool isSunExist = false;


    // Start is called before the first frame update
    void Awake()
    {
        sun = GameObject.Find("sun");
        flower = GameObject.Find("flowerM(Clone)");
        lightGameObject = GameObject.Find("Directional Light");
        sun.gameObject.SetActive(false);
    }
    void Start()
    {
        
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
            //temperature = JsonOp.getTemp(responseData);
            illuminate = JsonOp.getIlluminate(responseData);
            //humidity = JsonOp.getHumidity(responseData);
            //Debug.Log(temperature);
            Debug.Log(illuminate);
            //Debug.Log(humidity);

            isAsynvContentAccessed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        getStatus();
        float sunHeight = 0;
        if (isAsyncOver)
        {
            illuminate = 30;
            sunHeight = illuminate * 0.01f;
            
            isGetIll = true;
        }

        //检查模型存在情况，如果花存在太阳不存在，则在花瓶的相对位置处实例化太阳
        if (isflowerExist && !isSunExist)
        {
            sun.gameObject.SetActive(true);
            isSunExist = true;
        }
        else if(!isflowerExist)
        {
            
            if (flower)
            {
                isflowerExist = true;
            }
        }
        else if (isSunExist && isGetIll)
        {
            // 480 * 800
            // 
            Vector3 temp = new Vector3(sun.transform.position.x, sun.transform.position.y - 0.5f + sunHeight, sun.transform.position.z);
            Debug.Log(temp);
            // todo 定义不同亮度时，太阳的高度
            // todo 不同亮度时，花朵显示数量(下面的是成功的)
            // todo 不同亮度时，屏幕亮暗不同
            lightGameObject.GetComponent<Light>().intensity = illuminate * 0.02f;
            if (sunHeight > 0.5)
            {

            }
            else if(sunHeight <= 0.5)
            {
                if (sunHeight < 0.4)
                {
                    GameObject.Find("f01").SetActive(false);
                    GameObject.Find("f02").SetActive(false);
                    GameObject.Find("f03").SetActive(false);
                    if (sunHeight < 0.3)
                    {
                        GameObject.Find("f04").SetActive(false);
                        GameObject.Find("f05").SetActive(false);
                        GameObject.Find("f06").SetActive(false);
                        if (sunHeight < 0.2)
                        {
                            GameObject.Find("f07").SetActive(false);
                            GameObject.Find("f08").SetActive(false);
                            GameObject.Find("f10").SetActive(false);
                            if(sunHeight < 0.1)
                            {
                                GameObject.Find("f11").SetActive(false);
                                GameObject.Find("f12").SetActive(false);
                                GameObject.Find("f13").SetActive(false);
                            }
                        }
                    }
                    
                }
            }
            
            
            
            
            
            sun.transform.position = temp;
        }
        
    }
}
