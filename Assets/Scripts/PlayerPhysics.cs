using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;

	//space between ground and player's collider
	private float skin = .005f;

	[HideInInspector]
	public bool grounded;

	Ray ray;
	RaycastHit hit;

	void Start(){
		collider = GetComponent<BoxCollider> ();
		s = collider.size;
		c = collider.center;
	}
	
	//Move the player by x, y
	public void Move(Vector2 amountToMove) {
		float deltaX = amountToMove.x;
		float deltaY = amountToMove.y;
		Vector2 p = transform.position; //keeps track of player position

		grounded = false;
		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 * i;//left, center, and right points of collider
			float y = p.y + c.y + s.y/2 * dir;//bottom of collider

			ray = new Ray(new Vector2(x, y), new Vector2(0, dir));

			if(Physics.Raycast (ray, out hit, Mathf.Abs(deltaY), collisionMask)) {
				//distance between player's position and ground
				float dst = Vector3.Distance (ray.origin, hit.point);

				//Stop players downward movement after coming within skin width of a collider
				if(dst > skin){
					deltaY = dst * dir + skin;
				}
				else{
					deltaY = 0;
				}
				grounded = true;
				break;

			}
		}
		Vector2 finalTransform = new Vector2(deltaX, deltaY);

		transform.Translate (finalTransform);


	}


}
