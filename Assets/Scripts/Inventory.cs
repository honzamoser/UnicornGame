using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static readonly int _capacity = 3;
    public InventoryItem[] items = new InventoryItem[_capacity];
    public Image[] itemSlots;
    public Image[] itemIcons;

    public bool isItemSelected = false;
    public int selectedItem = 0;

    void Start()
    {
        int i = 0;
        foreach (Image itemSlot in itemSlots)
        {
            Button b = itemSlot.gameObject.GetComponent<Button>();
            var i1 = i;
            b.onClick.AddListener(() => ClickSelectSlot(i1));
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int pressedKey = -1;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) pressedKey = 0;
        if (Keyboard.current.digit2Key.wasPressedThisFrame) pressedKey = 1;
        if (Keyboard.current.digit3Key.wasPressedThisFrame) pressedKey = 2;

        if (pressedKey != -1)
        {
            SelectSlot(pressedKey);
        }

        for (int i = 0; i < _capacity; i++)
        {
            InventoryItem item = items[i];
            if (item)
            {
                itemIcons[i].sprite = item.icon;
                itemIcons[i].gameObject.SetActive(true);
            }
            else
            {
                itemIcons[i].sprite = null;
                itemIcons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectSlot(int pressedKey)
    {
        if (!isItemSelected)
        {
            selectedItem = pressedKey;
            isItemSelected = true;
            HighlightSlot();
        }
        else
        {
            if (selectedItem == pressedKey)
            {
                isItemSelected = false;
                UnHighlightSlot();
            }

            if (selectedItem != pressedKey)
            {
                UnHighlightSlot();
                selectedItem = pressedKey;
                HighlightSlot();
            }
        }
    }

    public void ClickSelectSlot(int location)
    {
        SelectSlot(location);
    }

    public void HighlightSlot()
    {
        itemSlots[selectedItem].color = Color.green;
    }

    public void UnHighlightSlot()
    {
        itemSlots[selectedItem].color = Color.white;
    }
}