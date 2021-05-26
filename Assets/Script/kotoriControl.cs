using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kotoriControl : MonoBehaviour
{
    private Animator squirrelAnim;
    private Animator kotoriAnim;

    private float positionX = 0;
    private float positionY = 0;

    private string moveObjectName = "default";
    private GameObject moveObject;
    private bool isObjectSelect = false;

    //private bool ifTouch = false;
    // Start is called before the first frame update
    void Awake()
    {
        squirrelAnim = GameObject.Find("squirrelM(Clone)").GetComponent<Animator>();
        kotoriAnim = GameObject.Find("kotori(Clone)").GetComponent<Animator>();
        
    }
    void Start()
    {
        kotoriAnim.SetFloat("temperature", 19);
    }

    // Update is called once per frame
    void Update()
    {
        AnimaTrigger();

    }

    private void AnimaTrigger()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("flag1");
            //Debug.Log(Perfeb.isModelExist);
            positionX = Input.mousePosition.x;
            positionY = Input.mousePosition.y;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(positionX, positionY));
            RaycastHit hitObject;
            //TrackableHit Thit = new TrackableHit();
            //TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.PlaneWithinBounds;
            if (Physics.Raycast(ray, out hitObject))
            {
                Debug.Log("flag2");
                moveObject = hitObject.collider.gameObject;
                moveObjectName = hitObject.transform.name;
                isObjectSelect = true;
                //Debug.Log(isObjectSelect);
                Debug.Log(name);
                Debug.Log(moveObjectName);

            }
            if (name == moveObjectName)
            {
                kotoriAnim.SetTrigger("touch");
                squirrelAnim.SetBool("AirConSwitch", true);
                Debug.Log("flag3");
                //ifTouch = true;  
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            //ifTouch = false;
            isObjectSelect = false;
        }
    }
}
