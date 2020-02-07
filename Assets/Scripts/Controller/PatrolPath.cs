using System;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float radius = 0.2f;

        private void OnDrawGizmos()
        {
            for(int i = 0; i<transform.childCount; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(transform.GetChild(i).position, radius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(GetNextIndex(i)));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i+1;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
