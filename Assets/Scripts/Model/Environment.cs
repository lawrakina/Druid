using Helper;
using Interface;
using UnityEngine;


namespace Model
{
    public abstract class Environment : BaseObjectScene, ICollision
    {
        #region Fields

        [SerializeField] private BulletProjector _projector; //todo manager

        #endregion


        #region ICollision

        public virtual void OnCollision(InfoCollision info)
        {
            if (_projector == null) return;
            var projectorRotation = Quaternion.FromToRotation(-Vector3.forward, info.Contact.normal);
            var obj = Instantiate(_projector, info.Contact.point + info.Contact.normal * 0.25f,
                projectorRotation, info.ObjCollision); //todo manager
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y,
                Random.Range(0, 360));
        }

        #endregion
    }
}