using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

//This is the Observer
namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
        }

        private void EnableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void DisableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
