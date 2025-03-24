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
    float progressDecayRate = 0.01f; // Rate at which progress decreases over time

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI Timer;
    public RectTransform progressBar; // UI Image to move
    public ManagerJJ managerJJ; // Reference to the Mangaer

    public GameObject Pot, Broke;

    private bool gameOverJJ = false;

    void Start()
    {
        WinText.text = string.Empty;
        Pot.SetActive(true);
        Broke.SetActive(false);
    }

    void Update()
    {
        if (gameOverJJ == false)
        {
            elapsedTime += Time.deltaTime;
        }

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
            NumHitDone = NumHitDone + 1;
            Debug.Log(NumHitDone + " " + NumHitNeed);
        }

        UpdateProgressBar();

        Timer.text = $"{elapsedTime} / {timeLimit}";
    }

    public void SetDifficulty(int Diff)
    {
        NumHitNeed *= Diff;
    }

    void WinGameL()
    {
        gameOverJJ = true;
        WinText.text = "You Win.... Coward";

        Pot.SetActive(false);
        Broke.SetActive(true);
    }

    void WinGameN()
    {
        gameOverJJ = true;
        WinText.text = "You Win";

        Pot.SetActive(false);
        Broke.SetActive(true);
    }

    void WinGameW()
    {
        gameOverJJ = true;
        WinText.text = "OMG HOW??!?!?";

        Pot.SetActive(false);
        Broke.SetActive(true);
    }

    void LoseGame()
    {
        gameOverJJ = true;
        WinText.text = "You Lose!";

        Pot.SetActive(true);
        Broke.SetActive(false);
    }

    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            float progress = Mathf.Clamp01((float)NumHitDone / NumHitNeed); // Ensures progress is between 0 and 1
            float maxWidth = progressBar.parent.GetComponent<RectTransform>().rect.width; // Get the container's width

            progressBar.sizeDelta = new Vector2(progress * maxWidth, progressBar.sizeDelta.y); // Adjust width
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

    public void ResetJ()
    {
        WinText.text = string.Empty;
        Pot.SetActive(true);
        Broke.SetActive(false);

        elapsedTime = 0f;
        NumHitDone = 0;

        gameOverJJ = false;


    }

    public void LeaveGame()
    {
        ResetJ();

        gameOverJJ = true;

        managerJJ.MenuJason.MenuJJ.SetActive(true);

        managerJJ.MenuJason.GameplayJJ.SetActive(false);
    }
}