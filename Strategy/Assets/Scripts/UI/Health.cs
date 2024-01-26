using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _slider.value -= 0.1f;
        }
    }
}