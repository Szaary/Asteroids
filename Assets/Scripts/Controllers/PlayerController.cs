using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public delegate void ShotAction();

    private Rigidbody _playerRb;

    [SerializeField] private float _trust;
    [SerializeField] private float _torque;

    private AudioSource _audioSource;
    public AudioClip _shot;

    private float _horizontalInput;
    private float _verticalInput;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        MovePlayer();             
        HUD.ShowStats();

        ShotProjectiles();
    }

    private void MovePlayer()
    {
        _horizontalInput = PlayerMovementController.HorizontalInputController(_horizontalInput, _playerRb, _trust);
        _verticalInput = PlayerMovementController.VerticalInputController(_verticalInput, _playerRb, _trust);

        PlayerMovementController.StopOutOfBounds(transform, _playerRb);
        PlayerMovementController.RotateOnMovement(transform, _horizontalInput, _torque);
        PlayerMovementController.LimitSpeedOfMovement(_playerRb);
    }

    private void ShotProjectiles()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (GameManagerAsteroids.Instance.numberOfWeapons <= 9)
            {
                EventBroker.CallShotAction();              
                _audioSource.PlayOneShot(_shot);
                PlayerMovementController.BackOnShot(_playerRb, 5f);                
            }
            else
            {
                GameManagerAsteroids.Instance.numberOfWeapons /= 3;
                GameManagerAsteroids.Instance.numberOfUpgrades++;
            }
        }
    }

    


}
