using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;
    [Header("Win Canvas Settings")]
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI earnMoneyText;
    [Header("InGame Canvas Settings")]
    [SerializeField] private Slider progressSlider;
    [SerializeField] private ProgressHandler progressHandler;
    private void Start()
    {
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnGameFinished += GameManager_OnGameFinished;
        progressSlider.maxValue = 100;
    }
    private void Update()
    {
        if(GameManager.isGameStarted && !GameManager.isGameFinished)
        {
            progressSlider.value = progressHandler.Distance;
        }
    }
    private void GameManager_OnGameFinished(bool isWin)
    {
        if (isWin)
        {
            winCanvas.SetActive(true);
            SetWinCanvasTexts();
        }
        else
        {
            loseCanvas.SetActive(true);
        }
    }
    private void SetWinCanvasTexts()
    {
        multiplierText.text = GetMultiplierText(GameManager.scoreMultiplier);
        var earn = GameManager.scoreMultiplier * 10;
        GameManager.money += earn;
        earnMoneyText.text = "<sprite index=23> " + earn;
    }
    private static string GetMultiplierText(int modifier) 
    {
        var res = modifier switch
        {
            < 3 => "Good \n " + modifier + "X",
            < 6 => "Great \n " + modifier + "X",
            < 9 => "Perfect \n " + modifier + "X",
            _ =>  "Excellent \n " + modifier + "X",
        };
        return res;
    }
    public void GoNextLevel()
    {
        // There is one level so it will restart
        GameManager.isGameFinished = false;
        GameManager.isGameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void GameManager_OnGameStarted()
    {
        startCanvas.SetActive(false);
        inGameCanvas.SetActive(true);  
    }
    private void OnDisable()
    {
        GameManager.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.OnGameFinished -= GameManager_OnGameFinished;
    }
}
