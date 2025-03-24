using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayLogic : MonoBehaviour
{
    int NumHitNeed = 10;
    int NumHitDone = 0;
    float timeLimit = 10f; // Time limit in seconds
    float elapsedTime = 0f;
    float progressDecayRate = 0.1f; // Rate at which progress decreases over time

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI Timer;
    public RectTransform progressBar; // UI Image to move
    public ManagerJJ managerJJ; // Reference to the Mangaer

    void Start()
    {
        WinText.text = string.Empty;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeLimit)
        {
            LoseGame();
        }

        if (NumHitNeed <= 0)
        {
            WinGameL();
        }
        else if (NumHitDone == NumHitNeed)
        {
            WinGameN();
        }
        else if (NumHitDone == NumHitNeed && NumHitNeed <= 1000)
        {
            WinGameW();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NumHitDone += 1;
            UpdateProgressBar();
        }

        DecayProgress();

        Timer.text = $"{elapsedTime} / {timeLimit}";
    }

    public void SetDifficulty(int Diff)
    {
        NumHitNeed *= Diff;
    }

    void WinGameL()
    {
        WinText.text = "You Win.... Coward";
    }

    void WinGameN()
    {
        WinText.text = "You Win";
    }

    void WinGameW()
    {
        WinText.text = "OMG HOW??!?!?";
    }

    void LoseGame()
    {
        WinText.text = "You Lose!";
    }

    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            float progress = (float)NumHitDone / NumHitNeed;
            progressBar.anchoredPosition = new Vector2(progress * 300, progressBar.anchoredPosition.y);
        }
    }

    void DecayProgress()
    {
        if (NumHitDone > 0)
        {
            NumHitDone -= Mathf.CeilToInt(progressDecayRate * Time.deltaTime * NumHitNeed);
            NumHitDone = Mathf.Max(0, NumHitDone); // Ensure it doesn't go negative
            UpdateProgressBar();
        }
    }
}