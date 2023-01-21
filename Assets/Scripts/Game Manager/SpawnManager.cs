using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int CurrentNumberOfEnemies;
    [SerializeField] private int _maxNumverOfEnemies;
    [SerializeField] private int _currentNumberOfEnemies;

    [SerializeField] private float _enemySpawnFrequency;
    [SerializeField] private float _asteroidSpawnFrequency;
    [SerializeField] private float _trippleShotTimeLowerLimit;
    [SerializeField] private float _trippleShotTimeUpperLimit;
    [SerializeField] private float _speedBoostTimeLowerLimit;
    [SerializeField] private float _speedBoostTimeUpperLimit;
    [SerializeField] private float _shieldTimeLowerLimit;
    [SerializeField] private float _shieldTimeUpperLimit;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject asteroid;
    [SerializeField] private Transform enemyContainer;

    [SerializeField] private GameObject trippleShotPowerUp;
    [SerializeField] private GameObject speedBoostPowerUp;
    [SerializeField] private GameObject shieldPowerUp;

    private float _upperBound = 7.2f;
    private float _leftBound = - 9.5f;
    private float _rightBound;

    private Player player;

    void Start()
    {
        _rightBound = -_leftBound;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnAsteroid());
        StartCoroutine(SpawnTrippleShot());
        StartCoroutine(SpawnSpeedBoost());
        StartCoroutine(SpawnShield());
        
    }

    private void Update()
    {
        _currentNumberOfEnemies = SpawnManager.CurrentNumberOfEnemies;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForEndOfFrame();
        while(SpawnManager.CurrentNumberOfEnemies <= this._maxNumverOfEnemies)
        {   
            if(player.IsPlayerAlive)
            {
                InstantiateNPC(enemy);
                SpawnManager.CurrentNumberOfEnemies++;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(_enemySpawnFrequency);
        }
    }

    IEnumerator SpawnAsteroid()
    {
        yield return new WaitForEndOfFrame();
        while (SpawnManager.CurrentNumberOfEnemies <= this._maxNumverOfEnemies)
        {
            if (player.IsPlayerAlive)
            {
                InstantiateNPC(asteroid);
                SpawnManager.CurrentNumberOfEnemies++;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(_asteroidSpawnFrequency);
        }
    }

    IEnumerator SpawnTrippleShot()
    {   
        while(true)
        {
            float waitSeconds = Random.Range(_trippleShotTimeLowerLimit, _trippleShotTimeUpperLimit);
            yield return new WaitForSeconds(waitSeconds);
            if (player.IsPlayerAlive)
                InstantiateNPC(trippleShotPowerUp);
            else
                break;
        }
    }

    IEnumerator SpawnSpeedBoost()
    {
        while (true)
        {
            float waitSeconds = Random.Range(_speedBoostTimeLowerLimit, _speedBoostTimeUpperLimit);
            yield return new WaitForSeconds(waitSeconds);
            if (player.IsPlayerAlive)
                InstantiateNPC(speedBoostPowerUp);
            else
                break;
        }
    }

    IEnumerator SpawnShield()
    {
        while (true)
        {
            float waitSeconds = Random.Range(_shieldTimeLowerLimit, _shieldTimeUpperLimit);
            yield return new WaitForSeconds(waitSeconds);
            if(!player.IsShieldPowerUpActive)
            {
                if (player.IsPlayerAlive)
                    InstantiateNPC(shieldPowerUp);
                else
                    break;
            }
        }
    }

    private void InstantiateNPC(GameObject npc)
    {
        float xPos = Random.Range(_leftBound, _rightBound);
        Instantiate(npc, new Vector3(xPos, _upperBound, 0), Quaternion.identity, enemyContainer);
    }
}
