using Infrastructure.Factory;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class GameHudScreen : BaseScreen
    {
        [SerializeField] private ProgressBar _progressBar;

        private float _startPosition;
        private float _finalPosition;
        private IGameFactory _gameFactory;
        
        public void Initialize(Vector3 sliceableStartPosition, Vector3 sliceableFinalPosition, IGameFactory gameFactory)
        {
            _startPosition = sliceableStartPosition.z;
            _finalPosition = sliceableFinalPosition.z - _startPosition;
            _gameFactory = gameFactory;
        }
        
        private void Update()
        {
            float currentPosition = _gameFactory.SliceableItem.transform.position.z - _startPosition;
            _progressBar.UpdateValue(currentPosition/_finalPosition);
        }
    }
}