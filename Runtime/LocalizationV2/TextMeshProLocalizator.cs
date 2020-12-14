using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProLocalizator : Translator
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        LocalizationManager.OnLanguageChanged += UpdateValue;
    }

    public override void SetText(string text)
    {
        _text.text = text;
    }
}
