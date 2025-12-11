using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : IViewBehaviour
{
    private Character _character;
    private Camera _camera;
    private Transform _transform;
    private Image _image;
    private float _maxHealh;

    public HealthBarController(Character character, Transform transform, Image image, float maxHealth)
    {
        _camera = Camera.main;
        _transform = transform;
        _character = character;
        _image = image;
        _maxHealh = maxHealth;

        _image.type = Image.Type.Filled;
    }

    public void Perform()
    {
        Rotate();
        ChangeCapacity();
        ChangeColor();
    } 

    private void Rotate()
    {
        Vector3 lookDirection = _camera.transform.position - _transform.position;
        lookDirection.y = 0f;
        lookDirection.Normalize();

        _transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void ChangeCapacity()
    {
        _image.fillAmount = _character.Health / _maxHealh;
    }

    private void ChangeColor()
    {
        if (_character.Health / _maxHealh < .3f)
            _image.color = Color.red;
        else
            _image.color = Color.green;
    }
}