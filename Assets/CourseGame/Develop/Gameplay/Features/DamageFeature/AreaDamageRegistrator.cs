using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DamageFeature
{
    public class AreaDamageRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Collider _areaDamage;

        public override void Register(Entity entity)
        {
            entity.AddSphereColliderDamage(_areaDamage);
        }
    }
}