using UnityEngine;
using TMPro;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProLocalizator : Translator
    {
        public override void SetText(string text)
        {
            TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
            textComponent.text = text;
        }
    }
}
