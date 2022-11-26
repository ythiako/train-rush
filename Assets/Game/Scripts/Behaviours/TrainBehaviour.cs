
using System;
using Dreamteck.Splines;
using UnityEngine;

namespace Behaviours
{
    public class TrainBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        [SerializeField] private SplineFollower splineFollower;

        private float _currentSpeed;
        private float _desiredSpeed;
        private float _currentAcceleration;

        private void Update()
        {
            UpdateSpeed();
            
            splineFollower.followSpeed = _currentSpeed;
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                Startup();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Stop();
            }
        }

        public void Startup()
        {
            _desiredSpeed = speed;
            _currentAcceleration = acceleration;
        }

        public void Stop()
        {
            _desiredSpeed = 0;
            _currentAcceleration = deceleration;
        }

        private void UpdateSpeed()
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, _desiredSpeed, _currentAcceleration * Time.deltaTime);
        }
    }
}