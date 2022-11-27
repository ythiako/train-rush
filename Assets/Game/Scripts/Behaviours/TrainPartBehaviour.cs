using Dreamteck.Splines;
using UnityEngine;

namespace Behaviours
{
    public abstract class TrainPartBehaviour : MonoBehaviour
    {
        [SerializeField] private SplineFollower follower;

        public float Speed
        {
            get => follower.followSpeed;
            set => follower.followSpeed = value;
        }
        
        public abstract Transform MeshRoot { get; }
        
        public abstract Transform TipPoint { get; }
        
        public abstract Transform EndPoint { get; }

        public void SetSpline(SplineComputer spline)
        {
            follower.spline = spline;
        }

        public void SetDistance(float distance)
        {
            follower.SetDistance(distance);
        }

        public float CalculateTipPointDistance()
        {
            if (!TipPoint) return 0;
            
            var distance = TipPoint.position - transform.position;
            distance.y = 0;
            return distance.magnitude;
        }

        public float CalculateEndPointDistance()
        {
            if (!EndPoint) return 0;
            
            var distance = EndPoint.position - transform.position;
            distance.y = 0;
            return distance.magnitude;
        }
    }
}