using System;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Camera mainCamera;
    
    private Rigidbody _playerRb;

    [SerializeField] private float trust;
    [SerializeField] private float torque;
    
    [SerializeField] private float maxPlayerVelocityX = 20;
    [SerializeField] private float maxPlayerVelocityY = 20;
    [SerializeField] private float borderForce = 1;

    
    private float _horizontalInput;
    private float _verticalInput;
    
    private PlayerMovementController _movement;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _movement = new PlayerMovementController(maxPlayerVelocityX, maxPlayerVelocityY, borderForce);

        GameManager.GameStateChanged += OnGameStateChanged;
        UiCards.CardsEnabled += OnCardsEnabled;
    }

    private void OnCardsEnabled(bool obj)
    {
        _playerRb.velocity /= 3;
        _playerRb.angularVelocity /= 3;
    }


    private void OnGameStateChanged(GameState state)
    {
        if (state is GameState.Victory or GameState.Defeat)
        {
            _playerRb.velocity = Vector3.zero;
            _playerRb.angularVelocity = Vector3.zero;

            transform.DOMove(Vector3.zero, 1).SetUpdate(true).onComplete = () =>
            {
                _playerRb.velocity = Vector3.zero;
                _playerRb.angularVelocity = Vector3.zero;
                transform.rotation= Quaternion.identity;
            };
        }
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

        _movement.StopOutOfBounds(transform, _playerRb, mainCamera);
        _movement.RotateOnMovement(transform, _horizontalInput, torque);
        _movement.LimitSpeedOfMovement(_playerRb);
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
        UiCards.CardsEnabled -= OnCardsEnabled;
    }
}