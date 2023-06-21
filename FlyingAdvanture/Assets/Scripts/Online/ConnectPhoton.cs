using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomPhoton
{
    public class ConnectPhoton : PhotonSingleTon<ConnectPhoton>
    {
        bool isConnecting = false;
        float waitTime = 1f;
        float waitSum = 0f;

        bool isWantConnect = false;
        public bool IsWantConnect
        {
            get
            {
                return isWantConnect;
            }
            set 
            {
                isWantConnect = value;
                if (isWantConnect)
                {
                    Connect();
                    CheckConnect();
                }
                else
                {
                    Disconnect();
                }
            }
        }
       
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        bool Connect()
        {
            PhotonNetwork.GameVersion = GlobalData.PhotonGameVersion;
            return PhotonNetwork.ConnectUsingSettings();
        }

        public bool IsConnectPhoton()
        {       
            return PhotonNetwork.IsConnected;
        }

        void CheckConnect()
        {
            StopAllCoroutines();
            StartCoroutine(CoCheckConnectedPhoton());
        }

        IEnumerator CoCheckConnectedPhoton()
        {
            yield return new WaitForSeconds(0.1f);
            var waitor = new WaitForSeconds(0.01f);
            while (true)
            {
                if (IsConnectPhoton() == false && isConnecting == false)
                {
                    isConnecting = true;
                    Connect();
                }
                if (isConnecting)
                {
                    waitSum += Time.deltaTime;
                    if (waitSum >= waitTime)
                    {
                        waitSum = 0f;
                        isConnecting = false;
                    }
                }
                yield return waitor;
            }
        }   

        void Disconnect()
        {
            StopAllCoroutines();
            PhotonNetwork.Disconnect();
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            // 포톤에 연결되었을 경우
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDisconnected reason {0}", cause);
        }
    }
}
