using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    // void Start()
    // {
    // }


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnGun());
        }
    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            StartCoroutine(SpawnGun()); 
        }
    }

    //Coroutine 이란? 프레임과 상관없이 별도의 서브 루틴에서 원하는 작업을 원하는 시간만큼 수행하는 것!

    public IEnumerator SpawnGun()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10)); //5~10초 사이에 잠시 코루틴을 멈추도록 하는 코드

            Vector3 position = Random.insideUnitSphere * 25;
            position.y = 1.5f; //y축 방향으로 1.5 높이에 위치. 

            Quaternion rot = Quaternion.Euler(90, Random.Range(0, 360), 0);
            PhotonNetwork.Instantiate("Gun", position, Quaternion.Euler(270, Random.Range(0,360), 180)); //x축으로 270, y축은 랜덤, z축은 180으로 회전해서 배치
        }
    }
}