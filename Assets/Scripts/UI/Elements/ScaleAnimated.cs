using DG.Tweening;
using UnityEngine;

namespace UI.Elements
{
    public class ScaleAnimated : MonoBehaviour
    {
        [SerializeField] private float _maxScale;
        [SerializeField] private float _duration;

        private void Awake()
        {
            transform.DOScale(_maxScale, _duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}