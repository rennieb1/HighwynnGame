using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class CompanionFollow : MonoBehaviour
    {
        public PlatformerCharacter2D target;
        public Vector3 targetPosition;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        public bool followTarget = true;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Start is called before the first frame update
        void Start()
        {
            m_LastTargetPosition = target.transform.position;
            m_OffsetZ = (transform.position - target.transform.position).z;
            transform.parent = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (followTarget) {
                FollowTarget();
            }
            else {
                FollowMark();
            }
        }

        void FollowTarget() {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.transform.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.transform.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.transform.position;
        }

        void FollowMark() {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (targetPosition - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = targetPosition + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = targetPosition;
        }


    }
}
