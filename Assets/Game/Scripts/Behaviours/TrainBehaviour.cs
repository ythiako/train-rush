
using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Game.Scripts.Controllers;
using Game.Scripts.Enumerations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Behaviours
{
    public class TrainBehaviour : MonoBehaviour
    {
        [TabGroup("Properties"), SerializeField] private TrainType type;
        [TabGroup("Properties"), SerializeField] private TrainColor color;
        [TabGroup("Properties"), SerializeField] private int initialWagons;
        
        [TabGroup("Movement"), SerializeField] private float speed;
        [TabGroup("Movement"), SerializeField] private float acceleration;
        [TabGroup("Movement"), SerializeField] private float deceleration;
        [TabGroup("Movement"), SerializeField] private SplineComputer spline;

        private float _currentSpeed;
        private float _desiredSpeed;
        private float _currentAcceleration;
        
        private readonly List<TrainPartBehaviour> _trainParts = new List<TrainPartBehaviour>();

        private void Awake()
        {
            FactoryController.TrainFactory.CreateTrain(_trainParts, type, color, initialWagons);
            
            SetSpline(spline);
        }

        private IEnumerator Start()
        {
            yield return null;
            yield return null;
            
            SetupTrain();
        }

        private void Update()
        {
            UpdateSpeed();
            SetTrainSpeed();
        }

        public void SetSpline(SplineComputer splineComputer)
        {
            spline = splineComputer;
            
            foreach (var trainPart in _trainParts)
            {
                trainPart.SetSpline(spline);
            }
        }

        [Button(ButtonSizes.Large)]
        public void Startup()
        {
            _desiredSpeed = speed;
            _currentAcceleration = acceleration;
        }

        [Button(ButtonSizes.Large)]
        public void Stop()
        {
            _desiredSpeed = 0;
            _currentAcceleration = deceleration;
        }

        [Button(ButtonSizes.Large)]
        private void SetupTrain()
        {
            var distanceSoFar = 0f;
            
            for (int i = _trainParts.Count - 1; i >= 0; i--)
            {
                var trainPart = _trainParts[i];
                distanceSoFar += trainPart.CalculateEndPointDistance();
                trainPart.SetDistance(distanceSoFar);
                distanceSoFar += trainPart.CalculateTipPointDistance();
            }
        }

        private void UpdateSpeed()
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, _desiredSpeed, _currentAcceleration * Time.deltaTime);
        }

        private void SetTrainSpeed()
        {
            foreach (var trainPart in _trainParts)
            {
                trainPart.Speed = _currentSpeed;
            }
        }
    }
}