using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Rigidbody _playerRb;

    [SerializeField] private float _trust;
    [SerializeField] private float _torque;


    private float _horizontalInput;
    private float _verticalInput;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _horizontalInput = PlayerMovementController.HorizontalInputController
            (joystick.Horizontal, _horizontalInput, _playerRb, _trust);
        _verticalInput = PlayerMovementController.VerticalInputController
            (joystick.Vertical, _verticalInput, _playerRb, _trust);

        PlayerMovementController.StopOutOfBounds(transform, _playerRb);
        PlayerMovementController.RotateOnMovement(transform, _horizontalInput, _torque);
        PlayerMovementController.LimitSpeedOfMovement(_playerRb);
    }
}