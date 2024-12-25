using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrickLayoutConfig", menuName = "Breakout/Brick Layout Config")]
public class BrickLayoutConfig : ScriptableObject
{
    [Range(1, 20)] public int columns = 10; // Number of columns
    [Range(1, 10)] public int rows = 5; // Number of rows
    public Color[] rowColors; // Array of colors for each row
}
