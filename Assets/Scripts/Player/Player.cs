using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput = new PlayerInput();

    private bool _toJump = false;
    private bool _toDoubleJump = false;
    private bool _isGrounded = true;

    [Header("Events")]
    public UnityEvent gameOver;
    public UpdateScore updateScore;
    

    private void Start() 
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        _toJump = _playerInput.ToJamp(_toJump);

        if(!_isGrounded)
        {
            _toDoubleJump = _playerInput.ToJamp(_toDoubleJump);
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.Jump(ref _isGrounded, _toJump);
        _playerMovement.DoubleJump(ref _isGrounded, _toDoubleJump);
        _playerMovement.Move();
    }

    private void OnCollisionEnter(Collision other) 
    {       
        _isGrounded = true;
        _toJump = false;
        _toDoubleJump = false;

        if(!other.transform.GetComponentInParent<Plane>())
        {
            gameOver.Invoke();
            updateScore.Invoke(_playerMovement.Speed);
           
            _playerMovement.enabled = false;
            enabled = false;
        }
    }
}

[System.Serializable]
public class UpdateScore : UnityEvent<float>{}
