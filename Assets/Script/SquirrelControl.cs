using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelControl : MonoBehaviour
{
    private Animator squirrelAnim;
    private GameObject zzz;
    private bool isActive = true;
    // Start is called before the first frame update
    void Awake()
    {
        squirrelAnim = GameObject.Find("squirrelM(Clone)").GetComponent<Animator>();
        zzz = GameObject.Find("zzz");
        zzz.gameObject.SetActive(false);
        isActive = false;
    }
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
        //Debug.Log("碰撞开始");
        //Debug.Log(name);
        //var name = collision.GetComponent<Collider>().name;
        //Debug.Log("Name is " + name);
        if (squirrelAnim.GetBool("AirConSwitch"))
        {
            if (name.Contains("apple"))
            {
                //如果发生了碰撞的当前物体是苹果物体，则销毁当前物体
                Destroy(this.gameObject);
            }
            // 销毁苹果后触发松鼠吃动作
            Animator squirrelAnime = GameObject.Find("squirrelM(Clone)").GetComponent<Animator>();
            squirrelAnime.SetTrigger("eat");
        }
        

    }
}
