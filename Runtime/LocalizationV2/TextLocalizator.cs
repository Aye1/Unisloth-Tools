using UnityEngine;
using UnityEngine.UI;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    [RequireComponent(typeof(Text))]
    public class TextLocalizator : Translator
    {
        public override void SetText(string text)
        {
            Text textComponent = GetComponent<Text>();
            textComponent.text = text;
        }
    }
}
