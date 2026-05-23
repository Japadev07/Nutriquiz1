using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerScore;
    [SerializeField] private TextMeshProUGUI MasterScore;

    private int playerScore;
    private int masterScore;


    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject draw;

    private void Start()
    {
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        masterScore = PlayerPrefs.GetInt("MasterScore", 0);

        PlayerScore.text = playerScore.ToString();
        MasterScore.text = masterScore.ToString();
    }

    private void Update()
    {
        if (playerScore > masterScore) 
        {
            Win.SetActive(true);

        }
        if (playerScore < masterScore)
        {
            lose.SetActive(true);

        }
        if (playerScore == masterScore)
        {
            draw.SetActive(true);
        }

    }

    public void WinGame()
    {
        Win.SetActive(true);
    }
    public void LoseGame()
    {
        lose.SetActive(true);
    }
    public void DrawGame()
    {
        draw.SetActive(true);
    }
}
