using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBullet : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 3); // 3초 안에 게임 오브젝트를 파괴. 
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Gun"))
        {
            Destroy(other.gameObject);
            // other.gameObject.SetActive(false);
        }
        else if(other.name.Contains("Enemy"))
        {
            other.GetComponent<GamePlayer>().Died();
        }
        Destroy(gameObject);
    }
    public void Shoot (float lag)
    {
        Rigidbody rbody = GetComponent<Rigidbody>();
        rbody.velocity = transform.forward * 10; //velocity == Rigidbody의 속력 벡터 /// 앞으로 10씩 전진
        rbody.position += rbody.velocity * lag;
    }
}
