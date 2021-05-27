using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class sunControl : MonoBehaviour
{

    private GameObject flower;
    private GameObject lightGameObject;


    private int illuminate;

    private bool isflowerExist = false;
    private bool isSunExist = false;

    private string moveObjectName = "default";
    private GameObject moveObject;
    private bool isObjectSelect = false;

    private Vector3 temp;
    private float positionX = 0;
    private float positionY = 0;

    private float recordPX = 0;
    private float recordPY = 0;
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        flower = GameObject.Find("flowerM(Clone)");
        lightGameObject = GameObject.Find("Directional Light");
    }

    // Update is called once per frame
    void Update()
    {
        HeightControl();
        
    }

    private void HeightControl()
    {
        if (!(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
        {
            return;
        }
        // 如果没有触屏或有触屏但触屏不等于刚按下；且没有任意一种鼠标点击
        if (Input.GetMouseButton(0))
        {
            //Debug.Log(Perfeb.isModelExist);
            positionX = Input.mousePosition.x;
            positionY = Input.mousePosition.y;



            Ray ray = Camera.main.ScreenPointToRay(new Vector3(positionX, positionY));
            RaycastHit hitObject;
            //TrackableHit Thit = new TrackableHit();
            //TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
            if (Physics.Raycast(ray, out hitObject) && !isObjectSelect)
            {
                
                moveObject = hitObject.collider.gameObject;
                moveObjectName = hitObject.transform.name;
                isObjectSelect = true;
                //Debug.Log(isObjectSelect);
                //Debug.Log(name);
                //Debug.Log(moveObjectName);

            }
            if (name == moveObjectName)
            {
                float y = 0.0f;
                if(recordPY == 0)
                {
                    y = 0.0f;
                }
                else
                {
                    y = positionY - recordPY;
                }
                               
                //Frame.Raycast(positionX, positionY, raycastFilter, out Thit);
                Debug.Log(flower.transform.position);
                Debug.Log(transform.position);
                if (transform.position.y - flower.transform.position.y < 0.5f && flower.transform.position.y - transform.position.y < 0.5f)
                {
                    //如果太阳没有超过范围
                    //var p = 0;
                    
                    /*if(y > 0)
                    {
                        p = 1;
                    }else if (y < 0)
                    {
                        p = -1;
                    }*/
                    Debug.Log(y);
                    //则将手指在屏幕上移动的距离作为参数，调整她的高度
                    temp = new Vector3(transform.position.x, transform.position.y + 0.001f * y, transform.position.z);
                    lightGameObject.GetComponent<Light>().intensity =  0.5f + (transform.position.y - flower.transform.position.y);
                    Debug.Log(temp);
                    transform.position = temp;
                }
                recordPY = positionY;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            //移动后，当鼠标放开，重置取到的objectName。
            //否则，当鼠标放开再点击时，将会无论是否点到目标物体都移动上一个物体（因为moveObjectName没变所以if永远为真）

            moveObjectName = "default";
            isObjectSelect = false;
            //Debug.Log(isObjectSelect);
        }
    }
}
