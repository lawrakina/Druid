using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Model.Ai
{
    public class Bot : BaseUnitModel, IExecute, ISelectObj
    {
        #region Properties

        public Vision Vision;
        public Weapon Weapon; //todo с разным оружием 
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        private float _waitTime = 3;
        private StateBot _stateBot;
        private Vector3 _point;
        private float _stoppingDistance = 10.0f;
        private float _patrolStoppingDistance = 0.0f;
        private float _deltaTimeMoving = 0.0f;
        private float _timeToMoving = 5.0f;

        public event Action<Bot> OnDieChange;
        private ITimeRemaining _timeRemaining;

        private StateBot StateBot
        {
            get => _stateBot;
            set
            {
                _stateBot = value;
                switch (value)
                {
                    case StateBot.None:
                        Color = Color.white;
                        break;
                    case StateBot.Patrol:
                        Color = Color.green;
                        break;
                    case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
                    case StateBot.Detected:
                        Color = Color.red;
                        break;
                    case StateBot.Died:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            Agent.stoppingDistance = _stoppingDistance;
            _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
            Weapon.enabled = true;
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (StateBot == StateBot.Died) return;

            if (StateBot != StateBot.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (StateBot != StateBot.Inspection)
                    {
                        if (StateBot != StateBot.Patrol)
                        {
                            StateBot = StateBot.Patrol;
                            _point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
                            Agent.stoppingDistance = _patrolStoppingDistance;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                StateBot = StateBot.Inspection;
                                _timeRemaining.AddTimeRemainingExecute();
                            }
                        }
                    }
                }

                if (Vision.VisionM(transform, Target))
                {
                    StateBot = StateBot.Detected;
                }
            }
            else
            {
                if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    // Debug.Log($"Agent.stoppingDistance {Agent.stoppingDistance}");
                    Agent.stoppingDistance = _stoppingDistance;
                }

                if (Vision.VisionM(transform, Target))
                {
                    // Debug.Log($"Weapon.Fire(); {transform.name}, {Target.name}");
                    Weapon.Fire();
                }
                else
                {
                    MovePoint(Target.position);

                    if (_deltaTimeMoving > _timeToMoving)
                    {
                        _deltaTimeMoving = 0.0f;

                        StateBot = StateBot.Patrol;
                        _point = Patrol.GenericPoint(transform);
                        MovePoint(_point);
                        Agent.stoppingDistance = _patrolStoppingDistance;
                    }
                    else
                    {
                        _deltaTimeMoving += Time.deltaTime;
                    }
                }
            }
        }

        #endregion


        #region Methods

        public void MovePoint(Vector3 point)
        {
            if (Agent.enabled)
                Agent.SetDestination(point);
        }

        private void ResetStateBot()
        {
            StateBot = StateBot.None;
        }

        private void SetDamage(InfoCollision info)
        {
            //todo реакциия на попадание  
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                StateBot = StateBot.Died;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }

                    tempRbChild.isKinematic = false;
                    tempRbChild.AddForce(info.Direction * Random.Range(10, 20));

                    Destroy(child.gameObject, 10);
                }

                OnDieChange?.Invoke(this);
            }
        }

        #endregion
        
        
        #region ISelectObject

        public string GetMessage()
        {
            return $"{name}, Hp:{Hp}";
        }

        #endregion
    }
}