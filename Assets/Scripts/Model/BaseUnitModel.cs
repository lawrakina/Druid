using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;


namespace Model
{
    public abstract class BaseUnitModel : BaseObjectScene
        ///todo IExecute перенести с потомков сюда, после того как будет сделан  
        /// собственный контроллер передвижения(убрать IMotor и CharacterController)
        
        //todo сделать базовую машину состояний для всех Unit`ов
    {
        #region Fields

        [SerializeField] protected float _hp = 100;
        private protected StateUnit _state;

        #region flags

        private protected bool IsCharacterController;
        private protected bool IsNavMeshAgent;
        private protected bool IsKinematicRigidBody;

        #endregion

        private protected CharacterController CashCharacterController;
        private protected NavMeshAgent CashNavMeshAgent;
        private protected bool CashKinematicRigidBody;

        #endregion


        #region Properties

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
    }
}