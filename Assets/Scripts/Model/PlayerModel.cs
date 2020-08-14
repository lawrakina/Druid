using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using UnityEngine;


namespace Model
{
    public sealed class PlayerModel : BaseUnitModel, ICollision
    {
        #region Fields

        public event Action<BaseUnitModel> OnDieChange;

        #endregion


        #region Properties

        

        #endregion


        public void OnCollision(InfoCollision info)
        {
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }

                    Destroy(child.gameObject, 10);
                }

                OnDieChange?.Invoke(this);
            }
        }
    }
}