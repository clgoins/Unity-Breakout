using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int rowCount;
    [SerializeField] private int colCount;
    private bool increaseRows = false;
    [SerializeField] private Vector2 blockGenStartPosition;
    [SerializeField] private int stageNumber;
    [SerializeField] private int livesRemaining;
    [SerializeField] private GameObject blockTemplate;  // Prefab referring to block object for duplication
    [SerializeField] private GameObject ballTemplate;   // Prefab referring to a ball object for duplication
    [SerializeField] private const float playFieldWidth = 22; // Width of the play field in unity units
    [SerializeField] private const float playFieldHeight = 3; // Height of the play field in unity units
    [SerializeField] private float initialBlockFrequency = 0.7f; // How likely a block is to spawn in each cell
    private float blockFrequency;

    void Start()
    {
        livesRemaining = 3;
        score = 0;
        stageNumber = 1;
        Block.OnBlockDestroyed += blockDestroyed;
        Ball.OnLostBall += lostBall;
        rowCount = 4;
        colCount = 12;
        blockFrequency = initialBlockFrequency;

        createStage();
    }


    /*
     Divides play field into cells. Cell size is based on desired # of blocks.
    Top left corner is populated with blocks randomly, based on blockFrequency.
    Other 3 corners are then mirrored to create a symmetric play field.
     */
    void createStage()
    {
        int newRows = rowCount / 2;
        int newCols = colCount / 2;
        Vector2 blockScale;
        blockScale.x = playFieldWidth / colCount;
        blockScale.y = playFieldHeight / rowCount;

        for (int i = 0; i < newCols; i++)
        {
            for (int j = 0; j < newRows; j++)
            {
                if (Random.Range(0.0f, 1.0f) <= blockFrequency)
                {
                    Vector2 position;

                    position.x = blockGenStartPosition.x + i * blockScale.x;
                    position.y = blockGenStartPosition.y - j * blockScale.y;
                    GameObject newBlock = Instantiate(blockTemplate, position, Quaternion.identity);
                    newBlock.transform.localScale = blockScale;

                    position.x = blockGenStartPosition.x + playFieldWidth - blockScale.x - i * blockScale.x;
                    position.y = blockGenStartPosition.y - j * blockScale.y;
                    newBlock = Instantiate(blockTemplate, position, Quaternion.identity);
                    newBlock.transform.localScale = blockScale;

                    position.x = blockGenStartPosition.x + i * blockScale.x;
                    position.y = blockGenStartPosition.y - playFieldHeight + blockScale.y + j * blockScale.y;
                    newBlock = Instantiate(blockTemplate, position, Quaternion.identity);
                    newBlock.transform.localScale = blockScale;

                    position.x = blockGenStartPosition.x + playFieldWidth - blockScale.x - i * blockScale.x;
                    position.y = blockGenStartPosition.y - playFieldHeight + blockScale.y + j * blockScale.y;
                    newBlock = Instantiate(blockTemplate, position, Quaternion.identity);
                    newBlock.transform.localScale = blockScale;


                }
            }
        }

    }

    public void blockDestroyed()
    {
        // Event gets called before block object is destroyed, so be sure to subtract 1 from total here
        int remainingBlocks = GameObject.FindGameObjectsWithTag("block").Length - 1;

        if (remainingBlocks <= 0)
            startNewLevel();
    }

    private void startNewLevel()
    {
        stageNumber++;

        // Every four levels, reset block frequency, and increase the number of rows or columns, in alternating order
        if ((stageNumber - 1) % 4 == 0)
        {
            if (increaseRows)
            {
                rowCount += 2;
                increaseRows = false;
            }
            else
            {
                colCount += 2;
                increaseRows = true;
            }

            blockFrequency = initialBlockFrequency;

        }
        // In between the fourth levels; just increase the frequency of blocks spawning.
        else
        {
            blockFrequency += (1 - initialBlockFrequency) / 3;
        }

        createStage();
    }


    public void lostBall()
    {

    }
}
