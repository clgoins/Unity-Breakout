using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGenerator
{

    [SerializeField] private int rowCount = 4;         //number of rows of blocks
    [SerializeField] private int colCount = 12;        // number of columns of blocks
    private const float playFieldWidth = 22;           // Width of the play field in unity units
    private const float playFieldHeight = 3;           // Height of the play field in unity units

    [SerializeField] private Vector2 blockGenStartPosition;  // Starting position for block gen code
    [SerializeField] private float initialBlockFrequency = 0.7f;    // Default probability a block is to spawn in each cell

    [SerializeField] private int levelToIncreaseDifficulty;     // How many levels before difficulty should be increased

    public Vector2[] newLevel(int stageNumber, out Vector2 blockScale)
    {
        int newRowCount;
        int newColCount;
        float blockFrequency = initialBlockFrequency;

        // Increase number of rows every 4 levels by default
        newRowCount = rowCount + Mathf.FloorToInt((stageNumber - 1) / levelToIncreaseDifficulty) * 2;
        // Increase number of columns every 8 levels by default
        newColCount = colCount + Mathf.FloorToInt((stageNumber - 1) / (levelToIncreaseDifficulty * 2)) * 2;

        // In between the fourth levels; just increase the frequency of blocks spawning.
        if (!((stageNumber - 1) % levelToIncreaseDifficulty == 0))   
            blockFrequency += (1 - initialBlockFrequency) / (levelToIncreaseDifficulty - 1);

        blockScale = Vector2.zero;
        return generateBlockPositions(newRowCount, newColCount, blockFrequency, out blockScale);
    }


    /* Divides play field into cells. Cell size is based on desired # of blocks.
    Top left quadrant is populated with blocks randomly, based on blockFrequency.
    Other 3 corners are then mirrored to create a symmetric play field. */
    // !! First index of returned array will always be blockScale
    private Vector2[] generateBlockPositions(int rowCount, int colCount, float frequency, out Vector2 scale)
    {
        // Calculate positions based on top left quadrant
        int newRows = rowCount / 2;
        int newCols = colCount / 2;
        
        List<Vector2> positions = new List<Vector2>();
        Vector2 blockScale;
        blockScale.x = playFieldWidth / colCount;
        blockScale.y = playFieldHeight / rowCount;

        scale = blockScale;

        // For each desired column & row
        for (int i = 0; i < newCols; i++)
        {
            for (int j = 0; j < newRows; j++)
            {
                // Randomly create block positions with probability 'frequency'
                if (Random.Range(0.0f, 1.0f) <= frequency)
                {
                    // Generate 4 positions; one for each quadrant of play field. Play field should be mirrored left/right and top/bottom
                    positions.Add(new Vector2(blockGenStartPosition.x + i * blockScale.x, blockGenStartPosition.y - j * blockScale.y));
                    positions.Add(new Vector2(blockGenStartPosition.x + playFieldWidth - blockScale.x - i * blockScale.x, blockGenStartPosition.y - j * blockScale.y));
                    positions.Add(new Vector2(blockGenStartPosition.x + i * blockScale.x, blockGenStartPosition.y - playFieldHeight + blockScale.y + j * blockScale.y));
                    positions.Add(new Vector2(blockGenStartPosition.x + playFieldWidth - blockScale.x - i * blockScale.x, blockGenStartPosition.y - playFieldHeight + blockScale.y + j * blockScale.y));
                }
            }
        }

        return positions.ToArray();
    }
}
