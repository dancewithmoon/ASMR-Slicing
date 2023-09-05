using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Knife
{
    public class SliceLineDrawer : MonoBehaviour
    {
        [SerializeField] private float _width;
        [SerializeField] private float _density;
        [SerializeField] private LineRenderer _lineRendererPrefab;

        private Transform _lineRenderersParent;
        private float _maxPoint;
        private float _step;
        
        private readonly Dictionary<Collider, LineRenderer> _lineRenderers = new Dictionary<Collider, LineRenderer>();

        private void Awake()
        {
            _maxPoint = GetMaxPoint();
            _step = GetStep();
            _lineRenderersParent = new GameObject("Line Renderers").transform;
        }

        private void Update()
        {
            CleanUpNullLineRenderers();
            CleanUpLineLinePoints();
            DrawLine();
        }

        private void DrawLine()
        {
            for (float i = -_maxPoint; i <= _maxPoint; i += _step)
            {
                Vector3 raycastPosition = transform.position + Vector3.right * i;
                DrawPoint(raycastPosition);
            }
        }

        private void DrawPoint(Vector3 at)
        {
            if (Physics.Raycast(at, Vector3.down, out RaycastHit hit) == false) 
                return;
            
            if (_lineRenderers.TryGetValue(hit.collider, out LineRenderer lineRenderer) == false)
            {
                lineRenderer = CreateLineRenderer(hit.collider);
            }

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
        }

        private LineRenderer CreateLineRenderer(Collider forKey)
        {
            LineRenderer lineRenderer = Instantiate(_lineRendererPrefab, _lineRenderersParent);
            _lineRenderers[forKey] = lineRenderer;
            return lineRenderer;
        }
        
        private void CleanUpNullLineRenderers()
        {
            foreach (Collider key in _lineRenderers.Keys.ToArray())
            {
                if (key == null)
                {
                    Destroy(_lineRenderers[key].gameObject);
                    _lineRenderers.Remove(key);
                }
            }
        }

        private void CleanUpLineLinePoints()
        {
            foreach (KeyValuePair<Collider, LineRenderer> lineRenderer in _lineRenderers)
            {
                lineRenderer.Value.positionCount = 0;
            }
        }

        private float GetMaxPoint() => _width / 2;
        private float GetStep() => 1f / _density;
    }
}
