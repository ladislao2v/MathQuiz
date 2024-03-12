using System;
using UnityEngine;

namespace Code.Services.StaticDataService.Configs
{
    [Serializable]
    public class Question
    {
        [SerializeField] private VariantData _answer;
        [SerializeField] private VariantData[] _otherVariants;

        public VariantData Answer => _answer;
        public VariantData[] Other => _otherVariants;
    }
}