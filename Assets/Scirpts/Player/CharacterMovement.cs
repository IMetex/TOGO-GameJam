using Scirpts.Animation;
using UnityEngine;

namespace Scirpts.Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Joystick Referance")] [SerializeField]
        private DynamicJoystick _dynamicJoystick;

        [Header("Player Transform Referance")] [SerializeField]
        private Transform _playerChildTransfrom;
    
        [Header("Player Speed Value")] [SerializeField]
        private float _moveSpeed;

        private Rigidbody _playerRigidbody;
        private AnimationController _animationController;
    
        private float _horizontal;
        private float _vertical;

        private void Awake()
        {
            _animationController = GetComponent<AnimationController>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetMovementInput();
        }

        private void FixedUpdate()
        {
            SetMovement();
            SetRotation();
        }

        private void SetMovement()
        {
            _playerRigidbody.velocity = GetNewVelocity();
            // Player Animation 
            _animationController.SetBoolean("IsWalking", _horizontal != 0 || _vertical != 0);
        }

        private void SetRotation()
        {
            if (_horizontal != 0 || _vertical != 0)
            {
                _playerChildTransfrom.rotation = Quaternion.LookRotation(GetNewVelocity());
            }
        }

        private Vector3 GetNewVelocity()
        {
            return new Vector3(_horizontal, 0f, _vertical) * (_moveSpeed * Time.fixedDeltaTime);
        }

        private void GetMovementInput()
        {
            _horizontal = _dynamicJoystick.Horizontal;
            _vertical = _dynamicJoystick.Vertical;
        }
    }
}