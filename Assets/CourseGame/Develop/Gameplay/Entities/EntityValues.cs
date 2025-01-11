namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public enum EntityValues
    {
        MoveDirection,
        MoveSpeed,
        MoveCondition,
        IsMoving,

        TeleportCondition, // ДЗ

        RotationDirection,
        RotationSpeed,
        RotationCondition,
        
        SelfTriggerReciever,
        SelfTriggerDamage,

        CharacterController,
        Transform,

        Health,
        MaxHealth,

        Energy,// ДЗ
        MaxEnergy, //ДЗ
        TimeRecoveryUnitEnergy, // ДЗ
        RecoveryEnergyCondition,// ДЗ

        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,

        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
    }
}
