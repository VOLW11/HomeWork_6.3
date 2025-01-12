using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.CourseGame.Develop.Gameplay.Features.TeleportFeature
{
    //  [RequireComponent(typeof(ParticleSystem))]
    public class TeleportView : EntityView
    {
        [SerializeField] private ParticleSystem _prefabTeleportEffect;

        private ReactiveEvent<Transform> _effectPosition;
        private IDisposable _disposableEffectView;

        /* private void OnValidate()
         {
             _prefabTeleportEffect ??= GetComponent<ParticleSystem>();
         }*/

        protected override void OnEntityInitialized(Entity entity)
        {
            _effectPosition = entity.GetEffectTeleportEvent();
            _disposableEffectView = _effectPosition.Subscribe(EffectView);
        }

        private void EffectView(Transform transform)
        {
            ParticleSystem teleportEffect = Instantiate(_prefabTeleportEffect, transform.position, Quaternion.identity, null);
            teleportEffect.Play();
        }

        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _disposableEffectView.Dispose();
        }
    }
}