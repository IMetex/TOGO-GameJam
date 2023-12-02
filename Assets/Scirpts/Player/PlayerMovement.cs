using Scirpts.Animation;
using UnityEngine;

namespace Scirpts.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Joystick Reference")] [SerializeField]
        private DynamicJoystick _dynamicJoystick;

        [Header("Player Transform Reference")] 
        [SerializeField] private Transform _playerChildTransform;

        [Header("Player Speed Value")] [SerializeField]
        private float _moveSpeed;

        private AnimationController _animationController;
        private CharacterController _characterController;

        private float _horizontal;
        private float _vertical;

        private void Awake()
        {
            _animationController = GetComponent<AnimationController>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            GetMovementInput();
            SetMovement();
            SetRotation();
        }

        private void SetMovement()
        {
            Vector3 movementVector = new Vector3(_horizontal, 0f, _vertical).normalized;
            
            _characterController.SimpleMove(movementVector * _moveSpeed);
            
            _animationController.SetBoolean("IsWalking", movementVector != Vector3.zero);
        }

        private void SetRotation()
        {
            Vector3 movementVector = new Vector3(_horizontal, 0f, _vertical).normalized;

            if (movementVector != Vector3.zero)
            {
                _playerChildTransform.rotation = Quaternion.LookRotation(movementVector);
            }
        }

        private void GetMovementInput()
        {
            _horizontal = _dynamicJoystick.Horizontal;
            _vertical = _dynamicJoystick.Vertical;
        }
    }
}