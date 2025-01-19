namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public enum EntityValues
    {
        MoveDirection,
        MoveSpeed,
        MoveCondition,
        IsMoving,

        TeleportCondition, 

        RotationDirection,
        RotationSpeed,
        RotationCondition,
        
        SelfTriggerReciever,
        SelfTriggerDamage,

        CharacterController,
        Transform,

        Health,
        MaxHealth,

        Energy,
        MaxEnergy, 
        AmountEnergyForTeleport, 
        TimeRecoveryUnitEnergy, 
        RecoveryEnergyCondition,

        IsTeleportEvent, 
        EffectTeleportEvent, 
        IsTeleport, 
        IsTeleportProcess, 

        SphereColliderDamage, 

        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,

        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
    }
}
