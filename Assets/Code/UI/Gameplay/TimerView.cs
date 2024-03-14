using TMPro;
using UnityEngine;

namespace Code.UI.Gameplay
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _half;
        [SerializeField] private TextMeshProUGUI _time;

        public void Construct(int half)
        {
            _half.text = $"{half}st half";
        }
        
        public void Render(int value)
        {
            _time.text = $"00:{value:00}";
        }
    }
}