using System;
using UnityEngine;


namespace Controller
{
    public sealed class GameController : MonoBehaviour
    {

        #region Fields

        private Controllers _controllers;

        #endregion

        
        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _controllers.FixedLenght; i++)
            {
                _controllers.FixedExecute[i].FixedExecute();
            }
        }

        #endregion
    }
}