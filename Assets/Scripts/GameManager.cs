using System;
using DefaultNamespace.Systems;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public GameManager Instance;

        private void Awake()
        {
            this.Instance = this;
        }

        public Pen unicornPen;
        
        
    }
}