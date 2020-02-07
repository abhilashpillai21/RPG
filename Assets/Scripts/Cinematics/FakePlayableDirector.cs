using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the Subject

namespace RPG.Cinematics
{
    public class FakePlayableDirector : MonoBehaviour
    {
        public event Action<float> onComplete;

        private void Start()
        {
            Invoke("OnFinish", 3f);
        }

        void OnFinish()
        {
            onComplete(5f);
        }
    }
}