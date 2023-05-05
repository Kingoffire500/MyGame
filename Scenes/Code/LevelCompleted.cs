using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public Text roundsText;
    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }
    public void Continue()
    {
        SceneManager.LoadScene(2);
    }
    public void Menu1()
    {
        SceneManager.LoadScene(0);
    }
}
