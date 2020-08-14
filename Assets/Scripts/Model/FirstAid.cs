using System;
using Controller;
using Interface;
using Manager;
using UnityEngine;


namespace Model
{
    public sealed class FirstAid : MonoBehaviour, ISelectObj
    {
        #region Properties

        public event Action OnPointChange = delegate {};

        #endregion


        #region Fields

        [SerializeField] private float _healthPoints = 20.0f;

        #endregion


        #region ICollision

        private void OnTriggerEnter(Collider other)
        {
            var tempObject = other.gameObject.GetComponent<BaseUnitModel>();
            if(tempObject != null)
                tempObject.OnHealing(_healthPoints);
            Destroy(gameObject, 0.5f);
        }

        #endregion


        #region ISelectObject

        public string GetMessage()
        {
            return $"{gameObject.name}, Health time: {_healthPoints}";
        }

        #endregion
    }
}