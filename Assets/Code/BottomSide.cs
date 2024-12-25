using System.Collections;
using System.Collections.Generic;
using Breakout_Test.Assets.Code.Sounds;
using UnityEngine;

public class BottomSide : MonoBehaviour, ISoundable
{
    [SerializeField] private string sound="WaterDrop";
    public string GetSound()
    {
        return sound;
    }
}
