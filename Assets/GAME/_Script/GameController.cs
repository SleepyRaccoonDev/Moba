using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private Character _player;
    [SerializeField] private float _forceMove;
    [SerializeField] private float _forceRotate;
    [SerializeField] private float _maxHP;

    [Header("Enemy")]
    [SerializeField] private Character[] _enemys;
    [SerializeField] private float _forceMoveEnemyMin;
    [SerializeField] private float _forceMoveEnemyMax;
    [SerializeField] private float _forceRotateEnemy;
    [SerializeField] private float _maxHPEnemyMin;
    [SerializeField] private float _maxHPEnemyMax;

    [Header("CharacterView")]
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private Transform _HPBar;
    [SerializeField] private Image _imageHP;

    [Header("Mine")]
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private Transform _spawnField;

    [Header("AidSpawner")]
    [SerializeField] private Aid _aidPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _healthToRestore;

    [Header("Sounds")]
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioClip _takeSound;
    [SerializeField] private AudioClip _explosiveSound;
    [SerializeField] private AudioSource _musicSource;

    private Controller _controller;
    private CharacterView _characterView;
    private InputsController _inputController;

    private List<Controller> _controllersEnemy = new List<Controller>();
    private List<CharacterView> _enemyViews = new List<CharacterView>();

    private AidSpawner _aidSpawner;

    private void Awake()
    {
        _inputController = new InputsController();

        CharacterCreator characterCreator = new CharacterCreator();

        SetPlayerSettings(characterCreator);

        SetEnemySettings(characterCreator);

        new MineSpawner(_spawnField, _minePrefab, _soundsSource, _explosiveSound).Spawn() ;

        _aidSpawner = new AidSpawner(_player, _aidPrefab, _soundsSource, _spawnDistance, _spawnTimer, _healthToRestore, _takeSound);
    }

    private void Update()
    {
        if (_inputController.IsLeftMouseButtonDown())
            _controller.OnUpdate(_inputController.GetMousePosition());

        if (_inputController.IsSpawnAidButtonPressed())
            _aidSpawner.ActivateSpawner();

        foreach (var controllerEnemy in _controllersEnemy)
            controllerEnemy.OnUpdate(_player.transform.position);

        _characterView.UpdateBehaviors();

        foreach (var enemyView in _enemyViews)
            enemyView.UpdateBehaviors();
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();

        foreach (var controllerEnemy in _controllersEnemy)
            controllerEnemy.OnFixedUpdate();
    }

    private void SetPlayerSettings(CharacterCreator characterCreator)
    {
        _controller = characterCreator.Create(
            _player,
            new CompositController(
                new MovableController(_player, _player, 0),
                new RotatableController(_player)),
            _maxHP, _forceMove, _forceRotate);

        _characterView = characterCreator.GetCharacterView(
            new HealthBarController(_player, _HPBar, _imageHP, _maxHP),
            new CharacterAnimationController(_player, _maxHP),
            new TargetPointController(_player, _controller, _pointPrefab)
            );
    }

    private void SetEnemySettings(CharacterCreator characterCreator)
    {
        foreach (var enemy in _enemys)
        {
            _controllersEnemy.Add(characterCreator.Create(
                enemy,
                new CompositController(
                    new NavMeshController(enemy, enemy, 0),
                    new RotatableController(enemy)),
                _maxHPEnemyMin,
                Random.Range(_forceMoveEnemyMin, _forceMoveEnemyMax),
                _forceRotateEnemy
                ));

            _enemyViews.Add(
                characterCreator.GetCharacterView(
                    new CharacterAnimationController(enemy, Random.Range(_maxHPEnemyMin, _maxHPEnemyMax)))
                );
        }
    }
}