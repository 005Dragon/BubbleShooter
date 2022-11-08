using UnityEngine;

namespace Code.Services
{
    public class CameraService
    {
        public Camera Camera { get; }

        public CameraService(Camera camera)
        {
            Camera = camera;
        }
    }
}