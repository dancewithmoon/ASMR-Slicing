using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _filling;

        public void UpdateValue(float value) => 
            _filling.fillAmount = value;
    }
}