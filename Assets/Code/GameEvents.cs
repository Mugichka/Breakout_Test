using System;

public static class GameEvents 
{
    public static Action OnBrickHit;
    public static Action OnBallDrop;
    public static Action OnAllBricksDestroyed;
    public static Action OnWallHit;
    public static Action<int> TotalBricksCount;
}
