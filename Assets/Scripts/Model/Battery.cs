using System;
using Controller;
using Helper;
using Interface;
using Manager;
using UnityEngine;


namespace Model
{
    public sealed class Battery : MonoBehaviour, ISelectObj
    {
        #region Properties

        public event Action OnPointChange = delegate {};

        #endregion


        #region Fields

        [SerializeField] private float _lightPoints = 20.0f;
        private bool _isDead;

        #endregion


        #region ICollision

        private void OnTriggerEnter(Collider other)
        {
            if(other.name !=  TagManager.PLAYER)
                return;
            // Debug.Log(other.name);
            ServiceLocator.Resolve<FlashLightController>().ChangeBattery(_lightPoints);
            Destroy(gameObject, 0.5f);
        }

        #endregion


        #region ISelectObject

        public string GetMessage()
        {
            return $"{gameObject.name}, Light time: {_lightPoints}";
        }

        #endregion
    }
}
