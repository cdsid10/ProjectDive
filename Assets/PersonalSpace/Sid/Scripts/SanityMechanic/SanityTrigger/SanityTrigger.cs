using System;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.SanityMechanic.SanityTrigger
{
    public class SanityTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SanityManager sanityManager))
            {
                sanityManager.ShouldSanityDeplete = !sanityManager.ShouldSanityDeplete;
            }
        }
    }
}
