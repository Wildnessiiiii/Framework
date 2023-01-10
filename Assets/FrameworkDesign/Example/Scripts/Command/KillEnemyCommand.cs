using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public struct KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            var gameModel = PointGame.Get<IGameModel>();
            gameModel.KillCount.Value++;

            if (gameModel.KillCount.Value == 9)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}

