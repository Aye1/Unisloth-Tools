using UnityEngine;
using UnityEngine.UI;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    [RequireComponent(typeof(Text))]
    public class TextLocalizator : Translator
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            LocalizationManager.OnLanguageChanged += UpdateValue;
        }

        public override void SetText(string text)
        {
            _text.text = text;
        }

    }
}
