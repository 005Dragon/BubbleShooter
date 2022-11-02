using Code.Common.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Common.Views
{
    [RequireComponent(typeof(Button))]
    public class MainMenuNavigationPointView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private Button _button;
        
        public void Initialize()
        {
            _button = GetComponent<Button>();
            
            var model = (MainMenuNavigationPoint)Model;
            _button.onClick.AddListener(model.Active);
        }
    }
}