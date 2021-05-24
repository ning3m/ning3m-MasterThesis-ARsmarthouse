using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelControl : MonoBehaviour
{
    private Animator squirrelAnim;
    private GameObject zzz;
    private bool isActive = true;
    
    void Awake()
    {
        // 有一个大概是Unity的bug
        // 有时候animator的state machine会不显示进度条，并且会伴随一些动画播放、参数上的问题（如本次遇到的是sleep->step->normal时，直接从sleep卡到normal）
        // 此时只要选中一下绑着Animator的GameObject，就可以复原
        // 原因成谜

        squirrelAnim = GameObject.Find("squirrelM(Clone)").GetComponent<Animator>();
        //squirrelAnim = GameObject.Find("squirrelM").GetComponent<Animator>();
        zzz = GameObject.Find("zzz");
        zzz.gameObject.SetActive(false);
        isActive = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        if (squirrelAnim.GetBool("AirConSwitch") && isActive)
        {
            isActive = false;
            zzz.gameObject.SetActive(false);//如果加载zzz上，运行之后整个object都变为非激活状态，脚本也会停止运行
            //Debug.Log(squirrelAnim.GetBool("AirConSwitch"));
            
            print("醒着");
        }
        else if(!squirrelAnim.GetBool("AirConSwitch")  && !isActive)
        {
            //显示zzz
            isActive = true;
            StartCoroutine("showZZZ");

            print("睡了");
            //Debug.Log(squirrelAnim.GetBool("AirConSwitch"));            
        }

    }

    private IEnumerator showZZZ()
    {
        yield return new WaitForSeconds(3);
        zzz.gameObject.SetActive(true);
        print("display");

    }
    void OnCollisionEnter(Collision collision)
    {
        // 销毁当前游戏物体
        //Destroy(this.gameObject);
    }
 
    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {
        
    }
     // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        //碰撞开始

    }
}
