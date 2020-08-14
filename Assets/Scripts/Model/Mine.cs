using System;
using Controller.TimeRemaining;
using Helper;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;


namespace Model
{
    public sealed class Mine : BaseObjectScene
    {
        #region Properties

        public event Action OnPointChange = delegate { };

        #endregion


        #region Fields

        [SerializeField] private float _radius = 5.0f;
        [SerializeField] private float _damagePoints = 20.0f;
        [SerializeField] private float _boomPower = 10;
        [SerializeField] private LayerMask _mask;
        private Collider[] _arrayCollider = new Collider[5];
        [SerializeField] private bool _enableDetection = true;

        #endregion


        #region ICollision

        private void OnTriggerEnter(Collider other)
        {
            Boom();
        }

        private void OnCollisionEnter(Collision other)
        {
            // if((_mask & (1 << other.gameObject.layer)) == 0)
            if(other.gameObject.layer == 8)
               Boom();
        }

        private void Boom()
        {
            if (!_enableDetection) return;
            _arrayCollider = Physics.OverlapSphere(Transform.position, _radius, _mask);
            foreach (var item in _arrayCollider)
            {
                if (item == null) continue;
                var baseUnitModel = item.gameObject.GetComponent<BaseUnitModel>();
                if (baseUnitModel)
                {
                    baseUnitModel.Bang(new InfoCollision
                    (
                        _damagePoints,
                        new ContactPoint(),
                        new RectTransform(),
                        (baseUnitModel.Rigidbody.position - new Vector3(Transform.position.x, Transform.position.y - 1,
                            Transform.position.z)
                        ).normalized * _boomPower));
                }
            }

            _enableDetection = false;
            Destroy(gameObject);
        }

        #endregion
    }
}