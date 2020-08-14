using System;
using Helper;
using Interface;
using UnityEngine;

namespace Model
{
    public sealed class Aim : MonoBehaviour, ICollision, ISelectObj
    {
        #region Fields

        public event Action OnPointChange;

        public float Hp = 100;

        private bool _isDead;

        #endregion


        #region ICollision

        //todo дописать поглащение урона
        public void OnCollision(InfoCollision info)
        {
            if (_isDead) return;
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }

                Destroy(gameObject, 10);

                OnPointChange?.Invoke();
                _isDead = true;
            }
        }

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}