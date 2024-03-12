using Code.Services.StaticDataService.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class VariantView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetData(VariantData data)
        {
            _image.sprite = data.Logo;
        }
    }
}