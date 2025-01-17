using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAreaDamageBehaviour : IEntityInitialize, IEntityUpdate
{
    private IReadOnlyVariable<bool> _condition;
    private Collider _collider;

    public void OnInit(Entity entity)
    {
        _condition = entity.GetIsTeleport();
        _collider = entity.GetSphereColliderDamage();

        _collider.isTrigger = false;

        _condition.Changed += Attack;
    }

    private void Attack(bool arg1, bool arg2)
    {
        if (arg2 == true)
        {
            _collider.isTrigger = true;
            
            Debug.Log(_collider.isTrigger + "1111");
            return;
        }
        _collider.isTrigger = false;
        Debug.Log(_collider.isTrigger + "לעגנל");
    }

    public void OnUpdate(float deltaTime)
    {
       /* if (_condition.Value)
        {
            _collider.isTrigger = true;

            Debug.Log(_collider.isTrigger);
        }

        _collider.isTrigger = false;*/
    }
}

