using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomPhoton
{
    public class Matching : PhotonSingleTon<Matching>
    {
        public bool JoinRoom()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return false;

            return PhotonNetwork.JoinRoom("Room");
        }

        public override void OnJoinedRoom()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return;
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            CreateRoom();
        }

        public bool CreateRoom(int maxNumber = 4)
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return false;

            return PhotonNetwork.CreateRoom("Room", new Photon.Realtime.RoomOptions { MaxPlayers = maxNumber }, null);
        }


        public override void OnCreatedRoom()
        {
            if (ConnectPhoton.Instance.IsConnectPhoton() == false)
                return;
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            //PopupComponent.PopupShow<Noti>
            //방을 만들 수 없다는 에러 호출
        }
    }
}
