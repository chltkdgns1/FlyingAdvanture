using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomPhoton
{
    public class Matching : PhotonSingleTon<Matching>
    {
        public bool JoinRoom()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return false;

            return PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return;

            // 게임 룸 진입
            LoadSceneManager.instance.LoadScene(StringList.InGameScene);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            CreateRoom();
        }

        public bool CreateRoom(int maxNumber = 4)
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return false;

            int randNumber = UnityEngine.Random.Range(0, 100000);
            return PhotonNetwork.CreateRoom("Room_" + DateTime.Now.Ticks + "_" + randNumber, new Photon.Realtime.RoomOptions { MaxPlayers = maxNumber }, null);
        }

        public override void OnCreatedRoom()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return;

            LoadSceneManager.instance.LoadScene(StringList.InGameScene);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            PopupComponent.PopupShow<NoticePopup>("");
            //방을 만들 수 없다는 에러 호출
        }

        public void RoomClose()
        {
            if(PhotonNetwork.CurrentRoom == null)
            {
                // 에러 로그 찍고
                return;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
            else
            {
                // 센드 메세지
            }
        }

        public int GetRoomPlayer()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return -2;

            if (PhotonNetwork.CurrentRoom == null)
                return -1;

            return PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }
}
