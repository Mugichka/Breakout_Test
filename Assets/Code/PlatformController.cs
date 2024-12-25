using Breakout_Test.Assets.Code.Sounds;
using UnityEngine;

public class PlatformController : MonoBehaviour, ISoundable
{
    public float speed = 10.0f;
    public float boundary = 7.5f;
    public string sound="WallHit";

    public string GetSound()
    {
        return sound;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(moveInput * speed * Time.deltaTime, 0, 0);
        
        newPosition.x = Mathf.Clamp(newPosition.x, -boundary, boundary);
        transform.position = newPosition;
        
    }
}
