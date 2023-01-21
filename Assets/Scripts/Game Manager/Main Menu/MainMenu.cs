using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int singlePlayerScene;
    public int coOpScene;
    
    public void LoadSinglePlayerScene()
    {
        //Debug.Log("Load Single Player Scene");
        SceneManager.LoadScene(singlePlayerScene); 
    }

    public void LoadCoOpScene()
    {
        //Debug.Log("Load Multi Player Scene");
        SceneManager.LoadScene(coOpScene);
    }
}
