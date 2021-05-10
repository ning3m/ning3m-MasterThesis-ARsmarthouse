using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



public class mouseControl : MonoBehaviour
{

    //旋转最大角度
    public int yMinLimit = -20;
    public int yMaxLimit = 80;
    //旋转速度
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    //旋转角度
    private float x = 0.0f;
    private float y = 0.0f;
    public bool IsClickedSelf { set; get; }

    private string moveObjectName = "default";
    private GameObject moveObject;
    private bool isObjectSelect = false;

    private Vector3 temp;

    private float positionX = 0;
    private float positionY = 0;

    private ARRaycastManager arRaycastManager;

    private  List<ARRaycastHit> hits = new List<ARRaycastHit>();
    

    private Camera arCamera;


    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        /*if (Input.touchCount < 1 && !(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
        {
            return;
        }*/

            Control3DMouse();

    }
    /*private void Control3DTouch()
    {

        Touch touch;
        // 如果没有触屏或有触屏但触屏不等于刚按下；且没有任意一种鼠标点击
        if ((Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) && !(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
        {
            return;
        }


        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began))
        {
            //Debug.Log(Perfeb.isModelExist);

            if (Input.touchCount > 0 && !Input.GetMouseButton(0))
            {
                // 如果是触屏
                touch = Input.GetTouch(0);
                positionX = touch.position.x;
                positionY = touch.position.y;
            }
            else if (Input.touchCount < 1 && Input.GetMouseButton(0))
            {

                //如果是鼠标
                positionX = Input.mousePosition.x;
                positionY = Input.mousePosition.y;
            }

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(positionX, positionY));
            RaycastHit hitObject;
            TrackableHit Thit = new TrackableHit();
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
            if (Physics.Raycast(ray, out hitObject) && !isObjectSelect)
            {
                moveObject = hitObject.collider.gameObject;
                moveObjectName = hitObject.transform.name;
                isObjectSelect = true;
                Debug.Log(isObjectSelect);
                Debug.Log(name);
                Debug.Log(moveObjectName);

            }
            if (name == moveObjectName)
            {

                Frame.Raycast(positionX, positionY, raycastFilter, out Thit);

                temp = Thit.Pose.position;
                Debug.Log(temp);

                transform.position = temp;
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
    }*/

    private void Control3DMouse()
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
            TrackableHit Thit = new TrackableHit();
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
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
                Frame.Raycast(positionX, positionY, raycastFilter, out Thit);
                temp = Thit.Pose.position;
                //Debug.Log(temp);
                transform.position = temp;
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



    /*private void Control3DARFoundation()
    {
        Touch touch;
        // 如果没有触屏或有触屏但触屏不等于刚按下；且没有任意一种鼠标点击
        if ((Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) && !(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
        {
            return;
        }


        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began ))
        {
            //Debug.Log(Perfeb.isModelExist);

            if (Input.touchCount > 0 && !Input.GetMouseButton(0))
            {
                // 如果是触屏
                touch = Input.GetTouch(0);
                positionX = touch.position.x;
                positionY = touch.position.y;
            }
            else if (Input.touchCount < 1 && Input.GetMouseButton(0))
            {

                //如果是鼠标
                positionX = Input.mousePosition.x;
                positionY = Input.mousePosition.y;
            }

            //Ray ray = FirstPersonCamera.ScreenPointToRay(new Vector3(positionX, positionY));
            RaycastHit hitObject;
            TrackableHit Thit = new TrackableHit();
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
            if (Physics.Raycast(ray, out hitObject) )
            {
                if (hitObject.transform.name.Contains(name))
                {
                    //moveObject = hitObject.collider.gameObject;
                    //moveObjectName = hitObject.transform.name;
                    isObjectSelect = true;
                    //Debug.Log(isObjectSelect);
                }
                
                

                
                
                //Debug.Log(name);
                //Debug.Log(moveObjectName);*/
                /*if (name == moveObjectName)
                {
                    //

                    Frame.Raycast(positionX, positionY, raycastFilter, out Thit);
 
                    temp = Thit.Pose.position;
                    Debug.Log(temp);

                    transform.position = temp;
                }
            }
   
        }

        if (isObjectSelect)
        {
            Vector2 position = new Vector2(positionX, positionY);
            Debug.Log(position);
            //Debug.Log(hits);
            //Debug.Log(UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
            Debug.Log(arRaycastManager.Raycast(position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon));
            if (arRaycastManager.Raycast(position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Debug.Log(hitPose.position);
                transform.position = hitPose.position;
                transform.rotation = hitPose.rotation;
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
    }*/

    private void Control2D()
    {
        Touch touch;
        if ((Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) && !(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
        {
            return;
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began ))
        {
            if (Input.touchCount > 0 && !Input.GetMouseButton(0))
            {
                // 如果是触屏
                touch = Input.GetTouch(0);
                positionX = touch.position.x;
                positionY = touch.position.y;
            }
            else if (Input.touchCount < 1 && Input.GetMouseButton(0))
            {

                //如果是鼠标
                positionX = Input.mousePosition.x;
                positionY = Input.mousePosition.y;
            }

            //移动
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !isObjectSelect)
            {
                moveObject = hit.collider.gameObject;    //获得选中物体
                moveObjectName = moveObject.name;    //获得选中物体的名字，使用hit.transform.name也可以
                //Debug.Log(moveObjectName);
                isObjectSelect = true;
                Debug.Log(isObjectSelect);

            }
            if (name == moveObjectName)
            {
                if (name == "squirrelM")
                {
                    temp = Camera.main.ScreenToWorldPoint(new Vector3(positionX, positionY, 1));
                }
                else if (name == "apple")
                {
                    temp = Camera.main.ScreenToWorldPoint(new Vector3(positionX, positionY, 0.9f));
                }
                //将屏幕坐标转化为世界坐标  ScreenToWorldPoint函数的z轴不能为0，不然返回摄像机的位置，而Input.mousePosition的z轴为0
                //z轴设成10的原因是摄像机坐标是（0，0，-10），而物体的坐标是（0，0，0），所以加上10，正好是转化后物体跟摄像机的距离

                transform.position = temp;
            }

        }

        else if (Input.GetMouseButton(1))
        {
            //旋转
            //Input.GetAxis("MouseX")获取鼠标移动的X轴的距离
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            //欧拉角转化为四元数
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //鼠标滚动滑轮 值就会变化
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                /*var offset = Input.GetAxis("Mouse ScrollWheel");
                float scaleFactor = 500.00f ;
                Vector3 localScale = transform.localScale;
                Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                            localScale.y + scaleFactor,
                                            localScale.z + scaleFactor);*/
                //范围值限定
                //if (Camera.main.fieldOfView <= 100)
                //    Camera.main.fieldOfView += 2;
                //if (Camera.main.orthographicSize <= 20)
                //    Camera.main.orthographicSize += 0.5F;
            }
            //Zoom in  
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                /*var offset = Input.GetAxis("Mouse ScrollWheel");
                float scaleFactor = 1.00f;
                Vector3 localScale = transform.localScale;
                Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                            localScale.y + scaleFactor,
                                            localScale.z + scaleFactor);*/

                //范围值限定
                //if (Camera.main.fieldOfView > 2)
                //    Camera.main.fieldOfView -= 2;
                //if (Camera.main.orthographicSize >= 1)
                //    Camera.main.orthographicSize -= 0.5F;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //移动后，当鼠标放开，重置取到的objectName。
            //否则，当鼠标放开再点击时，将会无论是否点到目标物体都移动上一个物体（因为moveObjectName没变所以if永远为真）

            moveObjectName = "default";
            isObjectSelect = false;
        }
    }

    //角度范围值限定
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        // if (angle > 360)
        // angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

