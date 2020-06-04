﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Objects bindings")]
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image characterImage;
    [SerializeField] private Sprite defaultSprite;
#pragma warning restore 0649

    private string _currentText;
    private string _remainingText;
    private DialogLine[] _allDialogLines;
    private DialogLine _currentDialogLine;
    private int _textId;

    [Header("Control booleans")]
    public bool displayTextFinished;
    public bool allTextsDisplayed;


    public event System.EventHandler OnDialogClosing;

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    public void Init(DialogLine[] text)
    {
        textMesh.text = "";
        _allDialogLines = text;
        allTextsDisplayed = false;
        StartDisplayingText();
    }

    private void StartDisplayingText()
    {
        _textId = 0;
        DisplayLine();
    }

    private void DisplayNextLine()
    {
        _textId++;
        DisplayLine();
    }

    private void DisplayLine()
    {
        PrepareVariablesForNewLine();
        StartCoroutine(DisplayLineLetterByLetter());
    }

    private void PrepareVariablesForNewLine()
    {
        _currentText = "";
        _remainingText = _allDialogLines[_textId].text;
        _currentDialogLine = _allDialogLines[_textId];
        displayTextFinished = false;
    }

    private void DisplayNextLineOrClose()
    {
        if (_textId == _allDialogLines.Length - 1)
        {
            // All texts displayed, close DialogBox
            allTextsDisplayed = true;
            CloseDialog();
        }
        else
        {
            // There are other texts remaining
            DisplayNextLine();
        }
    }

    private IEnumerator DisplayLineLetterByLetter()
    {
        ChangePicture();
        float timeBetweenLetters = 0.05f;
        while (!displayTextFinished)
        {
            string firstLetter = _remainingText.Substring(0, 1);
            _currentText = _currentText + firstLetter;
            if (_currentText == _currentDialogLine.text)
            {
                displayTextFinished = true;
            }
            else
            {
                _remainingText = _remainingText.Substring(1);
            }
            textMesh.text = _currentText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    private void ChangePicture()
    {
        if (_currentDialogLine.character == null)
        {
            characterImage.sprite = defaultSprite;
        }
        else
        {
            characterImage.sprite = _currentDialogLine.character.picture;
        }
    }

    /// <summary>
    /// Forces the display of the whole line (for impatient players)
    /// </summary>
    private void FastEndCurrentText()
    {
        _currentText = _currentDialogLine.text;
        _remainingText = "";
        textMesh.text = _currentText;
        displayTextFinished = true;
    }

    private void ManageInput()
    {
        if (Input.anyKeyDown)
        {
            if (displayTextFinished)
            {
                DisplayNextLineOrClose();
            }
            else
            {
                FastEndCurrentText();
            }
        }
    }

    private void CloseDialog()
    {
        OnDialogClosing?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}