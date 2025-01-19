using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Extensions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaDamageBehaviour : IEntityInitialize, IEntityDispose, IEntityUpdate
{
    private ICondition _condition;
    private Collider _collider;

    private TriggerReciever _triggerReciever;
    private ReactiveVariable<float> _damage;

    private IDisposable _disposableTriggerEnter;

    public void OnInit(Entity entity)
    {
        _condition = entity.GetTeleportCondition();
        _collider = entity.GetSphereColliderDamage();

        _collider.isTrigger = true;

      //  _condition.Changed += Attack;

        _triggerReciever = entity.GetSelfTriggerReciever();
        _damage = entity.GetSelfTriggerDamage();

        _disposableTriggerEnter = _triggerReciever.Enter.Subscribe(OnTriggerEnter);
    }

    private void Attack(bool arg1, bool arg2)
    {
        if (arg2 == true)
        {
            _collider.isTrigger = true;

             Debug.Log(_collider.isTrigger + " 1111");
            return;
        }
   
        //_collider.isTrigger = false;
   
    }

    private void OnTriggerEnter(Collider collider)
    {
        Entity otherEntity = collider.GetComponentInParent<Entity>();

        if (otherEntity != null)
        {
            Debug.Log("Õ¿ÿ≈À ¬–¿√¿, Õ¿ÕŒÿ” ”–ŒÕ");
            otherEntity.TryTakeDamage(_damage.Value);

            _collider.isTrigger = false;
        }
    }

    public void OnDispose()
    {
        _disposableTriggerEnter.Dispose();
    }

    public void OnUpdate(float deltaTime)
    {
        if (_condition.Evaluate())
        {
            /*_collider.isTrigger = true;

            Debug.Log(_collider.isTrigger + " 1111");
            return;*/
        }

      //  _collider.isTrigger = false;
    }
}

