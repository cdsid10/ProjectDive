using System;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.SanityMechanic
{
    public class QuickTimeEvent : MonoBehaviour
    {
        [field: SerializeField] public bool IsQteEnabled { get; set; }
        [SerializeField] private Trap currentTrap;
        
        private void Update()
        {
            if (! IsQteEnabled) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentTrap.TimeStuckFor -= 0.5f;
            }
        }

        public void SetTrapForQte(Trap trap)
        {
            currentTrap = trap;
        }
    }
}
