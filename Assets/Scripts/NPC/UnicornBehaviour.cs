using System;
using UnityEngine;

namespace DefaultNamespace.NPC
{
    public class UnicornBehaviour : MonoBehaviour
    {
        public Vector2 topLeftLimit;
        public Vector2 bottomRightLimit;
        public Vector2 targetPosition;

        private void Start()
        {
            targetPosition = new Vector2(
                UnityEngine.Random.Range(topLeftLimit.x, bottomRightLimit.x),
                UnityEngine.Random.Range(bottomRightLimit.y, topLeftLimit.y)
            );
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = new Vector2(
                    UnityEngine.Random.Range(topLeftLimit.x, bottomRightLimit.x),
                    UnityEngine.Random.Range(bottomRightLimit.y, topLeftLimit.y)
                );
                
                if(targetPosition.x < transform.position.x)
                    GetComponent<SpriteRenderer>().flipX = false;
                else
                    GetComponent<SpriteRenderer>().flipX = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime);
        }
    }
}