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
        private IReadOnlyVariable<Vector3> _direction;
        private ICondition _condition;

        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _direction = entity.GetMoveDirection();
            _condition = entity.GetTeleportCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
                return;

            _transform.position = _direction.Value;
        }
    }
}