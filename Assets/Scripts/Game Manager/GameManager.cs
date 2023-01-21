using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public GameObject explosion;

    [SerializeField] private bool isSinglePlayerMode;

    public float upperBound;
    public float lowerBound;
    public float rightBound;
    public float leftBound;

    public static GameManager gameManager;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        if (GameManager.gameManager == null)
        {
            GameManager.gameManager = this;
        }
        else
        {
            if(GameManager.gameManager != this)
            {
                Destroy(GameManager.gameManager.gameObject);
                GameManager.gameManager = this;
            }
            
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public bool IsSinglePlayerMode
    {
        get => isSinglePlayerMode;
    }

    public void GameOver()
    {
        this.isGameOver = true;
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R));
        SpawnManager.CurrentNumberOfEnemies = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
