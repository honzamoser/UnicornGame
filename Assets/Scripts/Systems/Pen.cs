using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class Pen : MonoBehaviour
    {
        public int animalCount = 2;
        public int maxAnimals = 5;

        private float _timer = 0;

        public List<GameObject> animals = new List<GameObject>();

        public Vector2 spawnLocation;
        public GameObject unicornPrefab;

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;

            while (animalCount > animals.Count)
            {
                animals.Add(Instantiate(unicornPrefab, spawnLocation, Quaternion.identity));
            }

            while (animalCount < animals.Count)
            {
                Destroy(animals[animals.Count - 1]);
                animals.RemoveAt(animals.Count - 1);
            }
        }
    }
}