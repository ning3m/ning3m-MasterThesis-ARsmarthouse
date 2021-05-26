using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunControl : MonoBehaviour
{

    private GameObject flower;
    private GameObject sun;
    private flowerControl flowerC = new flowerControl();
    private int illuminate;

    private bool isflowerExist = false;
    private bool isSunExist = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //todo 判断花瓶是否存在，如果花瓶存在，取灯的状态和亮度，然后实例化太阳/月亮在2d屏幕空间内
    }
}
