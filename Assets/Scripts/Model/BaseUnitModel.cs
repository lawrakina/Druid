using System;
using Controller.TimeRemaining;
using Helper;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;


namespace Model
{
    public abstract class BaseUnitModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected float _hp = 100;

        #region flags

        private bool _isCharacterController;
        private bool _isNavMeshAgent;
        private bool _isKinematicRigidBody;

        #endregion

        private CharacterController _cashCharacterController;
        private NavMeshAgent _cashNavMeshAgent;
        private bool _cashKinematicRigidBody;

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
            _cashCharacterController = GetComponent<CharacterController>();
            _isCharacterController = _cashCharacterController != null;

            _cashNavMeshAgent = GetComponent<NavMeshAgent>();
            _isNavMeshAgent = _cashNavMeshAgent != null;

            _cashKinematicRigidBody = Rigidbody.isKinematic;
            _isKinematicRigidBody = true;
        }

        #endregion


        public void Bang(InfoCollision collision)
        {
            if (_isCharacterController)
                _cashCharacterController.enabled = false;
            if (_isNavMeshAgent)
                _cashNavMeshAgent.enabled = false;
            if (_isKinematicRigidBody)
                Rigidbody.isKinematic = !_cashKinematicRigidBody;

            Rigidbody.AddForce(collision.Direction, ForceMode.Impulse);

            var tempObj = GetComponent<ICollision>();
            if (tempObj != null)
                tempObj.OnCollision(collision);
        }

        private void OnCollisionEnter(Collision other)
        {
            //todo переделать на рейкаст под себя и состояние нахождения на земле
            //проверка на принадлежность касаемого объекта к окружению
            if ((LayerMask.NameToLayer(TagManager.LAYER_MASK_ENVIRONMENT) & (1 << other.gameObject.layer)) == 0)
            {
                if (_isCharacterController)
                    _cashCharacterController.enabled = true;
                if (_isNavMeshAgent)
                    _cashNavMeshAgent.enabled = true;
                if (_isKinematicRigidBody)
                    Rigidbody.isKinematic = _cashKinematicRigidBody;
            }
        }

        public void OnHealing(float delta)
        {
            if (Hp > 0)
            {
                Hp += delta;
            }
        }
    }
}