using UnityEngine;


namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }

        private void OnMouseDown()
        {
            GetArchitecture().SendCommand<KillEnemyCommand>();
            Destroy(gameObject);
        }
    }
}


