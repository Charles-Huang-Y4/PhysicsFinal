using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Text _angMo;   // Represents Angular Momentum
    [SerializeField] private Text _angVelo; // Represents Angular Velocity
    [SerializeField] private Text _radiusValue;
    [SerializeField] private Text _massValue;

    private void Start() {
        _massValue.text = GameObject.Find("MassSlider").GetComponent<Slider>().value.ToString();
        _radiusValue.text = GameObject.Find("RadiusSlider").GetComponent<Slider>().value.ToString();
    }

    public void UpdateRadius(string val) {
        _radiusValue.text = val;
    }

    public void UpdateMass(string val) {
        _massValue.text = val;
    }

    public void UpdateAngMomentum(string val) {
        _angMo.text = "Angular Momentum: " + val;
    }

    public void UpdateAngVelocity(string val) {
        _angVelo.text = "Angular Velocity:\n" + val;
    }

    public void OnQuitButtonPress() {
        Application.Quit();
    }
}
