using UnityEngine;

namespace YourName.SurvivalShooter
{
    using Characters;
    public class CameraFollow : MonoBehaviour
    {
        public float SmoothSpeed = 5f;  //  카메라 이동할 때 스무딩할 속도
        public float ZoomScale = 2f;    //  오른쪽 클릭할 때 줌아웃 할 수 있는 거리
        private Vector3 m_Offset = Vector3.zero;       //  초기 카메라 위치 저장할 오프셋

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            var playerTrasnform = PlayerStatus.Get.PlayerTransform;
            if (playerTrasnform == null) return;
            else if (m_Offset == Vector3.zero)
            {
                m_Offset = transform.position - playerTrasnform.position;
            }

            Vector3 cameraPosition = playerTrasnform.position + m_Offset;

            if (Input.GetMouseButton(1))
            {
                //  ScreenToViewportPoint(Vector3) : ScreenPosition 를 viewportPosition 로 변경해준다
                //  viewPort 값은 0 에서 1로 끝나기 때문에 (0 ~ 1920 == 1. 해상도 최대 사이즈)
                //  기존 viewPort 값은 LeftBottom = 0, 1, RightTop = 1, 1
                //  가운데 값을 0으로 맞춰주기 위해 new Vector3(0.5f, 0.5f) 값을 빼준다
                //  -0.5, 0.5               0.5, 0.5
                //   --------------------------
                //   |                        |
                //   |                        |
                //   |          0, 0          |
                //   |                        |
                //   |                        |
                //   --------------------------
                //  -0.5, -0.5             0.5, -0.5
                var viewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f);
                //  x 축은 그대로 곱해도 되나 y 축은 가로 화면 스케일보다 크기가 적기 때문에
                //  추가로 Camera 의 종횡비를 곱해준다 (이렇게 까지 할 필요는 없으나 y 축도 x 축과 동일하게 확대하려면)
                viewPort = new Vector3(viewPort.x * ZoomScale, viewPort.y * ZoomScale * Camera.main.aspect);

                Vector3 zoomScale = new Vector3(
                    cameraPosition.x + viewPort.x,
                    m_Offset.y,
                    cameraPosition.z + viewPort.y);

                transform.position = Vector3.Lerp(
                    transform.position,
                    zoomScale,
                    SmoothSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    cameraPosition,
                    SmoothSpeed * Time.deltaTime);
            }
        }
    }
}
