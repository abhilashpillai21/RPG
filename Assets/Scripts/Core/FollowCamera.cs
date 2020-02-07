using UnityEngine;

namespace RPG.Core
{
    [DisallowMultipleComponent]
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
