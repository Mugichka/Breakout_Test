using System.Collections.Generic;
using UnityEngine;
public sealed class BrickPool : IBrickPool
{
    private Queue<GameObject> brickPool;
    private GameObject brickPrefab;
    private Dictionary<GameObject, int> rowBrickCount;
    private GameStatistics gameStatistics;

    public event System.Action<string> OnAllBricksReturned;

    public BrickPool(GameObject brickPrefab, int initialPoolSize, GameStatistics gameStatistics)
    {
        this.brickPrefab = brickPrefab;
        this.gameStatistics = gameStatistics;
        brickPool = new Queue<GameObject>();
        rowBrickCount = new Dictionary<GameObject, int>();
        InitializeBrickPool(initialPoolSize);
    }

    private void InitializeBrickPool(int initialPoolSize)
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject brick = Object.Instantiate(brickPrefab);
            brick.SetActive(false);
            brickPool.Enqueue(brick);
        }
    }

    public GameObject GetBrickFromPool()
    {
        if (brickPool.Count > 0)
        {
            GameObject brick = brickPool.Dequeue();
            brick.SetActive(true);
            return brick;
        }
        else
        {
            GameObject brick = Object.Instantiate(brickPrefab);
            brick.SetActive(false);
            return brick;
        }
    }

    public void ReturnBrickToPool(GameObject brick)
    {
        brick.SetActive(false);
        brickPool.Enqueue(brick);

        gameStatistics.IncrementBricksHit();

        // Track when a brick is returned to the pool
        GameObject rowObject = brick.transform.parent.gameObject;
        if (rowBrickCount.ContainsKey(rowObject))
        {
            rowBrickCount[rowObject]--;
            if (rowBrickCount[rowObject] == 0)
            {
                Debug.Log($"All bricks in {rowObject.name} are back in the pool.");
                OnAllBricksReturned?.Invoke($"All bricks in {rowObject.name} are back in the pool.");
            }
        }
    }

    public void RegisterRow(GameObject rowObject, int brickCount)
    {
        rowBrickCount[rowObject] = brickCount;
    }
}