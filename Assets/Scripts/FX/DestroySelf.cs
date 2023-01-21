using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float _destroyAfterSeconds;
    [SerializeField] private AudioClip destroySound;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.clip = destroySound;
        _audioSource.Play();
        Destroy(this.gameObject, _destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
