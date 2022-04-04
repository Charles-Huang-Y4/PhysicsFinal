using UnityEngine;
using UnityEngine.UI;

public class PhysicsEngine : MonoBehaviour {
    [SerializeField] private UIManager _uiMgr;
    [SerializeField] private GameObject _physicsObj;
    [SerializeField] private Slider _massSlider;
    [SerializeField] private Slider _radiusSlider;
    [SerializeField] private float _initialAngVelocity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private float _r2d2; // radians to degrees ratio
    private float _nextAngle;
    private float _radius;
    private float _height;
    private float _mass;
    private float _angularMomentum;
    private float _angularVelocity;
    private AudioSource _audio;
    private float _pitchIncrement;
    private float _pitchDecrement;

    void Start() {
        _r2d2 = 180 / Mathf.PI;
        _nextAngle = 0;
        _height = _physicsObj.transform.localScale.y;

        _radius = _radiusSlider.value;
        _mass = _massSlider.value;
        _physicsObj.transform.localScale = new Vector3(_radius * 2, _height, _radius * 2);

        _angularMomentum = GetAngMo(_initialAngVelocity);
        _angularVelocity = _initialAngVelocity;
        _uiMgr.UpdateAngMomentum(_angularMomentum.ToString());
        _uiMgr.UpdateAngVelocity(_angularVelocity.ToString());

        _audio = GetComponent<AudioSource>();
        CalibratePitch();
    }

    void FixedUpdate() {
        _nextAngle += _angularVelocity * _r2d2 * Time.fixedDeltaTime;
        _physicsObj.transform.rotation = Quaternion.AngleAxis(_nextAngle, Vector3.up);
    }

    public void SetRadius() {
        _radius = _radiusSlider.value;
        Vector3 newScale = new Vector3(_radius * 2, _height, _radius * 2);
        _physicsObj.transform.localScale = newScale;
        _uiMgr.UpdateRadius(_radius.ToString());
        CalculateAngularVelocity();
        setPitch();
    }

    public void SetMass() {
        _mass = _massSlider.value;
        _uiMgr.UpdateMass(_mass.ToString());
        CalculateAngularVelocity();
        setPitch();
    }

    /// <summary>
    /// get an angy momo
    /// </summary>
    /// <param name="v">velocity</param>
    private float GetAngMo(float v) {
        return _mass * v * _radius;
    }

    /// <summary>
    /// sets new angular velocity 
    /// </summary>
    private void CalculateAngularVelocity() {
        _angularVelocity = _angularMomentum / (_mass *_radius);
        _uiMgr.UpdateAngMomentum(_angularMomentum.ToString());
        _uiMgr.UpdateAngVelocity(_angularVelocity.ToString());
    }

    private void setPitch() {
        if (_angularVelocity < 5) {
            _audio.pitch = 1 - (5 - _angularVelocity) * _pitchDecrement;
        } else if (_angularVelocity > 5) {
            _audio.pitch = 1 + (_angularVelocity - 5) * _pitchIncrement;
        // to account for float imprecision? 
        } else {
            _audio.pitch = 1;
        }
    }

    /// <summary>
    /// Calibrates pitch increment and decrement based on slider and min/max pitch values.
    /// </summary>
    private void CalibratePitch() {
        float maxAngularVelocity = _angularMomentum / (_massSlider.maxValue * _radiusSlider.maxValue);
        float minAngularVelocity = _angularMomentum / (_massSlider.minValue * _radiusSlider.minValue);

        _pitchIncrement = (_maxPitch - 1) / (minAngularVelocity - _initialAngVelocity);
        _pitchDecrement = (1 - _minPitch) / (_initialAngVelocity - maxAngularVelocity);
    }

    private void OnValidate() {
        if(_minPitch < 0.1) {
            _minPitch = 0.1f;
        }

        if (_maxPitch > 3) {
            _maxPitch = 3;
        }
    }
}
