using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public GameObject singleShot;
    public GameObject trippleShot;

    public bool IsPlayerAlive { get => this.isAlive; }

    [SerializeField] private float speed;
    [SerializeField] private float speedBoostFactor;
    public float moveSpeed;

    [SerializeField] private float fireRate;
    private float nextFire = 0;

    [SerializeField] private int lives;

    [SerializeField] private Transform laserContainer;
    [SerializeField] private GameObject playerShield;

    [SerializeField] private float trippleShotActiveTime;
    [SerializeField] private float speedPowerUpActiveTime;
    [SerializeField] private float shieldPowerUpActiveTime;

    [SerializeField] private int score = 0;

    private bool isAlive = true;

    private bool isTrippleShotActive = false;
    private bool isSpeedPowerUpActive = false;
    public bool  isShieldPowerUpActive = false;

    private AudioSource audiosource;
    private GameManager gameManager;

    [SerializeField] private AudioClip shootSound;

    private IEnumerator trippleShootCoroutine;
    private IEnumerator speedPowerUpCoroutine;

    private string horizontalAxis;
    private string verticalAxis;
    private string shootAxis;

    private UIManager uiManager;

    public bool IsTrippleShotActive
    {
        get => this.isTrippleShotActive;
        set
        {
            this.isTrippleShotActive = value;
            if (value)
            {
                if (trippleShootCoroutine != null)
                    StopCoroutine(trippleShootCoroutine);

                trippleShootCoroutine = KeepTripleShotActive();
                StartCoroutine(trippleShootCoroutine);
                
            }   
        }
    }

    public bool IsShieldPowerUpActive
    {
        get => this.isShieldPowerUpActive;
        set
        {
            this.isShieldPowerUpActive = value;
            if (value)
                playerShield.SetActive(true);
            else
                playerShield.SetActive(false);
        }
    }

    public bool IsSpeedPowerUpActive
    {
        get => this.isSpeedPowerUpActive;
        set
        {
            this.isSpeedPowerUpActive = value;
            if (value)
            {
                if (speedPowerUpCoroutine != null)
                    StopCoroutine(speedPowerUpCoroutine);

                speedPowerUpCoroutine = KeepSpeedPowerUpActive();
                StartCoroutine(speedPowerUpCoroutine);

            }
        }
    }

    void Start()
    {
        int playerId = this.GetComponent<InitializePlayer>().PlayerId;
        this.gameManager = GameManager.gameManager;

        this.uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        this.uiManager.UpdateLivesImage(this.lives);

        this.audiosource = this.GetComponent<AudioSource>();
        this.horizontalAxis = "Horizontal" + playerId;
        this.verticalAxis = "Vertical" + playerId;
        this.shootAxis = "Fire" + playerId;
        Debug.Log(this.horizontalAxis);
    }

    void Update()
    {
        Move();

        if ((Input.GetAxisRaw(this.shootAxis) == 1) && Time.time > this.nextFire)
        {
            Shoot();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw(this.horizontalAxis);
        float verticalInput = Input.GetAxisRaw(this.verticalAxis);

        //float moveSpeed;

        if (this.IsSpeedPowerUpActive)
            moveSpeed = this.speed * this.speedBoostFactor;
        else
            moveSpeed = this.speed;

        this.transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

        this.transform.position = 
            new Vector3(Mathf.Clamp(this.transform.position.x, this.gameManager.leftBound, this.gameManager.rightBound),
                        Mathf.Clamp(this.transform.position.y, this.gameManager.lowerBound, this.gameManager.upperBound),
                        0);
    }

    private void Shoot()
    {
        if (IsTrippleShotActive)
            Instantiate(trippleShot, this.transform.position, Quaternion.identity, this.laserContainer);
        else
        {
            var laser = Instantiate(singleShot, this.transform.position, Quaternion.identity, this.laserContainer);
        }


        this.audiosource.clip = shootSound;
        if(this.isTrippleShotActive)
        {
            this.audiosource.pitch = 1.5f;
            this.nextFire = Time.time + this.fireRate / 1.5f;
        }
        else
        {
            this.audiosource.pitch = 1f;
            this.nextFire = Time.time + this.fireRate;
        }
        this.audiosource.Play();
    }

    public void TakeDamage(int damageValue)
    {
        this.lives -= damageValue;
        this.uiManager.UpdateLivesImage(this.lives);

        if (this.lives <= 0) Die();
    }

    private void Die()
    {
        this.isAlive = false;
        Instantiate(this.gameManager.explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator KeepTripleShotActive()
    {
        this.trippleShotActiveTime = Random.Range(3, 8);
        yield return new WaitForSeconds(this.trippleShotActiveTime);

        this.isTrippleShotActive = false;
    }

    IEnumerator KeepSpeedPowerUpActive()
    {
        this.speedPowerUpActiveTime = Random.Range(3, 8);
        yield return new WaitForSeconds(this.speedPowerUpActiveTime);

        this.isSpeedPowerUpActive = false;
    }
}
