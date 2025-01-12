using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.TeleportFeature
{
    public class TeleportBehaviour : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;
        private ICondition _condition;

        private ReactiveVariable<bool> _isTeleport;
        private ReactiveEvent<Transform> _effectPosition;


        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _condition = entity.GetTeleportCondition();
            _effectPosition = entity.GetEffectTeleportEvent();
            _isTeleport = entity.GetIsTeleport();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
                return;

            _effectPosition.Invoke(_transform);
            _transform.position = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f));
            _effectPosition.Invoke(_transform);

            _isTeleport.Value = false;
        }
    }
}