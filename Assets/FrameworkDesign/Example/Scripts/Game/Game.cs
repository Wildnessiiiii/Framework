using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    class  Game:MonoBehaviour
    {
        private void Awake()
        {
            GameStartEvent.RegisterEvent(OnGameStart);
        }

        private void OnGameStart()
        {
            transform.Find("Enemys").gameObject.SetActive(true);
        }


        private void OnDestroy()
        {
            GameStartEvent.UnRegisterEvent(OnGameStart);            
        }
    }
}
