using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameConncection : MonoBehaviourPunCallbacks
{
    public Text chatLog;
    
    private void Awake()
    {
        chatLog.text += "\nConnect to service...";
        PhotonNetwork.LocalPlayer.NickName = "GameLoot_" + Random.Range(0,1000);
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        chatLog.text += "\nConnect to service!";

        if(PhotonNetwork.InLobby == false)
        {
            chatLog.text += "\nEnter to Lobby...";
            PhotonNetwork.JoinLobby();
        }
    }


    public override void OnJoinedLobby()
    {
        chatLog.text += "\nEnter to Lobby!";
        
        chatLog.text +="\nEntering to the GameLoot...";
        PhotonNetwork.JoinRoom("GameLoot");
    }

    public override void OnJoinRoomFailed(short returnCode, string message) //방에 사람이 채워져 있거나 그렇지 않을경우
    {
        chatLog.text += "\nError entering Room: " + message + " | code: " + returnCode;

        if(returnCode == ErrorCode.GameDoesNotExist)
        {
            chatLog.text += "\nCreating GameLoot Room...";

            RoomOptions roomOptions = new RoomOptions {MaxPlayers = 20};
            PhotonNetwork.CreateRoom("GameLoot", roomOptions, null);
        } 
    }
    //-------------------------------------------------------------------------

    public override void OnLeftRoom()
    {
        chatLog.text +="\nLeft from GameLoot!";
    }


    public override void OnJoinedRoom()
    {
        chatLog.text += "\nYou have entered the GameLoot room! Your username is : " + PhotonNetwork.LocalPlayer;
        Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        Quaternion rot = Quaternion.Euler(Vector3.up * Random.Range(0.0f, 360.0f));
        PhotonNetwork.Instantiate("GamePlayer", pos, rot);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) 
    {
        chatLog.text += "\nPlayer in the room GameLoot! 0 NickName is : " + newPlayer.NickName; 
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        chatLog.text += "\nWelcome to GameLoot!! 0 NickName is : " + otherPlayer.NickName;
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        chatLog.text += "\nAn error has occurred! " + errorInfo.Info;
    }
}