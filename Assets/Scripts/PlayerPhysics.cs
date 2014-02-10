using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

  public LayerMask collisionMask;

	private BoxCollider boxCollider;
	private Vector3 s;
	private Vector3 c;

  private float skin = .005f;

  [HideInInspector]
  public bool grounded;

  [HideInInspector]
  public bool movementStopped;

  Ray ray;
  RaycastHit hit;

	void Start() {
    boxCollider = GetComponent<BoxCollider> ();
		s = boxCollider.size;
		c = boxCollider.center;
	}

	public void Move (Vector2 moveAmount) {
    float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;

		Vector2 p = transform.position;

    grounded = false;

		for (int i=0; i<3; ++i) {
      float direction = Mathf.Sign(deltaY);
      float x = (p.x + c.x - (s.x / 2)) + ((s.x / 2) * i);
      float y = p.y + c.y + ((s.y / 2) * direction);

      ray = new Ray(new Vector2(x, y), new Vector2(0, direction));

      if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask)) {
        float distance = Vector3.Distance(ray.origin, hit.point);

        if (distance > skin) {
          deltaY = (distance * direction) - (skin * direction);
        } else {
          deltaY = 0;
        }
        grounded = true;
        break;
      }
		}

    movementStopped = false;
    for (int i=0; i<3; ++i) {
      float direction = Mathf.Sign(deltaX);
      float x = p.x + c.x + ((s.x / 2) * direction);
      float y = (p.y + c.y - (s.y / 2)) + ((s.y / 2) * i);;
      
      ray = new Ray(new Vector2(x, y), new Vector2(direction, 0));
      
      if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask)) {
        float distance = Vector3.Distance(ray.origin, hit.point);
        
        if (distance > skin) {
          deltaX = (distance * direction) - (skin * direction);
        } else {
          deltaX = 0;
        }

        movementStopped = true;
        break;
      }
    }

    if (!grounded && !movementStopped) {
      Vector3 playerDirection = new Vector3 (deltaX, deltaY);
      Vector3 rayOrigin = new Vector3 (p.x + c.x + ((s.x / 2) * Mathf.Sign(deltaX)), p.y + c.y + ((s.y / 2) * Mathf.Sign(deltaY)));
      ray = new Ray (rayOrigin, playerDirection.normalized);
      
      if (Physics.Raycast (ray, out hit, Mathf.Sqrt (deltaX * deltaX + deltaY * deltaY) + skin, collisionMask)) {
        grounded = true;
        deltaY = 0;
      }
    }

    Vector2 finalTransform = new Vector2(deltaX, deltaY);

    transform.Translate (finalTransform);
	}
}
