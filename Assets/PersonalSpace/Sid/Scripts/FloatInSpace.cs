using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PersonalSpace.Sid.Scripts
{
    public class FloatInSpace : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            var randomRange = Random.Range(-10f, 10f);
            _rigidbody.AddTorque(randomRange, randomRange, randomRange);
        }

        private void FixedUpdate()
        {
            var randomRange = Random.Range(-0.1f, 0.1f);
            _rigidbody.AddForce(randomRange, randomRange, randomRange);
        }
    }
}
