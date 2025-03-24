using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuJason : MonoBehaviour
{
    public TMP_InputField difficultyInput; // Using TMP_InputField
    public ManagerJJ managerJJ; // Reference to the Mangaer
    public GameObject MenuJJ, GameplayJJ;

    void Start()
    {
        // Ensure the InputField only accepts numeric input
        difficultyInput.contentType = TMP_InputField.ContentType.IntegerNumber;
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(difficultyInput.text) && Input.GetKeyDown(KeyCode.Return))
        {
            managerJJ.gameplayLogic.SetDifficulty(int.Parse(difficultyInput.text)); // Pass value to GameplayLogic

            GameplayJJ.gameObject.SetActive(true);
            MenuJJ.gameObject.SetActive(false);
        }
    }

    public void QuitME()
    {
        Application.Quit();
    }
}
