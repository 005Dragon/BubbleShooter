using System.Collections.Generic;
using UnityEngine;

namespace Code.GameScene.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(LevelStorage), fileName = nameof(LevelStorage))]
    public class LevelStorage : ScriptableObject
    {
        [SerializeField] private List<LevelData> _levels;

        public IReadOnlyList<LevelData> Levels => _levels;
    }
}