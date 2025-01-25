using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace TMPro.Examples
{
    public class CameraController : MonoBehaviour
    {
        public enum CameraModes { Follow, Isometric, Free }

        private Transform cameraTransform;
        private Transform dummyTarget;

        public Transform CameraTarget;

        public float FollowDistance = 30.0f;
        public float MaxFollowDistance = 100.0f;
        public float MinFollowDistance = 2.0f;

        public float ElevationAngle = 30.0f;
        public float MaxElevationAngle = 85.0f;
        public float MinElevationAngle = 0f;

        public float OrbitalAngle = 0f;

        public CameraModes CameraMode = CameraModes.Follow;

        public bool MovementSmoothing = true;
        public bool RotationSmoothing = false;
        private bool previousSmoothing;

        public float MovementSmoothingValue = 25f;
        public float RotationSmoothingValue = 5.0f;

        public float MoveSensitivity = 2.0f;

        private Vector3 currentVelocity = Vector3.zero;
        private Vector3 desiredPosition;
        private Vector2 lookInput;
        private float zoomInput;

        private PlayerInput playerInput;
        private InputAction lookAction;
        private InputAction zoomAction;

        void Awake()
        {
            if (QualitySettings.vSyncCount > 0)
                Application.targetFrameRate = 60;
            else
                Application.targetFrameRate = -1;

            cameraTransform = transform;
            previousSmoothing = MovementSmoothing;

            playerInput = GetComponent<PlayerInput>();

            // Set up Input Actions
            lookAction = playerInput.actions["Look"];
            zoomAction = playerInput.actions["Zoom"];
        }

        void Start()
        {
            if (CameraTarget == null)
            {
                dummyTarget = new GameObject("Camera Target").transform;
                CameraTarget = dummyTarget;
            }
        }

        void LateUpdate()
        {
            GetPlayerInput();

            if (CameraTarget != null)
            {
                if (CameraMode == CameraModes.Isometric)
                {
                    desiredPosition = CameraTarget.position + Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * new Vector3(0, 0, -FollowDistance);
                }
                else if (CameraMode == CameraModes.Follow)
                {
                    desiredPosition = CameraTarget.position + CameraTarget.TransformDirection(Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * (new Vector3(0, 0, -FollowDistance)));
                }

                if (MovementSmoothing)
                {
                    cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref currentVelocity, MovementSmoothingValue * Time.fixedDeltaTime);
                }
                else
                {
                    cameraTransform.position = desiredPosition;
                }

                if (RotationSmoothing)
                {
                    cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.LookRotation(CameraTarget.position - cameraTransform.position), RotationSmoothingValue * Time.deltaTime);
                }
                else
                {
                    cameraTransform.LookAt(CameraTarget);
                }
            }
        }

        void GetPlayerInput()
        {
            lookInput = lookAction.ReadValue<Vector2>();
            zoomInput = zoomAction.ReadValue<float>();

            // Handle Look Input
            if (lookInput.sqrMagnitude > 0.01f)
            {
                ElevationAngle -= lookInput.y * MoveSensitivity * Time.deltaTime;
                ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);

                OrbitalAngle += lookInput.x * MoveSensitivity * Time.deltaTime;
                OrbitalAngle = OrbitalAngle % 360;
            }

            // Handle Zoom Input
            if (Mathf.Abs(zoomInput) > 0.01f)
            {
                FollowDistance -= zoomInput * 5.0f * Time.deltaTime;
                FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
            }
        }
    }
}
