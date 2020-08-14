using System;
using Helper;
using Interface;
using UnityEngine;


namespace Model.Ai
{
    public sealed class BodyBot : MonoBehaviour, ICollision
    {
        #region Properties

        public event Action<InfoCollision> OnApplyDamageChange;

        #endregion


        #region ICollision

        public void OnCollision(InfoCollision info)
        {
            Debug.Log($"BodyBot.OnCollision.Info:{info.Damage}");
            OnApplyDamageChange?.Invoke(info);
        }

        #endregion
    }
}