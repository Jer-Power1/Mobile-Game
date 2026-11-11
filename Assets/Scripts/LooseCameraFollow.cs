
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LooseCameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Free Movement Zone")]
    [Range(0.1f, 0.8f)]
    [SerializeField] private float deadZoneWidth = 0.35f;

    [Range(0.1f, 0.8f)]
    [SerializeField] private float deadZoneHeight = 0.35f;

    [Header("Smooth Follow")]
    [SerializeField] private float followSpeed = 5f;

    [Header("Arena Bounds")]
    [SerializeField]
    private Vector2 arenaMin =
        new Vector2(-100f, -100f);

    [SerializeField]
    private Vector2 arenaMax =
        new Vector2(100f, 100f);

    private Camera cam;
    private Vector3 desiredPosition;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
    }

    private void Start()
    {
        if (target == null)
            return;

        Vector3 startPosition = transform.position;

        startPosition.x = target.position.x;
        startPosition.y = target.position.y;

        desiredPosition = ClampCamera(startPosition);
        transform.position = desiredPosition;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float zoneHalfWidth =
            halfWidth * deadZoneWidth;

        float zoneHalfHeight =
            halfHeight * deadZoneHeight;

        Vector3 cameraPosition = transform.position;

        float differenceX =
            target.position.x - desiredPosition.x;

        float differenceY =
            target.position.y - desiredPosition.y;

        if (differenceX > zoneHalfWidth)
        {
            desiredPosition.x =
                target.position.x - zoneHalfWidth;
        }
        else if (differenceX < -zoneHalfWidth)
        {
            desiredPosition.x =
                target.position.x + zoneHalfWidth;
        }

        if (differenceY > zoneHalfHeight)
        {
            desiredPosition.y =
                target.position.y - zoneHalfHeight;
        }
        else if (differenceY < -zoneHalfHeight)
        {
            desiredPosition.y =
                target.position.y + zoneHalfHeight;
        }

        desiredPosition = ClampCamera(desiredPosition);

        float smoothing =
            1f - Mathf.Exp(-followSpeed * Time.deltaTime);

        cameraPosition = Vector3.Lerp(
            cameraPosition,
            desiredPosition,
            smoothing
        );

        transform.position = ClampCamera(cameraPosition);
    }

    private Vector3 ClampCamera(Vector3 position)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float minX = arenaMin.x + halfWidth;
        float maxX = arenaMax.x - halfWidth;

        float minY = arenaMin.y + halfHeight;
        float maxY = arenaMax.y - halfHeight;

        position.x = minX <= maxX
            ? Mathf.Clamp(position.x, minX, maxX)
            : (arenaMin.x + arenaMax.x) * 0.5f;

        position.y = minY <= maxY
            ? Mathf.Clamp(position.y, minY, maxY)
            : (arenaMin.y + arenaMax.y) * 0.5f;

        return position;
    }

    private void OnDrawGizmosSelected()
    {
        Camera cameraComponent = GetComponent<Camera>();

        if (cameraComponent == null)
            return;

        if (!cameraComponent.orthographic)
            return;

        float halfHeight = cameraComponent.orthographicSize;
        float halfWidth = halfHeight * cameraComponent.aspect;

        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube(
            transform.position,
            new Vector3(
                halfWidth * deadZoneWidth * 2f,
                halfHeight * deadZoneHeight * 2f,
                0f
            )
        );

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(
            new Vector3(
                (arenaMin.x + arenaMax.x) * 0.5f,
                (arenaMin.y + arenaMax.y) * 0.5f,
                0f
            ),
            new Vector3(
                arenaMax.x - arenaMin.x,
                arenaMax.y - arenaMin.y,
                0f
            )
        );
    }
}
