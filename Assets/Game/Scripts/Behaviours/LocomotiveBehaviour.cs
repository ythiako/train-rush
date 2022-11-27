using UnityEngine;

namespace Behaviours
{
    public class LocomotiveBehaviour : TrainPartBehaviour
    {
        [SerializeField] private Transform meshRoot;
        [SerializeField] private Transform endPoint;

        private GameObject _mesh;
        
        public override Transform MeshRoot => meshRoot;

        public override Transform TipPoint => null;

        public override Transform EndPoint => endPoint;

        public void SetMesh(GameObject locomotiveMesh)
        {
            if (_mesh)
            {
                Destroy(_mesh);
            }

            _mesh = locomotiveMesh;
            _mesh.transform.parent = meshRoot;
            _mesh.transform.localPosition = Vector3.zero;
            _mesh.transform.localRotation = Quaternion.identity;
        }
    }
}
