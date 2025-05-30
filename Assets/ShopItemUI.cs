using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI Элементы")]
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Button buyButton;

    private ShopItem itemData;
    private ShopManager shopManager;

    public void Setup(ShopItem item, ShopManager manager)
    {
        itemData = item;
        shopManager = manager;

        // Название и цена
        itemNameText.text = item.itemName;
        priceText.text = $"{item.price} монет";

        // Покупка
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyThisItem);

        // Отключить кнопку, если товар уже куплен
        buyButton.interactable = !item.isPurchased;
    }

    private void BuyThisItem()
    {
        shopManager.PurchaseItem(itemData);
    }
}