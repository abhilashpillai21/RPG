using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    [DisallowMultipleComponent]
    public class CinematicsTrigger : MonoBehaviour
    {
        bool isAlreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!isAlreadyTriggered && other.gameObject.tag=="Player")
            {
                isAlreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
            
        }
    }
}
