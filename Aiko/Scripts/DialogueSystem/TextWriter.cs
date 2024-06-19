using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public event Action OnEmptyQueue, OnPlayAudio, OnStopAudio;
    private Queue<TextWriterSingle> textWriterQueue;
    private TextWriterSingle activeTextWriterSingle;

    private bool isFinished;

    private void Awake()
    {
        textWriterQueue = new Queue<TextWriterSingle>();
    }

    public void AddWriter(TextMeshProUGUI uiText, string message, float timePerCharacter, bool invisibleCharacters)
    {
        TextWriterSingle newTextWriterSingle = new TextWriterSingle(uiText, message, timePerCharacter, invisibleCharacters);
        textWriterQueue.Enqueue(newTextWriterSingle);

        if (activeTextWriterSingle == null)
        {
            OnPlayAudio?.Invoke();
            activeTextWriterSingle = textWriterQueue.Dequeue();
            isFinished = false;
            Debug.Log("Play Audio");
        }
    }

    private void Update()
    {
        if (activeTextWriterSingle != null && !isFinished)
        {
            isFinished = activeTextWriterSingle.Update();
            if (isFinished)
            {
                OnStopAudio?.Invoke();
                activeTextWriterSingle = null;
            }
        }
    }

    public void StartNextWriter()
    {
        if (textWriterQueue.Count > 0 && isFinished)
        {
            OnPlayAudio?.Invoke();
            activeTextWriterSingle = textWriterQueue.Dequeue();
            isFinished = false;
        }
        else if ((textWriterQueue.Count > 0 && !isFinished) || (textWriterQueue.Count == 0 && !isFinished))
        {
            FinishMessage();
        }
        else
        {
            // no more text messages -> hide menu
            OnEmptyQueue?.Invoke();
        }
    }

    private void FinishMessage()
    {
        if (activeTextWriterSingle != null) activeTextWriterSingle.InstantlyFinishMessage();
        activeTextWriterSingle = null;
        isFinished = true;
        OnStopAudio?.Invoke();
    }
}

public class TextWriterSingle
{
    private TextMeshProUGUI uiText;
    private string message;
    private float timePerCharacter;
    private float timer;
    private int characterIndex;
    private bool invisibleCharacters;

    public TextWriterSingle(TextMeshProUGUI uiText, string message, float timePerCharacter, bool invisibleCharacters)
    {
        this.uiText = uiText;
        this.message = message;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
    }

    public bool Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = message.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + message.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;

                if (characterIndex >= message.Length)
                {
                    uiText = null;
                    return true;
                }
            }
        }
        return false;
    }

    public void InstantlyFinishMessage()
    {
        if (uiText != null)
        {
            uiText.text = message;
            timer = 0f;
            uiText = null;
        }
    }
}
