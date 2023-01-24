using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    class UI:MonoBehaviour, IController
    { 
        private void Awake()
        {
            //GamePassEvent.RegisterEvent(OnGamePass);
            this.RegisterEvent<GamePassEvent>(OnGamePass);
        }

        private void OnGamePass(GamePassEvent e)
        {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }


        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}
