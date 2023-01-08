using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Rigidbody _playerRb;

    [SerializeField] private float trust;
    [SerializeField] private float torque;
    
    [SerializeField] private float maxPlayerVelocityX = 20;
    [SerializeField] private float maxPlayerVelocityY = 20;


    private float _horizontalInput;
    private float _verticalInput;

    private PlayerMovementController _movement;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _movement = new PlayerMovementController(maxPlayerVelocityX, maxPlayerVelocityY);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _horizontalInput = _movement.HorizontalInputController
            (joystick.Horizontal, _horizontalInput, _playerRb, trust);
        _verticalInput = _movement.VerticalInputController
            (joystick.Vertical, _verticalInput, _playerRb, trust);

        PlayerMovementController.StopOutOfBounds(transform, _playerRb);
        _movement.RotateOnMovement(transform, _horizontalInput, torque);
        _movement.LimitSpeedOfMovement(_playerRb);
    }
}