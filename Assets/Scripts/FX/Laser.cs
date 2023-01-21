using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;


    private float _upperBound = 7.5f;
    private UIManager uiManager;

    private GameManager gameManager;
    private Player player;

    private void Awake()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if(playerGO != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        
    }

    private void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Move();
        DestroyThis();
    }

    public void Move()
    {
        this.transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    public void DestroyThis()
    {   
        if (this.transform.position.y > _upperBound)
        {
            Destroy(this.gameObject);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Enemy")
        {
            uiManager.UpdateScore(other.transform.GetComponent<Enemy>().BonusKillValue);
            Instantiate(gameManager.explosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            SpawnManager.CurrentNumberOfEnemies--;
            Destroy(this.gameObject);
        } 
        else if(other.transform.tag == "Player")
        {
            if (player.IsShieldPowerUpActive)
            {
                player.IsShieldPowerUpActive = false;
            }
            else
            {
                player.TakeDamage(_damage);
            }
            Destroy(this.gameObject);
        }
    }
}
