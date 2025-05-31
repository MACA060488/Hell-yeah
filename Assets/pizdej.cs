using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [Header("Клавиша взаимодействия")]
    public KeyCode interactionKey = KeyCode.E;

    [Header("Подсказка")]
    public GameObject pressHintObject;
    public TMP_Text pressHintText;
    public string pressHintMessage = "Нажмите E";

    [Header("Панель диалога")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    [Header("Настройки шрифта")]
    public TMP_FontAsset globalFont;
    public int fontSize = 24;

    [Header("Настройки текста")]
    [TextArea] public string initialText;
    public float textSpeed = 0.02f;

    [Header("Кнопка 1")]
    public Button option1Button;
    public TMP_Text option1Label;
    [TextArea] public string option1Response;

    [Header("Кнопка 2")]
    public Button option2Button;
    public TMP_Text option2Label;
    [TextArea] public string option2Response;

    private bool playerInRange;
    private bool dialogueActive;

    void Start()
    {
        pressHintObject.SetActive(false);
        dialoguePanel.SetActive(false);

        ApplyFontSettings();
    }

    void Update()
    {
        if (playerInRange && !dialogueActive && Input.GetKeyDown(interactionKey))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        pressHintObject.SetActive(false);
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeText(initialText));

        option1Button.gameObject.SetActive(!string.IsNullOrEmpty(option1Label.text));
        option2Button.gameObject.SetActive(!string.IsNullOrEmpty(option2Label.text));
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void OnOption1Selected()
    {
        StartCoroutine(TypeText(option1Response));
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    public void OnOption2Selected()
    {
        StartCoroutine(TypeText(option2Response));
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    public void ApplyFontSettings()
    {
        if (globalFont == null) return;

        dialogueText.font = globalFont;
        dialogueText.fontSize = fontSize;
        option1Label.font = globalFont;
        option1Label.fontSize = fontSize;
        option2Label.font = globalFont;
        option2Label.fontSize = fontSize;
        pressHintText.font = globalFont;
        pressHintText.fontSize = fontSize;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!dialogueActive)
            {
                pressHintText.text = pressHintMessage;
                pressHintObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            pressHintObject.SetActive(false);
            dialoguePanel.SetActive(false);
            dialogueActive = false;
        }
    }
}
