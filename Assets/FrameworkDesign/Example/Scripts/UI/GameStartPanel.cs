using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }

        private void Start()
        {
            transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                GetArchitecture().SendCommand<StartGameCommand>();
            });
        }
    }
}