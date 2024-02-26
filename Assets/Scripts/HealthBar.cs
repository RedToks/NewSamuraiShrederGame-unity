using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerStats _target;
    private Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthSlider.value = _target.Health;
    }
    private void OnEnable()
    {
        _target.OnHealthChanged += UpdateValue;
    }

    private void OnDisable()
    {
        _target.OnHealthChanged -= UpdateValue;
    }

    public void UpdateValue(int value)
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = value;
        }
    }


}
