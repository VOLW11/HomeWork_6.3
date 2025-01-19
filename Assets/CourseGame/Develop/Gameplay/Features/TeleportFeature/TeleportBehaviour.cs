using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.TeleportFeature
{
    public class TeleportBehaviour : IEntityInitialize
    {
        private Transform _transform;
        private ICondition _condition;

        private ReactiveVariable<bool> _isTeleport;
        private ReactiveVariable<bool> _isTeleportProcess;

        private ReactiveEvent<Transform> _effectPosition;
        private ReactiveEvent _useEnergyEvent;

        private static float _teleportDistance = 4f;

        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _condition = entity.GetTeleportCondition();
            _effectPosition = entity.GetEffectTeleportEvent();

            _isTeleportProcess = entity.GetIsTeleportProcess();
            _isTeleport = entity.GetIsTeleport();
      
            _isTeleport.Changed += Teleport;

            _useEnergyEvent = entity.GetIsTeleportEvent();
        }

        private void Teleport(bool arg1, bool arg2)
        {
            if (_condition.Evaluate() == false)
            {
                _isTeleport.Value = false;
                return;
            }

            _useEnergyEvent.Invoke();

            _effectPosition.Invoke(_transform);
            _transform.position = new Vector3(Random.Range(-_teleportDistance, _teleportDistance), 0, 
                                              Random.Range(-_teleportDistance, _teleportDistance));
            _effectPosition.Invoke(_transform);

            _isTeleportProcess.Value = true;
            _isTeleport.Value = false;
        }
    }
}