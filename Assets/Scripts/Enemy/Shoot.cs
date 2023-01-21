using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float _shotMinFrequency;
    [SerializeField] private float _shotMaxFrequency;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform _laserContainer;



    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _pitchLowerValue;
    [SerializeField] private float _pitchUpperValue;
    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        _laserContainer = GameObject.Find("LaserContainer").transform;
        StartCoroutine(ProduceShots());
        _audioSource.pitch = Random.Range(_pitchLowerValue, _pitchUpperValue);
    }

    IEnumerator ProduceShots()
    {
        while (true)
        {
            float coolDownTime = Random.Range(_shotMinFrequency, _shotMaxFrequency);
            yield return new WaitForSeconds(coolDownTime);
            Instantiate(laserPrefab, this.transform.position, Quaternion.identity, _laserContainer);
            _audioSource.Play();
        }
    }
}

