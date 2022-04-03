using UnityEngine;

public class PhysicsEngine : MonoBehaviour {
    [SerializeField] private GameObject _sphere;
    [SerializeField] private float _initialAngularVelocity;
    [SerializeField] private float _mass;

    private float _r2d2;
    private float _nextAngle;
    private float _radius;
    private float _height;
    private float _angularMomentum;
    private float _angularVelocity;

    void Start() {
        _r2d2 = 180 / Mathf.PI;
        _nextAngle = 0;
        _radius = _sphere.transform.localScale.x / 2;
        _height = _sphere.transform.localScale.y;
        _angularMomentum = GetAngMo(_initialAngularVelocity);
        _angularVelocity = _initialAngularVelocity;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            IncrementRadius(0.1f);
            SetNewVel();
            print(_angularVelocity);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            IncrementRadius(-0.1f);
            SetNewVel();
            print(_angularVelocity);
        }
    }

    void FixedUpdate() {
        _nextAngle += _angularVelocity * _r2d2 * Time.fixedDeltaTime;
        _sphere.transform.rotation = Quaternion.AngleAxis(_nextAngle, Vector3.up);
    }

    public void IncrementRadius(float num) {
        float newRadius = _radius + num;

        if (newRadius <= 0.1) {
            return;
        }

        _radius = newRadius;
        Vector3 newScale = new Vector3(_radius * 2, _height, _radius * 2);
        _sphere.transform.localScale = newScale;
    }

    /// <summary>
    /// get an angy momo
    /// </summary>
    /// <param name="v">velocity</param>
    private float GetAngMo(float v) {
        return _mass * v * _radius;
    }

    /// <summary>
    /// sets new velocity 
    /// </summary>
    private void SetNewVel() {
        _angularVelocity = _angularMomentum / (_mass *_radius);
    }
}
