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
        
        Debug.Log("碰撞开始");
        //var name = collision.GetComponent<Collider>().name;
        //Debug.Log("Name is " + name);
        if(name == "apple")
        {
            Destroy(this.gameObject);
        }
        Animator squirrelAnime = GameObject.Find("squirrelM").GetComponent<Animator>();
        squirrelAnime.SetTrigger("eat");
    }
}
