using System;
using Code.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenuScene.Views
{
    [RequireComponent(typeof(Button))]
    public class ChangeLevelView : MonoBehaviour
    {
        [SerializeField] private string _prefix;
        [SerializeField] private TextMeshProUGUI _text;

        public event EventHandler ChangingLevel;

        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(() => ChangingLevel?.Invoke(this, EventArgs.Empty));
        }

        public void SetLevel(LevelKey level)
        {
            _text.text = _prefix + level;
        }
    }
}