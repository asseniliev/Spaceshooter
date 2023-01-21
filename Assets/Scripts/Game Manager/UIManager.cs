using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField ] private Text _scoreText;
    private int score = 0;
    private string scoreText = "Score: ";
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _LivesImage;
    [SerializeField] private Text _GameOverText;
    [SerializeField] private GameObject _RestartText;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = scoreText + score.ToString();
        _GameOverText.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreModifier)
    {
        score += scoreModifier;
        _scoreText.text = scoreText + score.ToString();
    }

    public void UpdateLivesImage(int currentLives)
    {
        if (currentLives <= 0)
        {
            _LivesImage.sprite = _liveSprites[0];
            ActivateGameOverSequence();
        }
        else
        {
            _LivesImage.sprite = _liveSprites[currentLives];
        }
    }

    private void ActivateGameOverSequence()
    {
        _GameOverText.gameObject.SetActive(true);
        StartCoroutine(FlickerText());

        _RestartText.SetActive(true);
        gameManager.GameOver();
    }


    IEnumerator FlickerText()
    {
        bool enabled = true;
        while (true)
        {
            _GameOverText.enabled = enabled;
            yield return new WaitForSeconds(0.5f);
            enabled = !enabled;
        }
    }

    
}
