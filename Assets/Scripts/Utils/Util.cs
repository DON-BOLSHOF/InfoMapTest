using Cinemachine;
using UnityEngine;

namespace Utils
{
    public static class Util
    {
        public static Vector3 ToWorldPosition(Camera camera,Vector3 position)
        {
            position.z = 0;
            return camera.ScreenToWorldPoint(position);
        }
    }
}