using UnityEngine;

namespace Dungeons.Model.LocalizationSystemNEW
{
    [CreateAssetMenu(fileName = "Localization", menuName = "Dungeons/Model/LocalizationSystem/Localization")]
    public class Localization : ScriptableObject
    {
        [SerializeField] private string _languageDesignation;
        [SerializeField] private LocalizationTextID _testID;
        [SerializeField] private LocalizationData _dataList;

        public Localization(string languageDesiganation)
        {
            _languageDesignation = languageDesiganation;
            _dataList = new LocalizationData();
        }

        public string LanguageDesignation => _languageDesignation;
        public LocalizationData DataList => _dataList;
    }
}
