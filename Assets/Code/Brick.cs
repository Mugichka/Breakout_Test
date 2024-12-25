using System.Collections;
using System.Collections.Generic;
using Breakout_Test.Assets.Code.Sounds;
using UnityEngine;

public class Brick : MonoBehaviour, ISoundable
{
    [SerializeField] private string SoundOnCollision;

    public string GetSound()
    {
        return SoundOnCollision;
    }
}
