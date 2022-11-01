using System.Collections.Generic;
using Code.Common;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(LevelData), fileName = nameof(LevelData))]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private LevelKey _levelKey;
        [SerializeField] private List<LevelRowData> _levelRows;

        public LevelKey LevelKey => _levelKey;
        public IReadOnlyList<LevelRowData> LevelRows => _levelRows;
    }
}