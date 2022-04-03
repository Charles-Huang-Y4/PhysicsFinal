using UnityEngine;
using UnityEngine.UI;

public class PhysicsEngine : MonoBehaviour {
    [SerializeField] private UIManager _uiMgr;
    [SerializeField] private GameObject _physicsObj;
    [SerializeField] private Slider _massSlider;
    [SerializeField] private Slider _radiusSlider;
    [SerializeField] private float _initialOmega;

    private float _r2d2;
    private float _nextAngle;
    private float _radius;
    private float _height;
    private float _mass;
    private float _angularMomentum;
    private float _angularVelocity;

    void Start() {
        _r2d2 = 180 / Mathf.PI;
        _nextAngle = 0;
        _radiusSlider.value = _physicsObj.transform.localScale.x / 2;
        _height = _physicsObj.transform.localScale.y;
        _radius = _radiusSlider.value;
        _angularMomentum = GetAngMo(_initialOmega);
        _angularVelocity = _initialOmega;
        _mass = _massSlider.value;
    }

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.W)) {
    //        SetRadius(0.1f);
    //        CalculateAngularVelocity();
    //        print(_angularVelocity);
    //    } else if (Input.GetKeyDown(KeyCode.S)) {
    //        SetRadius(-0.1f);
    //        CalculateAngularVelocity();
    //        print(_angularVelocity);
    //    }
    //}

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
    }

    public void SetMass() {
        _mass = _massSlider.value;
        _uiMgr.UpdateMass(_mass.ToString());
        CalculateAngularVelocity();
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
    }
}
