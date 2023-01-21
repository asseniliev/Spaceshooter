using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _upperBound = 7.2f;
    private float _lowerBound = -5.5f;
    private float _leftBound = 9.5f;
    private float _rightBound;

    [SerializeField] private int _damageValue;
    [SerializeField] private int _bonusKillValue;

    [SerializeField] private float _shootIntervalLowerValue;
    [SerializeField] private float _shootIntervalUpperValue;

    [SerializeField] GameObject _laser;

    private Player player;

    public int DamageValue { get => _damageValue; }
    public int BonusKillValue { get => _bonusKillValue; }

    private void Start()
    {
        _rightBound = -_leftBound;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        if(this.transform.position.y <= _lowerBound)
        {
            if (player.IsPlayerAlive)
                ReSpawnAtTop();
            else
                Destroy(this.gameObject);
        }
    }

    private void ReSpawnAtTop()
    {
        float xPos = Random.Range(_leftBound, _rightBound);
        this.transform.position = new Vector3(xPos, _upperBound, 0);
    }
}
