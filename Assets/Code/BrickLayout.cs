using UnityEngine;
public class BrickLayout
{
    private Vector3 startPosition;
    private float brickWidth;
    private float brickHeight;
    private float padding;
    private BrickLayoutConfig brickLayoutConfig;
    private IBrickPool brickPool;
    private Transform parentTransform;

    public BrickLayout(Vector3 startPosition, float brickWidth, float brickHeight, float padding, BrickLayoutConfig brickLayoutConfig, IBrickPool brickPool, Transform parentTransform)
    {
        this.startPosition = startPosition;
        this.brickWidth = brickWidth;
        this.brickHeight = brickHeight;
        this.padding = padding;
        this.brickLayoutConfig = brickLayoutConfig;
        this.brickPool = brickPool;
        this.parentTransform = parentTransform;
    }

    public void SpawnBricks()
    {
        for (int row = 0; row < brickLayoutConfig.rows; row++)
        {
            GameObject rowObject = new GameObject($"Row_{row}");
            rowObject.transform.parent = parentTransform;
            rowObject.transform.localPosition = Vector3.zero;
            brickPool.RegisterRow(rowObject, brickLayoutConfig.columns);

            for (int col = 0; col < brickLayoutConfig.columns; col++)
            {
                float xPos = startPosition.x + col * (brickWidth + padding);
                float yPos = startPosition.y - row * (brickHeight + padding);
                Vector3 brickPosition = new Vector3(xPos, yPos, 0);

                GameObject brick = brickPool.GetBrickFromPool();
                brick.transform.position = brickPosition;
                brick.transform.parent = rowObject.transform;

                SetColor(brick, row);
            }
        }
    }

    private void SetColor(GameObject brick, int row)
    {
        Renderer brickRenderer = brick.GetComponent<Renderer>();
        if (row < brickLayoutConfig.rowColors.Length)
        {
            var color= brickLayoutConfig.rowColors[row];
            brickRenderer.material.color = color;
            SetEmission(brickRenderer,color);
        }
        else
        {
            brickRenderer.material.color = Color.white;
        }
    }

    private void SetEmission(Renderer brickRenderer, Color color)
    {
        var mat=brickRenderer.materials[1];
        //mat.EnableKeyword("_EMISSION");
        mat.SetColor("_OutlineColor",color*2);

    }
}