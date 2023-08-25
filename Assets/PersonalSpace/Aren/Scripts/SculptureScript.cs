using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptureScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private bool _isAggro = false;
    private bool _inCamView = false;
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _mainCam;
    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        Aggro();
    }


    private void Aggro()
    {
        _isAggro = true;
    }


    private void Chase()
    {
        Vector3 _playerXZ = _player.transform.position;
        _playerXZ.y = 0;

        transform.LookAt(_playerXZ);
        rb.AddForce(transform.forward * _moveSpeed);
    }


    private void Halt()
    {

    }


    private void FixedUpdate()
    {
        if (!_inCamView)
        {
            Chase();
        }
        else
        {
            Halt();
        }
    }
}
