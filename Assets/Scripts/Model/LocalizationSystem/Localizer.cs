using System.Collections.Generic;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [CreateAssetMenu(fileName = "Localizer", menuName = "Dungeons/Infrastructure/LocalizationSystem/Localizer")]
    public class Localizer : ScriptableObject
    {
        [SerializeField] private int _currentLocalization;
        [SerializeField] private List<Localization> _localizations;
    }
}
