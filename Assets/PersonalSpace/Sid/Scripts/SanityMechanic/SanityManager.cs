using System;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.SanityMechanic
{
    public class SanityManager : MonoBehaviour
    {
        [field: SerializeField] public float MaxSanity { get; private set; }
        [field: SerializeField] public float CurrentSanityLeft { get; private set; }
        [field: SerializeField] public float SanityDepletionRate { get; private set; }
        
        //can set to true or false on triggers in the levels
        [field: SerializeField] public bool ShouldSanityDeplete { get; set; }

        private void Awake()
        {
            CurrentSanityLeft = MaxSanity;
        }

        private void Update()
        {
            if (!ShouldSanityDeplete) return;

            CurrentSanityLeft = Mathf.Clamp(CurrentSanityLeft - (SanityDepletionRate * Time.deltaTime), 0, MaxSanity);

            if (CurrentSanityLeft > 0) return;
            
            //game over stuff
            
        }

        public void DepleteSomeSanity(float depleteAmount)
        {
            CurrentSanityLeft -= depleteAmount;
        }
    }
}