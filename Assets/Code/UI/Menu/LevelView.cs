using Code.Services.StaticDataService.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameText;

        public void SetData(LevelConfig data)
        {
            _image.sprite = data.Logo;
            _nameText.text = data.Name;
        }
    }
}