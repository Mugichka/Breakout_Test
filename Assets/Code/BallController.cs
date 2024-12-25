using UnityEngine;

public sealed class BallController : MonoBehaviour
{
    public float speed = 10f;
    public Transform platform;
    private Vector3 direction;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    private float maxDistancePerFrame;
    private bool isMoving = false;
   [SerializeField] private BrickSpawner brickSpawner;
   [SerializeField] private VFXManager vFXManager;
   [SerializeField] private TrailRenderer trailRenderer;

    void Start()
    {
        direction = new Vector3(1, 1, 0).normalized;
        maxDistancePerFrame = speed * Time.fixedDeltaTime;
    }

    void Update()
    {
        if (!isMoving)
        {
            transform.position = platform.position + offset;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                trailRenderer.Clear();
                isMoving = true;
            }
        }
        else
        {
            MoveBall();
        }
    }

    void MoveBall()
    {
        trailRenderer.emitting=true;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, maxDistancePerFrame))
        {
            Debug.Log("hit");
            Vector3 normal = hit.normal;
            direction = Vector3.Reflect(direction, normal).normalized;

            if (hit.collider.TryGetComponent<Brick>(out Brick brick))
            {
                GameEvents.OnBrickHit?.Invoke();
                AudioManager.Instance.PlaySFX(brick.GetSound());
                vFXManager.PlayVFXBrickHit(hit.point);
                brickSpawner.DestroyBrick(brick.gameObject);
                //Destroy(brick.gameObject);
            }
            if(hit.collider.TryGetComponent<WallMarker>(out WallMarker wallMarker))
            {
                GameEvents.OnWallHit?.Invoke();
                AudioManager.Instance.PlaySFX(wallMarker.GetSound());
                vFXManager.PlayVFXBallWallHit(hit.point);
            }
            if(hit.collider.TryGetComponent<PlatformController>(out PlatformController platformController))
            {
                AudioManager.Instance.PlaySFX(platformController.GetSound());
                vFXManager.PlayVFXBallWallHit(hit.point);
            }
            if(hit.collider.TryGetComponent<BottomSide>(out BottomSide bottomSide))
            {
                GameEvents.OnBallDrop?.Invoke();
                AudioManager.Instance.PlaySFX(bottomSide.GetSound());
                //respawn
                RespawnBall();
            }

            transform.position = hit.point + normal * 0.01f;
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    void RespawnBall()
    {
        trailRenderer.emitting=false;
        vFXManager.PlayVFXBallDrop(transform.position);
        transform.position = platform.position + offset;
        direction = new Vector3(1, 1, 0).normalized;
        isMoving = false;
    }
}