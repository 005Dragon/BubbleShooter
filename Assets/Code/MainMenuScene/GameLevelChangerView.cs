using Code.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenuScene
{
    [RequireComponent(typeof(Button))]
    public class GameLevelChangerView : MonoBehaviour, IView
    {
        [SerializeField] private string _prefix;
        [SerializeField] private TextMeshProUGUI _text;
        
        public IModel Model { get; set; }
        
        private GameLevelChanger _model;
        
        public void Initialize()
        {
            _model = (GameLevelChanger)Model;
            _text.text = GetText();
            
            var button = GetComponent<Button>();
            
            button.onClick.AddListener(ChangeMode);
        }

        private void ChangeMode()
        {
            _model.LevelKey = _model.LevelKey == LevelKey.Random ? LevelKey.TestLevel : LevelKey.Random;
            _text.text = GetText();
        }

        private string GetText()
        {
            return _prefix + _model.LevelKey;
        }
    }
}