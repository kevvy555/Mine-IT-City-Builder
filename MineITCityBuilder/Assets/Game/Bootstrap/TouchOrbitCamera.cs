using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace MineIT.CityBuilder.Bootstrap
{
    public sealed class TouchOrbitCamera : MonoBehaviour
    {
        [SerializeField] private float panSensitivity = 0.0018f;
        [SerializeField] private float zoomSensitivity = 0.65f;
        [SerializeField] private float rotateSensitivity = 0.22f;

        private Vector3 _focus;
        private float _yaw;
        private float _pitch;
        private float _distance;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            ResetView();
        }

        private void OnDisable()
        {
            if (EnhancedTouchSupport.enabled)
            {
                EnhancedTouchSupport.Disable();
            }
        }

        public void ResetView()
        {
            _focus = BootstrapLayout.CityCentre;
            _yaw = 38f;
            _pitch = 54f;
            _distance = BootstrapLayout.CityExtent * 0.92f;
            ApplyPose();
        }

        private void Update()
        {
            HandleTouch();
            HandleMouse();
            ApplyPose();
        }

        private void HandleTouch()
        {
            var touches = ETouch.activeTouches;
            if (touches.Count == 1)
            {
                Pan(touches[0].delta);
                return;
            }

            if (touches.Count < 2)
            {
                return;
            }

            var first = touches[0];
            var second = touches[1];
            var firstPrevious = first.screenPosition - first.delta;
            var secondPrevious = second.screenPosition - second.delta;

            var previousVector = secondPrevious - firstPrevious;
            var currentVector = second.screenPosition - first.screenPosition;
            var previousDistance = previousVector.magnitude;
            var currentDistance = currentVector.magnitude;
            Zoom((currentDistance - previousDistance) * zoomSensitivity);

            var previousAngle = Mathf.Atan2(previousVector.y, previousVector.x) * Mathf.Rad2Deg;
            var currentAngle = Mathf.Atan2(currentVector.y, currentVector.x) * Mathf.Rad2Deg;
            _yaw += Mathf.DeltaAngle(previousAngle, currentAngle);

            var midpointDelta = (first.delta + second.delta) * 0.5f;
            Pan(midpointDelta);
        }

        private void HandleMouse()
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }

            if (mouse.middleButton.isPressed)
            {
                Pan(mouse.delta.ReadValue());
            }

            if (mouse.rightButton.isPressed)
            {
                var delta = mouse.delta.ReadValue();
                _yaw += delta.x * rotateSensitivity;
                _pitch = Mathf.Clamp(_pitch - delta.y * rotateSensitivity, 25f, 78f);
            }

            var scroll = mouse.scroll.ReadValue().y;
            if (Mathf.Abs(scroll) > 0.01f)
            {
                Zoom(scroll * 0.12f);
            }
        }

        private void Pan(Vector2 screenDelta)
        {
            var scale = _distance * panSensitivity;
            var right = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
            var forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            _focus -= right * (screenDelta.x * scale);
            _focus -= forward * (screenDelta.y * scale);
        }

        private void Zoom(float delta)
        {
            _distance = Mathf.Clamp(
                _distance - delta,
                BootstrapLayout.CityExtent * 0.16f,
                BootstrapLayout.CityExtent * 1.65f);
        }

        private void ApplyPose()
        {
            var rotation = Quaternion.Euler(_pitch, _yaw, 0f);
            transform.rotation = rotation;
            transform.position = _focus - rotation * Vector3.forward * _distance;
        }
    }
}
