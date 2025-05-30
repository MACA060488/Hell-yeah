using UnityEngine;
using TMPro;

public class ShopTrigger : MonoBehaviour
{
    [Header("Объекты")]
    public GameObject shopPanel;
    public GameObject pressEHintObject;
    public TMP_Text pressEHintText;

    [Header("Настройки")]
    public KeyCode interactionKey = KeyCode.E;
    [TextArea]
    public string pressEMessage = "Нажмите E, чтобы открыть магазин";

    private bool playerInZone = false;
    private bool shopOpen = false;

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(interactionKey))
        {
            ToggleShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вошёл в зону магазина");
            playerInZone = true;

            if (!shopOpen && pressEHintObject != null)
            {
                pressEHintObject.SetActive(true);
                if (pressEHintText != null)
                {
                    pressEHintText.text = pressEMessage;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вышел из зоны магазина");
            playerInZone = false;
            CloseShop();

            if (pressEHintObject != null)
                pressEHintObject.SetActive(false);
        }
    }

    private void ToggleShop()
    {
        shopOpen = !shopOpen;

        if (shopPanel != null)
            shopPanel.SetActive(shopOpen);

        if (pressEHintObject != null)
            pressEHintObject.SetActive(!shopOpen);
    }

    private void CloseShop()
    {
        Debug.Log("Закрытие магазина (CloseShop вызван)");
        shopOpen = false;

        if (shopPanel != null)
            shopPanel.SetActive(false);
    }
}