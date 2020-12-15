using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Unisloth.Localization
{
    [ExecuteAlways]
    public abstract class Translator : MonoBehaviour
    {
        [SerializeField, Required]
        [ValueDropdown("GetKeys"), OnValueChanged("UpdateValue")]
        protected string _key;

        public void UpdateValue()
        {
            SetText(LocalizationManager.Instance.GetTranslation(_key));
        }

        private void OnEnable()
        {
            UpdateValue();
        }

        private void OnDestroy()
        {
            LocalizationManager.OnLanguageChanged -= UpdateValue;
        }

        private static IEnumerable<string> GetKeys()
        {
            return LocalizationManager.Instance.AllKeys();
        }

        public abstract void SetText(string text);
    }
}
