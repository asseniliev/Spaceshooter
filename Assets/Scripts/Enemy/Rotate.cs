using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float _rotateSpeed;
    private float _yRotation;

    void Start()
    {
        _rotateSpeed = Random.Range(-360, 360);
        int random = Random.Range(0, 2);
        float angle = (random == 0) ? 0 : 180;

        this.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(this.transform.position, Vector3.forward, _rotateSpeed * Time.deltaTime);
    }
}
