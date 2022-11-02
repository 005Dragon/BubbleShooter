using Code.Common.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Common.Views
{
    [RequireComponent(typeof(Button))]
    public class StartGameNavigationPointView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private Button _button;
        
        public void Initialize()
        {
            _button = GetComponent<Button>();
            
            var model = (StartGameNavigationPoint)Model;
            _button.onClick.AddListener(model.Active);
        }
    }
}