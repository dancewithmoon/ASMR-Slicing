using Deform;
using Logic.Slice;
using UnityEngine;

namespace Logic.Knife
{
    public class KnifeDeforming : MonoBehaviour
    {
        [SerializeField] private BendDeformer _bendDeformer;

        private BendDeformer _deformerClone;
        private bool _deformationStarted;

        public void StartDeformation(SliceDeformable deformable)
        {
            _deformerClone = Instantiate(_bendDeformer, deformable.transform, true);
            deformable.SetDeformer(_deformerClone);
            
            _deformationStarted = true;
        }

        public void StopDeformation() => _deformationStarted = false;

        private void Update()
        {
            if(_deformationStarted == false)
                return;

            if (_bendDeformer.transform.position.y < _deformerClone.transform.position.y)
            {
                _deformerClone.transform.position = _bendDeformer.transform.position;
            }
        }
    }
}
