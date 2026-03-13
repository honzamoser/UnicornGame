using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Systems
{
    public class Pen : MonoBehaviour
    {
        public int animalCount = 2;
        public int maxAnimals = 5;

        public TextMeshProUGUI amountText;
        public TextMeshProUGUI capacityText;
        public TextMeshProUGUI deadText;

        public Button buyButton;
        public Button sellButton;

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

        private void Start()
        {
            buyButton.onClick.AddListener(BuyUnicorn);
            sellButton.onClick.AddListener(SellUnicorn);
            UpdateText();
        }

        private void BuyUnicorn() {
            Debug.Log(GameManager.Instance.Money);
            if (animalCount < maxAnimals && GameManager.Instance.Money >= 200) {
                animalCount += 1;
                GameManager.Instance.Money -= 200;
                UpdateText();
            }
        }

        private void SellUnicorn() {
            if (animalCount > 0) {
                animalCount -= 1;
                UpdateText();
            }
        }

        private void UpdateText() {
            amountText.text = $"Amount: {animalCount}";
            capacityText.text = $"Capacity: {maxAnimals}";
            deadText.text = "Dead: 0";
        }
    }
}