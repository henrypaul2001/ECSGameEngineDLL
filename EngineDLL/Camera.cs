using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL_Game
{
    class Camera
    {
        public Matrix4 view, projection;
        public Vector3 cameraPosition, cameraDirection, cameraUp, lastPosition;
        private Vector3 targetPosition;
        public float collisionRadius = 1f;

        public Camera()
        {
            cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
            lastPosition = cameraPosition;
            cameraDirection = new Vector3(0.0f, 0.0f, -1.0f);
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            UpdateView();
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 1.0f, 0.1f, 100f);
        }

        public Camera(Vector3 cameraPos, Vector3 targetPos, float ratio, float near, float far)
        {
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            lastPosition = cameraPos;
            cameraPosition = cameraPos;
            cameraDirection = targetPos-cameraPos;
            cameraDirection.Normalize();
            UpdateView();
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), ratio, near, far);
        }

        public void MoveForward(float move)
        {
            lastPosition = cameraPosition;
            cameraPosition += move*cameraDirection;
            UpdateView();
        }

        public void Translate(Vector3 move)
        {
            cameraPosition += move;
            UpdateView();
        }

        public void RotateY(float angle)
        {
            cameraDirection = Matrix3.CreateRotationY(angle) * cameraDirection;
            UpdateView();
        }

        public void UpdateView()
        {
            targetPosition = cameraPosition + cameraDirection;
            view = Matrix4.LookAt(cameraPosition, targetPosition, cameraUp);
        }

        public void MoveToLastPos()
        {
            cameraPosition = lastPosition;
        }

        public void SetCamera(Vector3 camPos, Vector3 lookTarget)
        {
            cameraPosition = camPos;
            cameraDirection = lookTarget - camPos;
            cameraDirection.Normalize();
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            UpdateView();
        }
    }
}
