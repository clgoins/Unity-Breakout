using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject lifeImageTemplate;
    private List<GameObject> lifeImages;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Canvas canvas;

    public void Awake()
    {
        lifeImages = new List<GameObject>();
    }

    public void updateLives(int lives)
    {
        Vector3 offset = new Vector3(25, 0, 0);

        for (int i = 0; i < lifeImages.Count; i++)
        {
            Destroy(lifeImages[i]);
        }

        lifeImages = new List<GameObject>();

        for (int i = 0; i < lives; i++)
        {
            GameObject newImage = Instantiate(lifeImageTemplate);
            newImage.transform.SetParent(canvas.transform);
            newImage.transform.localPosition = lifeImageTemplate.transform.localPosition + offset * i;
            lifeImages.Add(newImage);
        }
    }

    public void updateScore(int score)
    {
        scoreText.SetText("" + score);
    }

    public void updateStage(int stage)
    {
        stageText.SetText("" + stage);
    }    
}
