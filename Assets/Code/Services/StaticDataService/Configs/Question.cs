using System;
using UnityEngine;

namespace Code.Services.StaticDataService.Configs
{
    [Serializable]
    public class Question
    {
        [SerializeField] private string _task;
        [SerializeField] private string _answer;
        [SerializeField] private string[] _otherVariants;

        public string Task => _task;
        public string Answer => _answer;
        public string[] Other => _otherVariants;
    }
}