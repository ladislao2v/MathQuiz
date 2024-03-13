﻿using System;
using Code.Services.SaveLoadDataService;

namespace Code.Services.ScoreService
{
    public interface IScoreService
    {
        event Action<int> PlayerScoreChanged;
        event Action<int> EnemyScoreChanged;

        void AddPlayerScore(int points);
        void AddEnemyScore(int points);
        void Reset();
    }
}