using System;
using System.Collections.Generic;
using NPC;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Systems
{
    public struct UnicornData
    {
        public int id;
        public float lastMilkingTime;
        public bool hasBody;
    }

    public class Pen : MonoBehaviour
    {
        public int maxAnimals = 5;
        public int lastId = 0;

        public TextMeshProUGUI amountText;
        public TextMeshProUGUI capacityText;
        public TextMeshProUGUI deadText;

        public Button buyButton;
        public Button sellButton;

        private float _timer = 0;

        public Dictionary<int, GameObject> unicornBodies = new Dictionary<int, GameObject>();
        public Dictionary<int, UnicornData> unicorns = new Dictionary<int, UnicornData>();
        public int[] InventoryUnicornIds = new int[3] { -1, -1, -1 };

        public Vector2 spawnLocation;
        public GameObject unicornPrefab;

        public GameObject PenUI;

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
            if (unicorns.Count < maxAnimals && GameManager.Instance.Money >= 200)
            {
                unicorns.Add(lastId, new UnicornData { id = lastId, lastMilkingTime = Time.time, hasBody = true });
                var unicornBody = Instantiate(unicornPrefab, spawnLocation, Quaternion.identity);
                unicornBody.GetComponent<UnicornBehaviour>().id = lastId;
                unicornBodies.Add(lastId, unicornBody);


                lastId += 1;
                GameManager.Instance.Money -= 200;
                UpdateText();
            }
        }

        public void Interact()
        {
            PenUI.SetActive(!PenUI.activeSelf);
        }

        private void SellUnicorn()
        {
            if (unicorns.Count > 0)
            {
                GameManager.Instance.Money += 100;
                unicorns.Remove(unicorns[lastId].id);
                unicornBodies.Remove(unicorns[lastId].id);
                UpdateText();
            }
        }

        public void UpdateText()
        {
            amountText.text = $"Amount: {unicorns.Count}";
            capacityText.text = $"Capacity: {maxAnimals}";
            deadText.text = "Dead: 0";
        }

        public void SpawnUnicornFromInventory(Vector2 position, int slotId)
        {
            var body = Instantiate(unicornPrefab, position, Quaternion.identity);
            int unicornId = InventoryUnicornIds[slotId];
            body.GetComponent<UnicornBehaviour>().id = unicornId;
            unicornBodies.Add(unicornId, body);
            InventoryUnicornIds[slotId] = -1;
            UpdateText();
        }

        public void UnicornPickedUp(UnicornBehaviour unicornBehaviour)
        {
            unicornBodies.Remove(unicornBehaviour.id);
            unicorns[unicornBehaviour.id] = new UnicornData
            {
                id = unicornBehaviour.id, lastMilkingTime = unicorns[unicornBehaviour.id].lastMilkingTime,
                hasBody = false
            };
            UpdateText();
        }
    }
}