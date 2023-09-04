using DG.Tweening;
using UnityEngine;

namespace Logic.Knife
{
    public class KnifeRemoving : MonoBehaviour
    {
        [SerializeField] private float _movementDuration;
        private Vector3 _finalPoint;

        public void Initialize(Vector3 finalPoint) => _finalPoint = finalPoint;

        public void Remove() => transform.DOMove(_finalPoint, _movementDuration);
    }
}