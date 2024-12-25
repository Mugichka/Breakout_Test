using System;
using UnityEngine;


public interface IBrickPool
{
    GameObject GetBrickFromPool();
    void ReturnBrickToPool(GameObject brick);
    void RegisterRow(GameObject rowObject, int brickCount);
    event Action<string> OnAllBricksReturned;
}