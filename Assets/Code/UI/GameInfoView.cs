using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class GameInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Construct(int number, string levelName, int playerScore, int enemyScore)
        {
            _text.text = $"{number}. {levelName} - {playerScore}:{enemyScore}";
        }
    }
}