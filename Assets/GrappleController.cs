using System;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    [Serializable]
    public struct UpDown
    {
        public float Up, Down;
    }

    enum LoweringState
    {
        Stationary, GoingDown, GoingUp
    }

    [SerializeField] float _MovementSpeed = 5.0f;
    [SerializeField] float _MovementSpeedMultiplier = 2.0f;
    [SerializeField] float _LoweringSpeed = 2.0f;
    [SerializeField] UpDown _LoweringUpDown = new UpDown(){ Up = 0.5f, Down = 9.0f };

    Rigidbody _ThisRB;
    SpringJoint _ThisJoint;

    LoweringState _LoweringState = LoweringState.Stationary;
    LoweringState _LastLoweringState = LoweringState.GoingUp;

    void Start()
    {
        _ThisRB = GetComponent<Rigidbody>();
        _ThisJoint = GetComponent<SpringJoint>();
    }

    void Update()
    {
        if (!GameManager.IsPlaying())
        {
            return;
        }

        TryMovement();
        TryLowering();

        if (_LoweringState != LoweringState.Stationary)
        {
            var grapplerSpeed = _LoweringSpeed * Time.deltaTime;
            var newAnchor = _ThisJoint.connectedAnchor;
            if (_LoweringState == LoweringState.GoingDown)
            {
                newAnchor.y += grapplerSpeed;
                if (newAnchor.y > _LoweringUpDown.Down)
                {
                    newAnchor.y = _LoweringUpDown.Down;
                    _LoweringState = LoweringState.Stationary;
                }
            }
            else if (_LoweringState == LoweringState.GoingUp)
            {
                newAnchor.y -= grapplerSpeed;
                if (newAnchor.y < _LoweringUpDown.Up)
                {
                    newAnchor.y = _LoweringUpDown.Up;
                    _LoweringState = LoweringState.Stationary;
                }
            }
            _ThisJoint.connectedAnchor = newAnchor;
        }
    }

    void TryMovement()
    {
        var actualSpeed = _MovementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            actualSpeed *= _MovementSpeedMultiplier;
        }
        var newPosition = _ThisRB.position;

        if (Input.GetKey(KeyCode.W))    newPosition.z += actualSpeed;
        if (Input.GetKey(KeyCode.S))    newPosition.z -= actualSpeed;
        if (Input.GetKey(KeyCode.A))    newPosition.x -= actualSpeed;
        if (Input.GetKey(KeyCode.D))    newPosition.x += actualSpeed;

        _ThisRB.position = newPosition;
    }

    void TryLowering()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_LoweringState == LoweringState.GoingDown)
            {
                _LoweringState = LoweringState.GoingUp;
                _LastLoweringState = LoweringState.GoingUp;
            }
            else if (_LoweringState == LoweringState.GoingUp)
            {
                _LoweringState = LoweringState.GoingDown;
                _LastLoweringState = LoweringState.GoingDown;
            }

            if (_LoweringState == LoweringState.Stationary)
            {
                if (_LastLoweringState == LoweringState.GoingDown)
                {
                    _LoweringState = LoweringState.GoingUp;
                    _LastLoweringState = LoweringState.GoingUp;
                }
                else if (_LastLoweringState == LoweringState.GoingUp)
                {
                    _LoweringState = LoweringState.GoingDown;
                    _LastLoweringState = LoweringState.GoingDown;
                }
            }
        }
    }
}
