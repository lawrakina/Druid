using System.Collections.Generic;
using Helper;
using Interface;
using Model.Ai;
using UnityEngine;


namespace Controller
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private int _countBot;
        private readonly List<Bot> _botList = new List<Bot>();

        #endregion

        
        #region IInitialization

        public void Initialization()
        {
            _countBot = ServiceLocatorMonoBehaviour.GetService<Reference>().BotsCount();
            for (var index = 0; index < _countBot; index++)
            {
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform; 
                //todo разных противников
                AddBotToList(tempBot);
            }
        }

        #endregion


        #region Methods

        private void AddBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChange += RemoveBotToList;
            }
        }

        private void RemoveBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotToList;
            _botList.Remove(bot);
        }

        #endregion

        
        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _botList.Count; i++)
            {
                _botList[i].Execute();
            }
        }

        #endregion
    }
}