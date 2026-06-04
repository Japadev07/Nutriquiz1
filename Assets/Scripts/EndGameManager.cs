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

        if (playerScore > masterScore)
        {
            Win.SetActive(true);
            lose.SetActive(false);
            draw.SetActive(false);
        }
        else if (playerScore < masterScore)
        {
            lose.SetActive(true);
            Win.SetActive(false);
            draw.SetActive(false);
        }
        else
        {
            draw.SetActive(true);
            Win.SetActive(false);
            lose.SetActive(false);
        }
    }

    public void NovoJogo()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("MasterScore", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("NovoJogo");
    }

    public void Voltar()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("MasterScore", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }
}