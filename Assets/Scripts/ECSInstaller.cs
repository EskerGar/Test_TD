using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Systems;
using UnityEngine;

public class ECSInstaller : MonoBehaviour
{
    [SerializeField] private ViewsKeeper _viewsKeeper;
    [SerializeField] private Configs _configs;
    
    private EcsWorld _ecsWorld;
    private EcsSystems _initSystems;
    private EcsSystems _updateSystems;

    private void Awake()
    {
        _ecsWorld = new EcsWorld();

        _initSystems = new EcsSystems(_ecsWorld);
        _updateSystems = new EcsSystems(_ecsWorld);

        var enemyPool = new EnemyPool(_viewsKeeper.EnemyViewPrefab);
        var bulletPool = new BulletPool(_viewsKeeper.BulletViewPrefab);

        _initSystems
            .Add(new CreateBaseSystem())
            .Add(new CreateGameEntitySystem())
            .Inject(_viewsKeeper)
            .Inject(_configs)
            .Init();

        _updateSystems
            .Add(new UpgradeBaseSystem())
            .Add(new FindTargetSystem())
            .Add(new AttackSystem())
            .Add(new AttackCooldownSystem())
            .Add(new SpawnTimerSystem())
            .Add(new EnemySpawnSystem())
            .Add(new MoveSystem())
            .Add(new CheckReachTargetSystem())
            .Add(new DamageSystem())
            .Add(new DestroySystem())
            .Add(new RestartGameSystem())
            .Add(new EventsDeletingSystem())
            .Inject(_viewsKeeper)
            .Inject(_configs)
            .Inject(enemyPool)
            .Inject(bulletPool)
            .Init();
    }

    private void Update()
    {
        _updateSystems?.Run();
    }

    public void OnDestroy()
    {
        _initSystems?.Destroy();
        _updateSystems?.Destroy();
        _initSystems = null;
        _updateSystems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}
