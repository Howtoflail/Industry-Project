using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMemoryGame : MonoBehaviour
{
    public GameObject UiCanvas;
    void Start()
    {
        UiCanvas = GameObject.Find("UI Canvas");
    }



    public void LoadScene()
    {
        
        SceneManager.LoadScene("MemoryGame", LoadSceneMode.Additive);
        
    }
}
