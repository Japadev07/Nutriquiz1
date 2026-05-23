using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class DataBank
    {
        public string answerQuestion;
        public string[] FuorAnswer;
        public string qustions;
    }

    public DataBank[] dataBank;

    private int indexQuestions;

    // Lista de perguntas já usadas
    private List<int> perguntasUsadas = new List<int>();

    public Button[] AnswerButtons;

    public TextMeshProUGUI showQuestions;

    public Image timerAmount;

    [SerializeField] private float timer = 15;

    [SerializeField] private Color green;
    [SerializeField] private Color red;

    [SerializeField] private TextMeshProUGUI PlayerScore;
    [SerializeField] private TextMeshProUGUI MasterScore;
    [SerializeField] private TextMeshProUGUI QuestionCount;

    private int playerScore;
    private int masterScore;
    private int questionCount;
    private int starCount;

    public void Start()
    {
        starCount = PlayerPrefs.GetInt("StarCount");
        CheckQuestions();

    }

    private void Update()
    {
        PlayerScore.text = playerScore.ToString();
        MasterScore.text = masterScore.ToString();
        QuestionCount.text = questionCount.ToString();

        timer -= Time.deltaTime;

        timerAmount.fillAmount = timer / 15;

        if (timer <= 0)
        {
            Debug.Log("Tempo Finalizado");
        }

        // Finaliza após 10 perguntas
        if (questionCount > 9)
        {
            SceneManager.LoadScene("EndGame");
            Stars();
        }
    }

    private void CheckQuestions()
    {
        // Verifica se todas as perguntas já foram usadas
        if (perguntasUsadas.Count >= dataBank.Length)
        {
            Debug.Log("Todas as perguntas foram usadas!");
            SceneManager.LoadScene("EndGame");
            return;
        }

        // Sorteia pergunta sem repetir
        do
        {
            indexQuestions = Random.Range(0, dataBank.Length);
        }
        while (perguntasUsadas.Contains(indexQuestions));

        // Marca pergunta como usada
        perguntasUsadas.Add(indexQuestions);

        // Define respostas nos botões
        AnswerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text =
            dataBank[indexQuestions].FuorAnswer[0];

        AnswerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text =
            dataBank[indexQuestions].FuorAnswer[1];

        AnswerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text =
            dataBank[indexQuestions].FuorAnswer[2];

        AnswerButtons[3].GetComponentInChildren<TextMeshProUGUI>().text =
            dataBank[indexQuestions].FuorAnswer[3];

        // Mostra pergunta
        showQuestions.text = dataBank[indexQuestions].qustions;
    }

    private void CheckAnswer(int check)
    {
        // Verifica se a resposta clicada é a correta
        if (dataBank[indexQuestions].FuorAnswer[check] ==
            dataBank[indexQuestions].answerQuestion)
        {
            timer = 15;

            // Verde
            AnswerButtons[check].GetComponent<Image>().color = green;

            playerScore++;
            questionCount++;

            PlayerPrefs.SetInt("PlayerScore", playerScore);
            PlayerPrefs.SetInt("MasterScore", masterScore);
            PlayerPrefs.Save();
        }
        else
        {
            // Vermelho
            AnswerButtons[check].GetComponent<Image>().color = red;

            masterScore++;
            questionCount++;

            PlayerPrefs.SetInt("PlayerScore", playerScore);
            PlayerPrefs.SetInt("MasterScore", masterScore);
            PlayerPrefs.Save();
        }

        StartCoroutine(waitforNextQuestions(check));

        timer = 15;
    }

    IEnumerator waitforNextQuestions(int check)
    {
        yield return new WaitForSeconds(0.5f);

        CheckQuestions();

        // Volta cor do botão para branco
        AnswerButtons[check].GetComponent<Image>().color = Color.white;
    }

    public void ClickButtons(int check)
    {
        CheckAnswer(check);
    }
    void Stars()
    {
        if (playerScore > masterScore)
        {
            starCount += 9;

        }
        else if (playerScore < masterScore)
        {
            starCount -= 5;
        }
        PlayerPrefs.SetInt("StarCount", starCount);
        PlayerPrefs.Save();
    }
}