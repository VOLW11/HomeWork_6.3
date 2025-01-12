using Assets.CourseGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _ghost;
        private Entity _ghostTeleport;

        public void StartProcess(DIContainer container)
        {
            _container = container;

            _ghost = _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero);
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.forward * 4);

            Debug.Log($"Скорость созданного призрака: {_ghost.GetMoveSpeed().Value}");

            _ghostTeleport = _container.Resolve<EntityFactory>().CreateGhostTeleport(Vector3.zero); // ДЗ
        }

        private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (_ghost != null)
            {
                _ghost.GetMoveDirection().Value = input;
                _ghost.GetRotationDirection().Value = input;

                if (Input.GetKeyDown(KeyCode.F) && _ghost.TryGetTakeDamageRequest(out var takeDamageRequest))
                {
                    takeDamageRequest.Invoke(100);
                    Debug.Log("Здоровье: " + _ghost.GetHealth().Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.T) && _ghostTeleport != null)/// ДЗ
            {
                if (_ghostTeleport.TryGetIsTeleportEvent(out var isTeleport))
                    isTeleport.Invoke(_ghostTeleport.GetIsTeleport().Value = true);

               // _ghostTeleport.GetIsTeleportEvent().Invoke(_ghostTeleport.GetIsTeleport().Value = true);
               // _ghostTeleport.TryGetIsTeleportEvent()
               // _ghostTeleport.GetMoveDirection().Value = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f));
            }
        }
    }
}
