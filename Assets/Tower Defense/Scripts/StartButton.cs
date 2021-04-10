using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public Button startButton;
    
    void Start()
    {
        if (startButton != null)
        {
            Button btn = startButton.GetComponent<Button>();
            btn.onClick.AddListener(startGame);   
        }
    }

    void startGame()
    {
        SceneManager.LoadScene("TowerDefense");
    }
}
