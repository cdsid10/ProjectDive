using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSequenceScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SculptureScript.Aggro();
        }
    }
}
