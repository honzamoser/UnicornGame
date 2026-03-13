using System;
using System.Data.SqlTypes;
using DefaultNamespace.Systems;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField]private int money;
        public int Money {
            set {
                money = value;
                moneyUI.text = value.ToString();
                PlayerPrefs.SetInt("money", value);
                PlayerPrefs.Save();
            }
            get { return money; }
        }


        private void Awake()
        {
            GameManager.Instance = this;
            if (PlayerPrefs.HasKey("money"))
            {
                Money = 2000;
            }
            else {
                Money = 2000;
            }

        }

        public TextMeshProUGUI moneyUI;

        public Pen unicornPen;

        public Inventory Inventory;
        public bool disableInteraction;

        public Vector2 playerHandOffset = Vector2.zero;
    }
}