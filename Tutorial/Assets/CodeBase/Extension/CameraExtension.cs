using UnityEngine;

namespace CodeBase.Extension
{
    public static class CameraExtension
    {
        public static bool IsVisiblePoint(this Camera camera, Vector3 screenPoint, float offset) =>
            screenPoint.z > 0 && screenPoint.x - offset > 0 && screenPoint.x + offset < camera.pixelWidth && screenPoint.y - offset > 0 && screenPoint.y + offset < camera.pixelHeight;
    }
}