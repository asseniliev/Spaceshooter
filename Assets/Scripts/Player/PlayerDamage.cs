using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private GameObject[] FireBallLocations;
    [SerializeField] private int damageModifier = 1;
    private Player player;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        player = this.transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if(player.IsShieldPowerUpActive)
            {
                player.IsShieldPowerUpActive = false;
            }
            else
            {
                int damageValue = enemy.DamageValue;
                damageValue *= damageModifier;
                player.TakeDamage(damageValue);
                ActivateFireBalls();
            }
            Instantiate(gameManager.explosion, enemy.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            SpawnManager.CurrentNumberOfEnemies--;
        }
    }

    private void ActivateFireBalls()
    {
        foreach (var location in FireBallLocations)
        {
            location.SetActive(true);
        }
    }
}
