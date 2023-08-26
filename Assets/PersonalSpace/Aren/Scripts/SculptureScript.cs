using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class SculptureScript : MonoBehaviour
{
    private bool _isAggro = false;
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Collider _collider;
    private Animator _animator;
    [SerializeField] private float _animSpeed;
    private NavMeshAgent _agent;
    private Transform _head;
    [SerializeField] private Transform _humanoidRig;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
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

        _agent.destination = _player.position;
    }


    private void Halt()
    {
        _animator.speed = 0;
        _agent.SetDestination(transform.position);
    }


    private bool IsInCamView()
    {
        // Calculate the bounds of the object's collider
        Bounds _objBounds = _collider.bounds;

        bool _inSight = true;

        int otherLayers = ~LayerMask.GetMask("Player", "Sculpture");
        RaycastHit hitInfo;
        if (Physics.Raycast(_mainCam.transform.position, (transform.position - _mainCam.transform.position).normalized, out hitInfo, 100, otherLayers))
        {
            if (hitInfo.collider.gameObject.layer != LayerMask.NameToLayer("SculptureLimbs"))
            {
                _inSight = false;

                Debug.DrawRay(_mainCam.transform.position, (transform.position - _mainCam.transform.position).normalized * 100, Color.red, 1);
                Debug.Log(hitInfo.collider.gameObject.name);
            }

        }

        // Check if the object's bounds are within the camera's view frustum
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_mainCam), _objBounds) && _inSight;
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
