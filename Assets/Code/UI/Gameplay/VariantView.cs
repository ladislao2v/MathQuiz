using TMPro;
using UnityEngine;

namespace Code.UI.Gameplay
{
    public class VariantView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _taskText;

        public void SetData(string task)
        {
            _taskText.text = task;
        }
    }
}