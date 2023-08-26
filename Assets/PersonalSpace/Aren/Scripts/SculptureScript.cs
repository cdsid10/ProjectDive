using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptureScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private bool _isAggro = false;
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _mainCam;
    Rigidbody rb;
    [SerializeField] private Collider _collider;
    private Animator _animator;
    [SerializeField] private float _animSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
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
        _animator.SetBool("Walking", true);
        _animator.speed = _animSpeed;

        Vector3 _playerXZ = _player.transform.position;
        _playerXZ.y = transform.position.y;

        transform.LookAt(_playerXZ);

        // set velocity directly to achieve constant velocity
        rb.velocity = transform.forward * _moveSpeed;
    }


    private void Halt()
    {
        _animator.speed = 0;
    }


    private bool IsInCamView()
    {
        // Calculate the bounds of the object's collider
        Bounds _objBounds = _collider.bounds;

        // Check if the object's bounds are within the camera's view frustum
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_mainCam), _objBounds);
    }


    private void FixedUpdate()
    {
        if (_isAggro)
        {
            if (!IsInCamView())
            {
                Chase();
            }
            else
            {
                Halt();
            }
        }
    }
}
