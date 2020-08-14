using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using UnityEngine;
using UnityEngine.AI;


namespace Model
{
    public abstract class BaseUnitModel : BaseObjectScene, IExecute
        ///todo IExecute перенести с потомков сюда, после того как будет сделан  
        /// собственный контроллер передвижения(убрать IMotor и CharacterController)

        //todo сделать базовую машину состояний для всех Unit`ов
    {
        #region Fields

        [SerializeField] protected float _hp = 100;
        private protected StateUnit _state;

        //костыль: нахождение на земле начинаем проверять через пол секунды после получения AddForce
        private float _timeStunned = 0.5f;
        private float _distanceCheckGround = 1.05f;

        #region flags

        //костыль: нахождение на земле начинаем проверять через пол секунды после получения AddForce
        private bool _stunned = false;
        private bool _unstunnedRun = false;

        private protected bool IsCharacterController;
        private protected bool IsNavMeshAgent;
        private protected bool IsKinematicRigidBody;

        #endregion

        private protected CharacterController CashCharacterController;
        private protected NavMeshAgent CashNavMeshAgent;
        private protected bool CashKinematicRigidBody;

        #endregion


        #region Properties

        public StateUnit StateUnit
        {
            get => _state;
            set => _state = value;
        }

        public float Hp
        {
            get { return _hp; }
            set { _hp = value; } //todo добавить расчет коэффициента снижения урона по броне (все закешировать)
        }

        public float PercentXp => Hp; //todo добавить расчет по формуле: выносливость * 17 с занесением в кеш

        #endregion


        #region Methods

        private void Start()
        {
            CashCharacterController = GetComponent<CharacterController>();
            IsCharacterController = CashCharacterController != null;

            CashNavMeshAgent = GetComponent<NavMeshAgent>();
            IsNavMeshAgent = CashNavMeshAgent != null;

            CashKinematicRigidBody = Rigidbody.isKinematic;
            IsKinematicRigidBody = true;
        }

        #endregion


        public void Bang(InfoCollision collision)
        {
            if (IsCharacterController)
                CashCharacterController.enabled = false;
            if (IsNavMeshAgent)
                CashNavMeshAgent.enabled = false;
            if (IsKinematicRigidBody)
                Rigidbody.isKinematic = !CashKinematicRigidBody;

            Rigidbody.AddForce(collision.Direction, ForceMode.Impulse);

            var tempObj = GetComponent<ICollision>();
            if (tempObj != null)
                tempObj.OnCollision(collision);

            _state = StateUnit.Fly;
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
                {
                    CashNavMeshAgent.enabled = true;
                }

                if (IsKinematicRigidBody)
                    Rigidbody.isKinematic = CashKinematicRigidBody;
                StateUnit = StateUnit.Indy;

                Debug.Log($"PlayerModel.CheckGround:TRUE");
            }
            else
                Debug.Log($"PlayerModel.CheckGround:FALSE");
        }

        // private void OnCollisionEnter(Collision other)
        // {
        //     //todo переделать на рейкаст под себя и состояние нахождения на земле
        //     //проверка на принадлежность касаемого объекта к окружению
        //     if ((LayerMask.NameToLayer(TagManager.LAYER_MASK_ENVIRONMENT) & (1 << other.gameObject.layer)) == 0)
        //     {
        //         if (IsCharacterController)
        //             CashCharacterController.enabled = true;
        //         if (IsNavMeshAgent)
        //             CashNavMeshAgent.enabled = true;
        //         if (IsKinematicRigidBody)
        //             Rigidbody.isKinematic = CashKinematicRigidBody;
        //     }
        // }

        public void OnHealing(float delta)
        {
            if (Hp > 0)
            {
                Hp += delta;
            }
        }

        public virtual void Execute()
        {
            //todo привязать машину состояний, делать проверку CheckGround только в состояниях Indy,Run,Invisible,Float,Fly
            switch (StateUnit)
            {
                case StateUnit.None:
                    StateUnitNone();
                    break;
                case StateUnit.Indy:
                    StateUnitIndy();
                    break;
                case StateUnit.Run:
                    StateUnitRun();
                    break;
                case StateUnit.Attack:
                    StateUnitAttack();
                    break;
                case StateUnit.Attacked:
                    StateUnitAttacked();
                    break;
                case StateUnit.Invisible:
                    StateUnitInvisible();
                    break;
                case StateUnit.Stunned:
                    StateUnitStunned();
                    break;
                case StateUnit.Float:
                    StateUnitFloat();
                    break;
                case StateUnit.Fly:
                    StateUnitFly();
                    break;
                case StateUnit.Died:
                    StateUnitDied();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region StateUnit

        protected void StateUnitNone()
        {
        }

        protected void StateUnitIndy()
        {
        }

        protected void StateUnitRun()
        {
        }

        protected void StateUnitAttack()
        {
        }

        protected void StateUnitAttacked()
        {
        }

        protected void StateUnitInvisible()
        {
        }

        protected void StateUnitStunned()
        {
        }

        protected void StateUnitFloat()
        {
        }

        protected void StateUnitFly()
        {
            if (!_unstunnedRun)
                UnStunned();
            else
                CheckGround();
        }

        protected void StateUnitDied()
        {
        }

        #endregion
    }
}