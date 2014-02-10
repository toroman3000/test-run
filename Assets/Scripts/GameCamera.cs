using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

  private Transform target;
  private float trackSpeed = 10;

	public void SetTarget(Transform t) {
    target = t;
  }

  void LateUpdate() {
    if (target) {
      float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
      float y = IncrementTowards(transform.position.y, target.position.y, trackSpeed);
      transform.position = new Vector3(x, y, transform.position.z);
    }
  }

  
  private float IncrementTowards (float current, float target, float acceleration) {
    if (current == target) {
      return current;
    } else {
      float direction = Mathf.Sign (target - current);
      current += acceleration * Time.deltaTime * direction;
      return (direction == Mathf.Sign (target - current) ? current : target);
    }
  }

}
