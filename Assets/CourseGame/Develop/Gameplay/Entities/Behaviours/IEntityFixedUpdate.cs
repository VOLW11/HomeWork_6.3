using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Entities.Behaviours
{
    public interface IEntityFixedUpdate : IEntityBehaviour
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}