using System;
using System.Collections;
using StarterAssets;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.SanityMechanic
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float sanityDepletionAmount;
        [field: SerializeField] public float TimeStuckFor { get; set; }
        [SerializeField] private bool isTrapTriggered;
        [SerializeField] private bool trapAlreadyTriggered;

        private FirstPersonController controller;
        private QuickTimeEvent quickTimeEvent;

        private void Update()
        {
            if (!isTrapTriggered) return;

            TimeStuckFor -= Time.deltaTime;
            if (!(TimeStuckFor <= 0)) return;
            
            controller.CanMove = true;
            quickTimeEvent.IsQteEnabled = false;
            quickTimeEvent.SetTrapForQte(null);
            isTrapTriggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (trapAlreadyTriggered) return;
            if (!other.TryGetComponent(out SanityManager sanityManager)) return;

            StartCoroutine(ActivateTrap(sanityManager));
        }

        IEnumerator ActivateTrap(SanityManager sanityManager)
        {
            yield return new WaitForSeconds(0.25f);

            var fpsController = sanityManager.gameObject.GetComponent<FirstPersonController>();
            var qte = sanityManager.gameObject.GetComponent<QuickTimeEvent>();

            controller = fpsController;
            fpsController.CanMove = false;

            sanityManager.DepleteSomeSanity(sanityDepletionAmount);

            quickTimeEvent = qte;
            qte.SetTrapForQte(this);
            qte.IsQteEnabled = true;

            isTrapTriggered = true;
            trapAlreadyTriggered = true;
            //ui manager and sound
        }
    }
}