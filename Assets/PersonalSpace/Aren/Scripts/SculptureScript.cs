using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class SculptureScript : MonoBehaviour
{
    [NonSerialized] public bool isAggro = false;
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Collider _collider;
    private Animator _animator;
    [SerializeField] private float _animSpeed;
    private NavMeshAgent _agent;
    private Transform _head;
    [SerializeField] private Transform _humanoidRig;
    [SerializeField] private Transform checkpoint;
    [SerializeField] private FirstPersonController _fpsController;
    [NonSerialized] public Vector3 _ogPos;
    [NonSerialized] public Quaternion _ogRot;
    [SerializeField] private AudioSource _screamAudio;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _ogPos = transform.position;
        _ogRot = transform.rotation;
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
        bool _inCam = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_mainCam), _objBounds);
        bool _inSight = true;

        if (_inCam)
        {
            int otherLayers = ~LayerMask.GetMask("Player", "ObjectsThatCanBePushed");

            RaycastHit hitInfo;
            if (Physics.Raycast(_mainCam.transform.position, (transform.position - _mainCam.transform.position).normalized, out hitInfo, 100, otherLayers))
            {
                int layer = hitInfo.collider.gameObject.layer;
                if ((layer != LayerMask.NameToLayer("SculptureLimbs") || layer != LayerMask.NameToLayer("Sculpture")) && transform.name.Contains("(1)"))
                {
                    _inSight = false;

                    Debug.DrawRay(_mainCam.transform.position, (transform.position - _mainCam.transform.position).normalized * 100, Color.red, 1);
                    Debug.Log(hitInfo.collider.gameObject.name);
                }

            }
        }

        Debug.Log(_inCam);
        // Check if the object's bounds are within the camera's view frustum
        return _inCam && _inSight;
    }


    private void FixedUpdate()
    {
        if (!IsInCamView())
        {
            if (isAggro)
            {
                Chase();
            }
        }
        else
        {
            isAggro = true;
            Halt();
        }
    }


    private static void ResetLevel()
    {
        foreach (SculptureScript sculptureScript in GameObject.Find("SculpturesFolder").GetComponentsInChildren<SculptureScript>())
        {
            sculptureScript.isAggro = false;
            sculptureScript.transform.SetPositionAndRotation(sculptureScript._ogPos, sculptureScript._ogRot);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _screamAudio.Play();
            _fpsController.transform.SetPositionAndRotation(checkpoint.position, checkpoint.rotation);
            ResetLevel();
        }
    }
}
