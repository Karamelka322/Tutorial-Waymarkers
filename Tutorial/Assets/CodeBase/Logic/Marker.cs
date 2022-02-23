using CodeBase.Extension;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] 
    private Camera _camera;
    
    [SerializeField] 
    private Transform _marker;

    [SerializeField] 
    private Transform _target;

    [Space, SerializeField] 
    private float _offset = 100;
    
    private Vector3 _screenCenter;
    private Vector3 _screenBorder;

    private void Awake()
    {
        //float height = Screen.width * _camera.aspect;
        //float width = Screen.height / _camera.aspect;

        _screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        _screenBorder = _screenCenter - new Vector3(_offset, _offset, 0f);
    }

    private void Update()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(_target.position);

        if (!_camera.IsVisiblePoint(screenPoint, _offset))
        {
            if (screenPoint.z < 0)
            {
                screenPoint.x = Screen.width - screenPoint.x;
                screenPoint.y = Screen.width - screenPoint.y;
            }

            screenPoint -= _screenCenter;

            float angle = Mathf.Atan2(screenPoint.y, screenPoint.x);
            angle -= 90 * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(-angle);
            float cotangent = cos / sin;

            float borderY = cos > 0 ? _screenBorder.y : -_screenBorder.y;
            
            screenPoint.Set(borderY / cotangent, borderY, 0);

            if (screenPoint.x > _screenBorder.x)
            {
                screenPoint.Set(_screenBorder.x, _screenBorder.x * cotangent, 0);
            }
            else if(screenPoint.x < -_screenBorder.x)
            {
                screenPoint.Set(-_screenBorder.x, -_screenBorder.x * cotangent, 0);
            }

            screenPoint += _screenCenter;
        }
        
        _marker.position = screenPoint;
    }
}