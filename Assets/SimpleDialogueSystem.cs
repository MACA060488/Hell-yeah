using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SimpleDialogueSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject interactionHintObject;
    public TMP_Text interactionHintText;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public Button option1Button;
    public Button option2Button;

    public TMP_Text option1ButtonText;
    public TMP_Text option2ButtonText;

    [Header("Dialogue Texts (в нужном порядке)")]
    [TextArea] public string interactionHintString = "Нажмите E";
    [TextArea] public string initialText;
    public string option1Label = "Вариант 1";
    [TextArea] public string option1ResultText;
    public string option2Label = "Вариант 2";
    [TextArea] public string option2ResultText;

    [Header("Typewriter Settings")]
    [Tooltip("Скорость появления текста (секунд на символ)")]
    [Range(0.001f, 0.1f)]
    public float typeSpeed = 0.02f;

    [Header("Global Font Override")]
    [Tooltip("Если задан, будет автоматически применён ко всем текстам")]
    public TMP_FontAsset defaultFont;

    [Tooltip("Размер шрифта (если > 0 — применяется ко всем текстам)")]
    [Min(0)] public float defaultFontSize = 0f;

    [Header("Settings")]
    public KeyCode interactionKey = KeyCode.E;

    private bool isPlayerNear = false;
    private bool isDialogueActive = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        ApplyFontToAll();

        if (interactionHintText != null)
            interactionHintText.text = interactionHintString;

        if (option1ButtonText != null)
            option1ButtonText.text = option1Label;

        if (option2ButtonText != null)
            option2ButtonText.text = option2Label;

        option1Button.onClick.AddListener(() => ShowResponse(option1ResultText));
        option2Button.onClick.AddListener(() => ShowResponse(option2ResultText));

        interactionHintObject.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && !isDialogueActive && Input.GetKeyDown(interactionKey))
        {
            StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionHintObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionHintObject.SetActive(false);
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        interactionHintObject.SetActive(false);
        dialoguePanel.SetActive(true);

        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(initialText));
    }

    private void ShowResponse(string responseText)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(responseText));

        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);
    }

    private IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";
        option1Button.interactable = false;
        option2Button.interactable = false;

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        option1Button.interactable = true;
        option2Button.interactable = true;
    }

    public void ApplyFontToAll()
    {
        if (defaultFont != null)
        {
            if (interactionHintText != null)
                interactionHintText.font = defaultFont;
            if (dialogueText != null)
                dialogueText.font = defaultFont;
            if (option1ButtonText != null)
                option1ButtonText.font = defaultFont;
            if (option2ButtonText != null)
                option2ButtonText.font = defaultFont;
        }

        if (defaultFontSize > 0f)
        {
            if (interactionHintText != null)
                interactionHintText.fontSize = defaultFontSize;
            if (dialogueText != null)
                dialogueText.fontSize = defaultFontSize;
            if (option1ButtonText != null)
                option1ButtonText.fontSize = defaultFontSize;
            if (option2ButtonText != null)
                option2ButtonText.fontSize = defaultFontSize;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyFontToAll();
    }
#endif
}