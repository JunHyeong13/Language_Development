using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class GamePlayer : MonoBehaviour
{

    public Rigidbody rbody;
    public GameObject Gun;
    public Transform bulletSpawnPoint;
    public PhotonView PhotonView;
    
    void Start()
    {
        Gun.SetActive(false);
    }

   
    void Update()
    {
        if(PhotonView.IsMine)
        {
            float inputRotation = Input.GetAxis("Horizontal");
            float inputSpeed = Input.GetAxis("Vertical");

            Quaternion rot = rbody.rotation * Quaternion.Euler(0, inputRotation * Time.deltaTime * 60, 0);
            rbody.MoveRotation(rot);

            Vector3 force = rot * Vector3.forward * inputSpeed * 1000 * Time.deltaTime;
            rbody.AddForce(force);

            if(rbody.velocity.magnitude > 2)
            {
                rbody.velocity = rbody.velocity.normalized * 2;
            }

            if(Input.GetKeyDown(KeyCode.Space) && Gun.activeSelf)
            {
                Gun.SetActive(false);
                PhotonView.RPC("Fire",RpcTarget.All);
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                PhotonView.RPC("ChangeColor",RpcTarget.All, Random.Range(0.0f, 1.0f));
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////
    [PunRPC]
    public void Fire(PhotonMessageInfo info) // 총알 prefab을 복제하여 발사하는 코드 
    {
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        GameObject bulletPrefab = Resources.Load("Bullet") as GameObject;
        GameObject bulletObject = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation) as GameObject;
        bulletObject.GetComponent<GameBullet>().Shoot(lag);
    }
    

    // 키보드에서 'c'를 누를 경우, 플레이어의 색깔이 실시간으로 바뀌는 코드 

    // [PunRPC]
    // public void ChangeColor(float hue, PhotonMessageInfo info) 
    // {
    //     Color newColor = Color.HSVToRGB(hue, 1, 1);
    //     GetComponent<MeshRenderer>().material.color = newColor;
    // }

    public void Died()
    {
        Gun.SetActive(false);
        rbody.MovePosition(new Vector3(0,1,0));
    }
    

    public void OnTriggerEnter(Collider other) 
    {
        if(other.name.Contains("Gun")) 
        {
            Gun.SetActive(true);
            Destroy(other.gameObject); //'Gun'이라는 이름의 물체와 부딫혔을 때 부딫힌 물체를 부숨.
        }
    }
}