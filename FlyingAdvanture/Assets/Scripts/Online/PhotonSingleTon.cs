using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomPhoton
{
    public abstract class PhotonSingleTon<T> : MonoBehaviourPunCallbacks where T : PhotonSingleTon<T>
    {
        static T instance = null;

        static bool isDestory = false;
        public static T Instance
        {
            get
            {
                if (isDestory)
                {
                    return null;
                }

                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                    instance.gameObject.name = typeof(T).Name;
                    DontDestroyOnLoad(instance.gameObject);
                    instance.Init();
                }
                return instance;
            }
        }

        public virtual void Init()
        {

        }

        protected virtual void OnDestroy()
        {
            isDestory = true;
            instance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            isDestory = true;
            instance = null;
        }
    }
}