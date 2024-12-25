//using System;
using System.Collections.Generic;
using UnityEngine;



public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject vfxBrickHit;
    [SerializeField] private GameObject vfxBallDrop;
    [SerializeField] private GameObject vfxBallPlatformHit;

    public void PlayVFXBrickHit(Vector3 position)
    {
        GameObject vfx = Object.Instantiate(vfxBrickHit, position, Quaternion.identity);
    }
    public void PlayVFXBallDrop(Vector3 position)
    {

        GameObject vfx = Object.Instantiate(vfxBallDrop, position, Quaternion.identity);
    }
    public void PlayVFXBallWallHit(Vector3 position)
    {
        GameObject vfx = Object.Instantiate(vfxBallPlatformHit, position, Quaternion.identity);
    }
}
