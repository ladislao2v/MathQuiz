﻿using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Gameplay
{
    public class HalfEndView : Overlay
    {
        [SerializeField] private ScoreView _scoreView;
        [field: SerializeField] public Button NextHalf { get; private set; }

        public void TurnOn()
        {
            _scoreView.TurnOn();
        }

        public void TurnOff()
        {
            _scoreView.TurnOff();
        }
    }
}