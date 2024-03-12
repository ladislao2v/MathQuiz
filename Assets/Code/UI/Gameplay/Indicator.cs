using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _fire;

        private Color _rightColor = Color.green;
        private Color _wrongColor = Color.red;

        public void SetRight()
        {
            _image.color = _rightColor;
            _fire.SetActive(true);
        }

        public void SetWrong()
        {
            _image.color = _wrongColor;
        }
    }
}