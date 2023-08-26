using System;
using System.Collections;
using PersonalSpace.Sid.Scripts.Managers;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

namespace PersonalSpace.Sid.Scripts.CheckPoints
{
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private Transform checkpointRespawnTransform;
        [SerializeField] private AudioClip checkpointAudio;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out FirstPersonController firstPersonController)) return;
            StartCoroutine(FadeCheckpoint(firstPersonController));
        }

        IEnumerator FadeCheckpoint(FirstPersonController firstPersonController)
        {
            UIManager.Instance.FadeInCheckpointScreen(0.5f);
            SoundManager.Instance.PlayCheckpointSound(checkpointAudio);
            yield return new WaitForSeconds(0.5f);
            firstPersonController.transform.position = checkpointRespawnTransform.position;
            firstPersonController.transform.rotation = checkpointRespawnTransform.rotation;
            UIManager.Instance.FadeOutCheckpointScreen(0.5f);
        }
    }
}