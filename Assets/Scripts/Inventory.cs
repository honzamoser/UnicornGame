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

    public GameObject currentInventoryItem;

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
        int previousSelectedItem = selectedItem;
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

        if (previousSelectedItem != selectedItem)
        {
            Destroy(currentInventoryItem);
        }
        
        if (isItemSelected && items[selectedItem] != null && currentInventoryItem == null)
        {
            

            currentInventoryItem = Instantiate(items[selectedItem].inventoryPrefab);
        }

        if (!isItemSelected && currentInventoryItem != null)
        {
            Destroy(currentInventoryItem);
            currentInventoryItem = null;
        }

        if (currentInventoryItem != null)
        {
            currentInventoryItem.transform.position = GameManager.Instance.playerHandOffset;
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

    public void Add(InventoryItem item)
    {
        for (int i = 0; i < _capacity; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return;
            }
        }
    }

    public bool HasCapacity()
    {
        for (int i = 0; i < _capacity; i++)
        {
            if (items[i] == null)
            {
                return true;
            }
        }

        return false;
    }

    public void RemoveCurrentItem()
    {
        items[selectedItem] = null;
        isItemSelected = false;
        UnHighlightSlot();
    }
}