using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Subscribe(UnityAction unityAction)
        {
            _button.onClick.AddListener(unityAction);
        }

        public void Unsubscribe(UnityAction unityAction)
        {
            _button.onClick.AddListener(unityAction);
        }
    }
}