using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted;
    public static event Action<bool> OnGameFinished;
    public static bool isGameStarted = false;
    public static bool isGameFinished = false;
    public static int scoreMultiplier = 0;
    public static int money = 0;
    private void Awake()
    {
        isGameStarted = false;
        isGameFinished = false;
    }
    public static void StartGame()
    {
        if (isGameStarted) return;
        isGameStarted = true;
        OnGameStarted?.Invoke();
    }
    public static void FinishGame(bool isWin)
    {
        if(isGameFinished) return;
        isGameFinished = true;
        OnGameFinished?.Invoke(isWin);
    }
}
