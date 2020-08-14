using UnityEngine;


namespace Helper
{
    public sealed class BulletProjector : MonoBehaviour
    {

        #region Fields

        [SerializeField] private float _distanceTolerance = 0.1f;
        private float _origNearClipPlane;
        private float _origFarClipPlane;
        private Projector _projector;

        #endregion

        
        #region UnityMethods

        private void Start()
        {
            _projector = GetComponent<Projector>();
            _origNearClipPlane = _projector.nearClipPlane;
            _origFarClipPlane = _projector.farClipPlane;
            Late();
        }

        #endregion


        #region Methods

        private void Late()
        {
            var ray = new Ray(_projector.transform.position + 
                              _projector.transform.forward.normalized * _origNearClipPlane, _projector.transform.forward);
            if (!Physics.Raycast(ray, out var hit, _origFarClipPlane - _origNearClipPlane, ~_projector.ignoreLayers)) return;
            var dist = hit.distance + _origNearClipPlane;
            _projector.nearClipPlane = Mathf.Max(dist - _distanceTolerance, 0);
            _projector.farClipPlane = dist + _distanceTolerance;
        }

        #endregion
    }
}