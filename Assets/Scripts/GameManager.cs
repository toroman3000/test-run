using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

  public GameObject player;

  private GameCamera cam;

	// Use this for initialization
	void Start () {
    cam = GetComponent<GameCamera> ();
    SpawnPlayer ();
  }
	
	// Update is called once per frame
	void Update () {
	
	}

  private void SpawnPlayer() {
    cam.SetTarget((Instantiate (player, Vector3.zero, Quaternion.identity) as GameObject).transform);
  }
}
