using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1f,1500f)] private float _speed = 1000f;
    [SerializeField] [Range(4f,20f)] private float _jumpForce = 10f;

    private Rigidbody _rigidbody;

    private float _gravityValue = Physics.gravity.y;

    private Vector3 _baseGravity;
    private Vector3 _alternativeGravity;

    public float Speed { get{return _speed;} private set{ _speed = value;}}

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _baseGravity =  new Vector3(0, _gravityValue, 0);
        _alternativeGravity = new Vector3(0,Mathf.Abs(_gravityValue), 0);
    }

    public void Move()
    {      
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, (_speed * Time.fixedDeltaTime));
        _speed += Time.fixedDeltaTime;
    }

    public void Jump(ref bool isGrounded, bool toJump)
    {
        if(Physics.gravity.y < _alternativeGravity.y)
        {
            if(isGrounded & toJump)
            {         
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

                isGrounded = false;
            }
        }
        else if(isGrounded & toJump)
        {
            Physics.gravity = _baseGravity;
            isGrounded = false;
            
            _rigidbody.AddForce(Vector3.down * _jumpForce, ForceMode.Impulse);
        }      
    }

    public void DoubleJump(ref bool isGrounded, bool toJump)
    {
        if(Physics.gravity.y < _alternativeGravity.y)
        {
            if(!isGrounded & toJump)
            {       
                Physics.gravity = _alternativeGravity;

                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDisable() 
    {
        Physics.gravity = _baseGravity;
    }
}
