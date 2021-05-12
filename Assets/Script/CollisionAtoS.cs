using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAtoS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
