using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject messageGameObject;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private ButtonMessageUI uiButtonMessage;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        uiButtonMessage.OnButtonMessageClicked += UiButtonMessage_OnButtonMessageClicked;
        textWriter.OnEmptyQueue += TextWriter_OnEmptyQueue;
        textWriter.OnPlayAudio += TextWriter_OnPlayAudio;
        textWriter.OnStopAudio += TextWriter_OnStopAudio;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddText("Hey that's me, long message needed cause i wanna test something, pls work", 0.05f, true);
            AddText("second message from me, long message, quickly something, something, blah, blah, ez", 0.05f, true);
            AddText("third message from me, short message", 0.05f, true);
            AddText("fourth message from me, everything works ok", 0.05f, true);
        }
    }

    private void TextWriter_OnStopAudio()
    {
        if (audioSource != null) audioSource.Stop();
    }

    private void TextWriter_OnPlayAudio()
    {
        if (audioSource != null) audioSource.Play();
    }

    private void TextWriter_OnEmptyQueue()
    {
        Toggle(false);
    }

    private void UiButtonMessage_OnButtonMessageClicked(object sender, System.EventArgs e)
    {
        textWriter.StartNextWriter();
    }

    public void AddText(string message)
    {
        float defaultTimerPerCharacter = 0.05f;
        bool invisibleCharacters = true;
        AddText(message, defaultTimerPerCharacter, invisibleCharacters);
    }

    public void AddText(string message, float timePerCharacter, bool invisibleCharacters)
    {
        Toggle(true);
        textWriter.AddWriter(this.uiText, message, timePerCharacter, invisibleCharacters);
    }

    private void Toggle(bool value)
    {
        messageGameObject.SetActive(value);
    }
}
