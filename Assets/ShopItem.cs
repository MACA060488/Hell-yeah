using System;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string itemId;
    public string itemName;
    public int price;
    public bool isPurchased = false;

    public Sprite itemIcon;
    public string description;
    public Color backgroundColor;
}