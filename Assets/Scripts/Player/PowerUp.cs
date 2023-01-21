using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _lowerBound = -5.5f;
    //ID for Powerups
    //0 = Tripple Shot
    //1 = Speed
    //2 = Shields
    [SerializeField]
    private int powerupId;

    [SerializeField] private AudioClip powerUpSound;
    private AudioSource _playerAudioSource;

    private Player player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        _playerAudioSource.clip = powerUpSound;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y < _lowerBound)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            _playerAudioSource.clip = powerUpSound;
            _playerAudioSource.Play();
            switch(powerupId)
            {
                case 0:
                    player.IsTrippleShotActive = true;
                    break;
                case 1:
                    player.IsSpeedPowerUpActive = true;
                    break;
                case 2:
                    player.IsShieldPowerUpActive = true;
                    break;
            }
            Destroy(this.gameObject);
            //StartCoroutine(DestroyMe());
        }
    }
}
