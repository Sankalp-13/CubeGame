using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public string newGameString;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(newGameString);
    }
}
