using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using UnityEngine;


namespace Model
{
    public sealed class PlayerModel : BaseUnitModel, ICollision, IExecute
    {
        #region Fields

        [SerializeField] private float _distanceCheckGround = 1.05f;
        public event Action<BaseUnitModel> OnDieChange;

        private bool _stunned = false;
        private bool _unstunnedRun = false;
        private float _timeStunned = 0.5f;

        #endregion


        #region Properties

        public StateUnit StateUnit
        {
            get => _state;
            set
            {
                _state = value;
                switch (value)
                {
                    case StateUnit.None:
                        break;
                    case StateUnit.Indy:
                        break;
                    case StateUnit.Run:
                        break;
                    case StateUnit.Attack:
                        break;
                    case StateUnit.Attacked:
                        break;
                    case StateUnit.Invisible:
                        break;
                    case StateUnit.Stunned:
                        break;
                    case StateUnit.Float:
                        break;
                    case StateUnit.Fly:
                        break;
                    case StateUnit.Died:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

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

        public void Execute()
        {
            //todo привязать машину состояний, делать проверку ChechGround только в состояниях Indy,Run,Invisible,Float,Fly
            switch (StateUnit)
            {
                case StateUnit.Died:
                    return;
                case StateUnit.Fly:
                    if (!_unstunnedRun) 
                        UnStunned();
                    else
                        CheckGround();
                    break;
                case StateUnit.None:
                    break;
                case StateUnit.Indy:
                    break;
                case StateUnit.Run:
                    break;
                case StateUnit.Attack:
                    break;
                case StateUnit.Attacked:
                    break;
                case StateUnit.Invisible:
                    break;
                case StateUnit.Stunned:
                    break;
                case StateUnit.Float:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnStunned()
        {
            _stunned = true;
            _unstunnedRun = true;
            var timeRemainingUnStunned = new TimeRemaining(delegate { _stunned = false; }, _timeStunned);
            timeRemainingUnStunned.AddTimeRemainingExecute();
        }

        private void CheckGround()
        {
            if (_stunned) return;
            Debug.DrawRay(Transform.position, Vector3.down, Color.magenta, _distanceCheckGround);
            if (Physics.Raycast(Transform.position, Vector3.down, _distanceCheckGround))
            {
                if (IsCharacterController)
                    CashCharacterController.enabled = true;
                if (IsNavMeshAgent)
                    CashNavMeshAgent.enabled = true;
                if (IsKinematicRigidBody)
                    Rigidbody.isKinematic = CashKinematicRigidBody;
                StateUnit = StateUnit.Indy;

                Debug.Log($"PlayerModel.CheckGround:TRUE");
            }
            else
                Debug.Log($"PlayerModel.CheckGround:FALSE");
        }
    }
}