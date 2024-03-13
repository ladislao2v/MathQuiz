using System;
using System.Collections.Generic;
using System.Linq;
using Code.Services.SaveLoadDataService;
using ModestTree;
using UnityEngine;

namespace Code.Services.LevelSelectorService
{
    public class LevelSelector : ILevelSelector
    {
        public int SelectedLevel { get; private set; }
        public int LevelCount => _availableLevels.Count;
        
        private Dictionary<int, bool> _availableLevels = new()
        {
            [1] = true,
            [2] = false,
            [3] = false
        };

        public void Select(int levelIndex)
        {
            if(IsCorrect(levelIndex) == false)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));
            
            if(IsOpen(levelIndex) == false)
                return;

            SelectedLevel = levelIndex;
        }

        public bool IsOpen (int levelIndex)
        {
            if(IsCorrect(levelIndex) == false)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));

            return _availableLevels[levelIndex];
        }

        public void OpenNextLevelTo(int levelIndex)
        {
            if(levelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));

            if (levelIndex == _availableLevels.Count)
                return;

            _availableLevels[levelIndex + 1] = true;
        }

        private bool IsCorrect(int levelIndex)
        {
            var levelIndexes = _availableLevels.Keys;

            if (levelIndexes.Contains(levelIndex) == false)
                return false;

            return true;
        }

        public void LoadData(ISaveLoadDataService saveLoadDataService)
        {
            var loadable = saveLoadDataService
                .LoadByCustomKey<bool[]>(nameof(SelectedLevel));

            if (loadable == null)
                return;

            for (int i = 0; i < loadable.Length; i++)
            {
                _availableLevels[i + 1] = loadable[i];
            }
        }

        public void SaveData(ISaveLoadDataService saveLoadDataService)
        {
            var marks = _availableLevels.Values.ToArray();
            
            saveLoadDataService.SaveByCustomKey(marks, nameof(SelectedLevel));
        }
    }
}