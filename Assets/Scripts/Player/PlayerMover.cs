using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _jumpForce = 400;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ContactFilter2D _filter;

    private Rigidbody2D _rigidbody;
    private bool _isRotated;
    private readonly RaycastHit2D[] _results = new RaycastHit2D[1];

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetBall()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _rigidbody.angularVelocity = 0;
    }

    public void Move(float deltaX)
    {
        if (deltaX != 0)
        {
            _rigidbody.AddTorque(-deltaX * _rotationSpeed, ForceMode2D.Force);
            _isRotated = true;
        }
    }

    public void Jump()
    {
        if (isOnGround())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        }
    }

    private bool isOnGround()
    {
        int collisionCount = _rigidbody.Cast(-Vector2.up, _filter, _results, 0.1f);

        return collisionCount != 0;
    }

    private void LateUpdate()
    {
        InertiaDeceleration();
    }

    private void InertiaDeceleration()
    {
        if (!_isRotated)
        {
            _rigidbody.angularVelocity = Mathf.Lerp(_rigidbody.angularVelocity, 0, Time.deltaTime);
        }
        _isRotated = false;
    }
}
