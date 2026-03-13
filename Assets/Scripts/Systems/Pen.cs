using System;
using System.Collections.Generic;
using NPC;
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
        }

        private void Start()
        {
            buyButton.onClick.AddListener(BuyUnicorn);
            sellButton.onClick.AddListener(SellUnicorn);
            UpdateText();
        }

        private void BuyUnicorn()
        {
            Debug.Log(GameManager.Instance.Money);
            if (animalCount < maxAnimals && GameManager.Instance.Money >= 200)
            {
                animalCount += 1;
                GameManager.Instance.Money -= 200;
                animals.Add(Instantiate(unicornPrefab, spawnLocation, Quaternion.identity));
                UpdateText();
            }
        }

        private void SellUnicorn()
        {
            if (animalCount > 0)
            {
                animalCount -= 1;
                UpdateText();
            }
        }

        public void UpdateText()
        {
            amountText.text = $"Amount: {animalCount}";
            capacityText.text = $"Capacity: {maxAnimals}";
            deadText.text = "Dead: 0";
        }

        public void SpawnUnicornFromInventory(Vector2 position)
        {
            animals.Add(Instantiate(unicornPrefab, position, Quaternion.identity));
            animalCount += 1;
            UpdateText();
        }

        public void UnicornPickedUp(UnicornBehaviour unicornBehaviour)
        {
            if (animals.Contains(unicornBehaviour.gameObject))
            {
                animals.Remove(unicornBehaviour.gameObject);
                animalCount -= 1;
                UpdateText();
            }
        }
    }
}