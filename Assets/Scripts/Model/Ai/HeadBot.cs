using System;
using Helper;
using Interface;
using UnityEngine;


namespace Model.Ai
{
    public sealed class HeadBot : MonoBehaviour, ICollision
    {
        #region Properties

        public event Action<InfoCollision> OnApplyDamageChange;

        #endregion


        #region ICollision

        public void OnCollision(InfoCollision info)
        {
            Debug.Log($"HeadBot.OnCollision.Info:{info.Damage}");
            OnApplyDamageChange?.Invoke(new InfoCollision(info.Damage * 500,
                info.Contact, info.ObjCollision, info.Direction));
        }

        #endregion
    }
}