using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;
    public BrickLayoutConfig brickLayoutConfig;
    public Text messageText;

    public float brickWidth = 1.0f;
    public float brickHeight = 0.5f;
    public float padding = 0.1f;
    public Vector3 startPosition = new Vector3(-4.5f, 3f, 0);

    private IBrickPool brickPool;
    private BrickLayout brickLayout;
    private GameStatistics gameStatistics;

    void Start()
    {
        gameStatistics = new GameStatistics(brickLayoutConfig.rows * brickLayoutConfig.columns);
        brickPool = new BrickPool(brickPrefab, brickLayoutConfig.rows * brickLayoutConfig.columns, gameStatistics);
        brickLayout = new BrickLayout(startPosition, brickWidth, brickHeight, padding, brickLayoutConfig, brickPool, transform);
        brickPool.OnAllBricksReturned += DisplayMessage;
        gameStatistics.OnStatisticsUpdated += UpdateStatisticsDisplay;
        brickLayout.SpawnBricks();
        GameEvents.TotalBricksCount?.Invoke(brickLayoutConfig.rows * brickLayoutConfig.columns);
        Debug.Log("Started Spawner");
    }

    public void DestroyBrick(GameObject brick)
    {
        brickPool.ReturnBrickToPool(brick);
    }

    private void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    private void UpdateStatisticsDisplay(int bricksHit, int bricksLeft)
    {
        if (messageText != null)
        {
            messageText.text = $"Bricks hit: {bricksHit}, Bricks left: {bricksLeft}";
        }
    }
}