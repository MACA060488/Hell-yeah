using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject shopPanel;
    public List<ShopItem> shopItems; // Добавляешь товары в инспекторе
    public GameObject shopItemUIPrefab;
    public Transform itemContainer;
    public TMP_Text balanceText;

    public int playerBalance = 1000; // Начальный баланс

    private void Start()
    {
        shopPanel.SetActive(false);
        LoadData();
        PopulateShop();
        UpdateBalanceUI();
    }

    void PopulateShop()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject); // очищаем
        }

        foreach (ShopItem item in shopItems)
        {
            GameObject obj = Instantiate(shopItemUIPrefab, itemContainer);
            ShopItemUI ui = obj.GetComponent<ShopItemUI>();
            ui.Setup(item, this);
        }
    }

    public void PurchaseItem(ShopItem item)
    {
        if (item.isPurchased || playerBalance < item.price)
            return;

        playerBalance -= item.price;
        item.isPurchased = true;
        SaveData();
        UpdateBalanceUI();
        PopulateShop(); // Обновить UI
    }

    public void UpdateBalanceUI()
    {
        balanceText.text = $"Баланс: {playerBalance} монет";
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("PlayerBalance", playerBalance);
        foreach (var item in shopItems)
        {
            PlayerPrefs.SetInt(item.itemId + "_purchased", item.isPurchased ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        playerBalance = PlayerPrefs.GetInt("PlayerBalance", 1000);
        foreach (var item in shopItems)
        {
            item.isPurchased = PlayerPrefs.GetInt(item.itemId + "_purchased", 0) == 1;
        }
    }
}