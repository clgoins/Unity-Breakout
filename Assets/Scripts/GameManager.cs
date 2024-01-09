using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int blockCount;
    [SerializeField] private int stageNumber;
    [SerializeField] private int livesRemaining;
    [SerializeField] private GameObject blockTemplate;
    [SerializeField] private GameObject ballTemplate;
    [SerializeField] private Vector2 ballStartPosition;
    [SerializeField] private Vector2 blockStartPosition;

    void Start()
    {
        livesRemaining = 5;
        score = 0;
        stageNumber = 1;

        createNewStage(stageNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createNewStage(int stage)
    {
        int blockColumns = 14;
        int blockRows = 4;
        Vector2 newPos = blockStartPosition;
        for (int i = 0; i < blockRows; i++)
        {
            for (int j = 0; j < blockColumns; j++)
            {
                GameObject.Instantiate(blockTemplate, newPos, Quaternion.identity);
                blockCount++;
                newPos.x = blockStartPosition.x + ((j + 1) * blockTemplate.transform.localScale.x);
            }
            newPos.y = blockStartPosition.y - ((i + 1) * blockTemplate.transform.localScale.y);
            newPos.x = blockStartPosition.x;
        }

        GameObject.Instantiate(ballTemplate, ballStartPosition, Quaternion.identity);
    }

    public void blockDestroyed()
    {
        blockCount--;
        score++;
    }
}
