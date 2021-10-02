// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;
// using UnityEngine.UI;
// using Photon.Realtime;

// public class NetworkConnectManager : MonoBehaviourPunCallbacks
// {
//     public Button BtnConnectMaster;
//     public Button BtnConnectRoom;

//     public bool TriesToConnectToMaster;
//     public bool TriesToConnectToRoom;


//     // Start is called before the first frame update
//     void Start()
//     {
//         TriesToConnectToMaster = false;
//         TriesToConnectToRoom = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
//         BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
//     }

//     // public void OnClick()
//     // {
//     //     BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
//     //     BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
//     // }

//     public void OnClickConnectToMaster()
//     {
//         //Setting {all optional and only for tutorial purpose}
//         PhotonNetwork.OfflineMode = false;
//         PhotonNetwork.NickName = "PlayerName";
//         PhotonNetwork.AutomaticallySyncScene = true; //PhotonNetwork.LoadLevel()
//         //PhotonNetwork.GameVersion = "v1"; // 버전관리 (같은 버전에서 구현가능) 

//         TriesToConnectToMaster = true;
//         //PhotonNetwork.ConnectToMaster(ip,port,appid); // manual Connection
//         PhotonNetwork.ConnectUsingSettings(); // Config파일 안에 있는 포톤서버가 자동적으로 연결될 수 있도록 함. 

//     }
//     public override void OnDisconnected(DisconnectCause cause)
//     {
//         base.OnDisconnected(cause);
//         TriesToConnectToMaster = false;
//         TriesToConnectToRoom = false;
//         Debug.Log(cause);
//     }

//     public override void OnConnectedToMaster()
//     {
//         base.OnConnectedToMaster();
//         TriesToConnectToMaster = false;
//         Debug.Log("Connected to Master!");
//     }


// }
