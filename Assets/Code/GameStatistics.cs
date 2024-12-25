using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatistics
{
    private int totalBricks;
    private int bricksHit;

    public event System.Action<int, int> OnStatisticsUpdated;

    public GameStatistics(int totalBricks)
    {
        this.totalBricks = totalBricks;
        this.bricksHit = 0;
    }

    public void IncrementBricksHit()
    {
        bricksHit++;
        int bricksLeft = totalBricks - bricksHit;
        OnStatisticsUpdated?.Invoke(bricksHit, bricksLeft);
        if(bricksLeft == 0)
        {
            GameEvents.OnAllBricksDestroyed?.Invoke();
            //RestartGame();
        }
    }
    
}
