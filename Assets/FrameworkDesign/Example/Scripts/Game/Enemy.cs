using UnityEngine;


namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour
    {
        private void OnMouseDown()
        {
            new KillEnemyCommand().Execute();
            Destroy(gameObject);
        }
    }
}


