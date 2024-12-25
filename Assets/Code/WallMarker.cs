using System.Collections;
using System.Collections.Generic;
using Breakout_Test.Assets.Code.Sounds;
using UnityEngine;

public class WallMarker : MonoBehaviour,ISoundable
{
    [SerializeField] private string sound="WallHit";

    public string GetSound()
    {
        return sound;
    }
}
