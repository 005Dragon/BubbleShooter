using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.Storage;

namespace Code.Services
{
    public class LevelService
    {
        private readonly Dictionary<LevelKey, LevelData> _levelKeyToLevelDataIndex;

        public LevelService(LevelStorage levelStorage)
        {
            _levelKeyToLevelDataIndex = levelStorage.Levels.ToDictionary(x => x.LevelKey, x => x);
        }

        public Queue<BubbleType[]> GetLevelQueue(LevelKey levelKey)
        {
            IReadOnlyList<LevelRowData> levelRows = _levelKeyToLevelDataIndex[levelKey].LevelRows;

            int maxLength = levelRows.Max(x => x.BubbleTypes.Count);

            var result = new Queue<BubbleType[]>();
            
            foreach (LevelRowData levelRowData in levelRows)
            {
                result.Enqueue(GetNormalizedRow(levelRowData, maxLength));
            }

            return result;
        }

        public Queue<BubbleType[]> GetRandomLevelQueue(int width, int height)
        {
            var result = new Queue<BubbleType[]>();

            for (int y = 0; y < height; y++)
            {
                var row = new BubbleType[width];
                
                for (int x = 0; x < width; x++)
                {
                    row[x] = BubbleService.GetRandomType();
                }
                
                result.Enqueue(row);
            }

            return result;
        }

        private BubbleType[] GetNormalizedRow(LevelRowData levelRowData, int maxLength)
        {
            int difference = maxLength - levelRowData.BubbleTypes.Count;
            
            if (difference == 0)
            {
                return levelRowData.BubbleTypes.ToArray();
            }

            var row = new BubbleType[maxLength];
            int rowIndex = 0;

            if (difference % 2 != 0)
            {
                row[rowIndex++] = BubbleType.None;
            }

            for (int i = 0; i < difference / 2; i++)
            {
                row[rowIndex++] = BubbleType.None;
            }

            foreach (var bubbleType in levelRowData.BubbleTypes)
            {
                row[rowIndex++] = bubbleType;
            }

            return row;
        }
    }
}