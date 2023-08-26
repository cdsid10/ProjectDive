using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

namespace PersonalSpace.Sid.Scripts.CheckPoints
{
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private Transform checkpointRespawnTransform;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out FirstPersonController firstPersonController)) return;
            firstPersonController.transform.position = checkpointRespawnTransform.position;
            firstPersonController.transform.rotation = checkpointRespawnTransform.rotation;
        }
    }
}