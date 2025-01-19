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

public class AreaDamageBehaviour : IEntityInitialize, IEntityDispose
{
    private ReactiveEvent _isTeleportEvent;
    private ReactiveVariable<float> _damage;
    private TriggerReciever _triggerReciever;
    private ReactiveVariable<bool> _isTeleportProcess;

    private IDisposable _disposableTriggerEnter;

    public void OnInit(Entity entity)
    {
        _triggerReciever = entity.GetSelfTriggerReciever();
        _damage = entity.GetSelfTriggerDamage();
        _isTeleportProcess = entity.GetIsTeleportProcess();

        _disposableTriggerEnter = _triggerReciever.Enter.Subscribe(OnTriggerEnter);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Entity otherEntity = collider.GetComponentInParent<Entity>();

        if (otherEntity != null && _isTeleportProcess.Value)
        {
            Debug.Log("Урон по области");
            otherEntity.TryTakeDamage(_damage.Value);

            _isTeleportProcess.Value = false;
        }

        _isTeleportProcess.Value = false;
    }

    public void OnDispose()
    {
        _disposableTriggerEnter.Dispose();
    }
}

