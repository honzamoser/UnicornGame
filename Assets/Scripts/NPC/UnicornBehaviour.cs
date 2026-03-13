using DefaultNamespace;
using UnityEngine;

namespace NPC
{
    public class UnicornBehaviour : MonoBehaviour
    {
        public Vector2 topLeftLimit;
        public Vector2 bottomRightLimit;
        public Vector2 targetPosition;

        public Animator animator;
        public float speed = 1f;

        private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _chillCooldown;
        [SerializeField] private bool _moving;

        public bool isInPen = false;
        
        public int id;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            // start with a small random delay so multiple unicorns don't move in sync
            _chillCooldown = Random.Range(0f, 2f);
            // initialize target to current position
            targetPosition = transform.position;
            this.animator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            if (transform.position.y < topLeftLimit.y
                && transform.position.y > bottomRightLimit.y
                && transform.position.x > topLeftLimit.x
                && transform.position.x < bottomRightLimit.x)
            {
                isInPen = true;
            } else isInPen = false;

            if (_moving)
            {
                // Move towards target using 2D positions
                Vector2 currentPos = transform.position;
                Vector2 newPos = Vector2.MoveTowards(currentPos, targetPosition, speed * Time.deltaTime);
                transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

                // Flip sprite based on horizontal direction to target
                if (_spriteRenderer != null)
                    _spriteRenderer.flipX = (targetPosition.x > transform.position.x);

                // Check if we've arrived
                if (Vector2.Distance(currentPos, targetPosition) < 0.1f)
                {
                    _moving = false;
                    animator?.SetBool("IsWalking", false);
                    _chillCooldown = Random.Range(2f, 5f);
                }
            }
            else
            {
                // Idle: count down cooldown
                _chillCooldown -= Time.deltaTime;
                if (_chillCooldown <= 0f)
                {
                    StartMoving();
                }
            }
        }

        private void StartMoving()
        {
            // Compute safe min/max in case limits are inverted
            float minX = Mathf.Min(topLeftLimit.x, bottomRightLimit.x);
            float maxX = Mathf.Max(topLeftLimit.x, bottomRightLimit.x);
            float minY = Mathf.Min(bottomRightLimit.y, topLeftLimit.y);
            float maxY = Mathf.Max(bottomRightLimit.y, topLeftLimit.y);

            targetPosition = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );

            _moving = true;
            animator?.SetBool("IsWalking", true);

            if (_spriteRenderer != null)
                _spriteRenderer.flipX = (targetPosition.x > transform.position.x);
        }

        public void AfterRemove(int _id)
        {
            Debug.Log("inventory slot " + _id);
            GameManager.Instance.unicornPen.UnicornPickedUp(this);
            GameManager.Instance.unicornPen.UpdateText();
            GameManager.Instance.unicornPen.InventoryUnicornIds[_id] = this.id;
        }
    }
}