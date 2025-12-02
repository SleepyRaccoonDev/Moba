using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private Character _character;
    [SerializeField] private float _forceMove;
    [SerializeField] private float _forceRotate;
    [SerializeField] private float _maxHP;

    [Header("CharacterView")]
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private Transform _HPBar;
    [SerializeField] private Image _imageHP;

    [Header("Mine")]
    [SerializeField] private GameObject _minePrefab;
    [SerializeField] private Transform _spawnField;
    
    private Controller _controller;
    private CharacterView _characterView;
    private InputsController _inputController;

    private void Awake()
    {
        _inputController = new InputsController();

        CharacterCreator characterCreator = new CharacterCreator(
            _character,
            new NavMeshCharacterController(_character, 0, _pointPrefab), _maxHP,
            new PhysicMover(_forceMove),
            new PhysicRotator(_forceRotate));;

        _controller = characterCreator.GetCharacterController();

        _characterView = characterCreator.GetCharacterView(
            new HealthBarController(_character, _HPBar, _imageHP, _maxHP),
            new CharacterAnimationController(_character, _maxHP)
            );

        new MineSpawner(_spawnField, _minePrefab).Spawn() ;
    }

    private void Update()
    {
        if (_inputController.IsLeftMouseButtonDown())
            _controller.OnUpdate(_inputController.GetMousePosition());

        _characterView.UpdateBehaviors();
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }
}