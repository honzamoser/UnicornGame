using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _movement;
    private InputSystem_Actions _actions;

    private void OnEnable()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    void Update()
    {
        _movement = _actions.Player.Move.ReadValue<Vector2>();

        if (_animator != null)
        {
            if (_movement.magnitude > 0.01f)
            {
                // Nastavíme směr, kterým se díváme
                _animator.SetFloat("Horizontal", _movement.x);
                _animator.SetFloat("Vertical", _movement.y);

                // Říkáme, že se hýbeme
                _animator.SetBool("isWalking", true);
            }
            else
            {
                // Říkáme, že stojíme
                _animator.SetBool("isWalking", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (_rb == null) return;
        Vector2 mv = _movement;
        Debug.Log(mv);
        if (mv.sqrMagnitude > 1f) mv.Normalize();
        _rb.linearVelocity = mv * speed;
    }
}