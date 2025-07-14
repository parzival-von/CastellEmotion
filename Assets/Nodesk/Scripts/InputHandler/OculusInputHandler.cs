using System.Collections;
using System.Collections.Generic;
using Nodesk.Scripts.Core;
using UnityEngine;
using UnityEngine.XR;

namespace Nodesk.Scripts.InputHandler
{
    public class XRInputHandler : AbstractInputHandler
    {
        public enum NodeskHandType
        {
            Left,
            Right
        }

        [SerializeField] public NodeskHandType controllerHandType = NodeskHandType.Right;
        [SerializeField] public bool uiFollowController;

        private InputDevice _device;
        private XRNode _xrNode => controllerHandType == NodeskHandType.Left ? XRNode.LeftHand : XRNode.RightHand;

        private Vector2 _previousPosition = Vector2.zero;
        private readonly Queue<bool> _buttonBuffer = new Queue<bool>();

        void Start()
        {
            TryGetDevice();
        }

        void TryGetDevice()
        {
            var devices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(_xrNode, devices);
            if (devices.Count > 0)
            {
                _device = devices[0];
            }
        }

        void Update()
        {
            if (!_device.isValid) TryGetDevice();

            if (uiFollowController && _device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 pos) &&
                _device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rot))
            {
                NodeskManager.GetUiTransform().position = pos + (rot * Vector3.up) * 0.1f;
                NodeskManager.GetUiTransform().rotation = rot;
            }

            ProcessInput();
        }

        private void ProcessInput()
        {
            // Switch Keys (e.g. press primary button)
            if (_device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool stickClick) && stickClick)
            {
                InvokeTouchEvent(new InputData() { Action = InputAction.SwitchKeys });
            }

            // Select (trigger release)
            if (_device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && !triggerPressed)
            {
                InvokeTouchEvent(new InputData() { Action = InputAction.Select });
            }

            // Stick
            var stickData = ProcessStickInput();
            if (stickData != null) InvokeTouchEvent(stickData);
        }

        private InputData ProcessStickInput()
        {
            if (!_device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 vector)) return null;

            InputAction action = InputAction.Nothing;

            bool buttonDownInFrame = vector.magnitude > 0.001f;
            _buttonBuffer.Enqueue(buttonDownInFrame);
            if (_buttonBuffer.Count > 5) _buttonBuffer.Dequeue();

            bool buttonDown = false;
            foreach (var down in _buttonBuffer) if (down) buttonDown = true;

            if (buttonDown)
            {
                action = InputAction.StickMoving;
            }

            if (action != InputAction.Nothing)
            {
                Vector2 position = vector == Vector2.zero ? _previousPosition : vector;
                var lastPosition = _previousPosition;
                _previousPosition = position;

                return new InputData()
                {
                    Action = action,
                    Position = position,
                    LastPosition = lastPosition
                };
            }

            return null;
        }

        public override void ChangeVibrate()
        {
            SendHaptic(0.5f, 0.1f);
        }

        public override void SubmitVibrate()
        {
            SendHaptic(1f, 0.2f);
        }

        private void SendHaptic(float amplitude, float duration)
        {
            if (_device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
            {
                _device.SendHapticImpulse(0u, amplitude, duration);
            }
        }
    }
}
