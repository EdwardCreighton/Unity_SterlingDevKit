using UnityEngine;

namespace SterlingAssets
{
    public class OrbitCameraController : CameraControllerBase
    {
        #region Fields

        [SerializeField] private float distance = 2f;
        [Space]
        [SerializeField] private float pitchHigh = 10f;
        [SerializeField] private float pitchLow = -60f;
        [Space]
        [SerializeField] private float pitchSensitivity = 1f;
        [SerializeField] private float yawSensitivity = 1f;

        private Vector2 newAngles;

        #endregion

        public override void OnAwake()
        {
            cameraData = SterlingCamera.Instance.CameraData;
        }

        public override void OnEditorSetup()
        {
            cameraData = SterlingCamera.Instance.CameraData;

            cameraData.followOffset = Quaternion.Euler(cameraData.followOffsetEulerAngles.x, cameraData.followOffsetEulerAngles.y, 0f) * Vector3.forward;

            Transform cameraTransform = cameraData.camera.transform;
            cameraTransform.position = cameraData.followTarget + cameraData.followOffset * distance;
            cameraData.lookAtTarget = cameraData.followTarget;
            cameraTransform.LookAt(cameraData.lookAtTarget);
        }

        public override void OnUpdate()
        {

        }

        public override void OnLateUpdate()
        {
            Transform cameraTransform = cameraData.camera.transform;

            ComputeRotationAngles();

            ComputeDirectionVector();

            cameraTransform.position = cameraData.followTarget + cameraData.followOffset * distance;

            cameraData.lookAtTarget = cameraData.followTarget;
            cameraTransform.LookAt(cameraData.lookAtTarget);
        }

        private void ComputeRotationAngles()
        {
            newAngles = cameraData.followOffsetEulerAngles;
            newAngles.x += cameraData.inputDelta.y * Time.deltaTime * pitchSensitivity;
            newAngles.y += cameraData.inputDelta.x * Time.deltaTime * yawSensitivity;

            newAngles.x = Mathf.Clamp(newAngles.x, pitchLow, pitchHigh);

            if (newAngles.y > 360f)
            {
                newAngles.y -= 360f;
            }
            else if (newAngles.y < 0f)
            {
                newAngles.y += 360f;
            }

            cameraData.followOffsetEulerAngles = newAngles;
        }

        private void ComputeDirectionVector()
        {
            cameraData.followOffset = Quaternion.Euler(cameraData.followOffsetEulerAngles.x, cameraData.followOffsetEulerAngles.y, 0f) * Vector3.forward;
            cameraData.followOffset.Normalize();
        }
    }
}