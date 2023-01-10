using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    class UI:MonoBehaviour
    { 
        private void Awake()
        {
            GamePassEvent.RegisterEvent(OnGamePass);
        }

        private void OnGamePass()
        {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }


        private void OnDestroy()
        {
            GamePassEvent.UnRegisterEvent(OnGamePass);
        }
    }
}
