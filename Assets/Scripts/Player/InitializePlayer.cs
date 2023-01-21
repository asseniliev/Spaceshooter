using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    [SerializeField] private int _playerId;
    private GameManager gameManager;

    public int PlayerId { get => _playerId; }

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Start()
    {
        if(gameManager.IsSinglePlayerMode)
        {
            this.transform.position = new Vector3(0, -3.5f, 0);
        }
        else
        {
            if(_playerId == 1)
                this.transform.position = new Vector3(4, -3.5f, 0);
            else
                this.transform.position = new Vector3(-4, -3.5f, 0); 
        }
    }


}
