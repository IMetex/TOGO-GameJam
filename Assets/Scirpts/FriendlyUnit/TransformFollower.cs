using UnityEngine;

namespace Scirpts.Unit
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private Vector3 _offset;

        void LateUpdate()
        {
            var targetPosition = _target.position + _offset;
            transform.position = targetPosition; 
        }
    }
}
