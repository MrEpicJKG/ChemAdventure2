using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1; //damping applied to SmoothDamp function
        public float lookAheadFactor = 3; //mult applied to look ahead dist
        public float lookAheadReturnSpeed = 0.5f; //mult applied to MoveTowards function
        public float lookAheadMoveThreshold = 0.1f; //min target movement to trigger look ahead
        public Vector2 camPosMin; //upper left corner
        public Vector2 camPosMax; //lower right corner

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

			//print(newPos);
			newPos = RestrictCamMovement(newPos, camPosMin, camPosMax);
			transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
        private Vector3 RestrictCamMovement(Vector3 vectToRestrict, Vector2 camMin, Vector2 camMax)
        {
            float x = vectToRestrict.x;
            float y = vectToRestrict.y;

            x = Mathf.Clamp(x, camMin.x, camMax.x);
            y = Mathf.Clamp(y, camMin.y, camMax.y);

            return new Vector3(x, y, vectToRestrict.z);
        }
    }
}
