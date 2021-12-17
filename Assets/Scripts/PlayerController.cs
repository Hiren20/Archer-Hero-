using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	public GameObject arrowPrefab;
	public Transform arrowPosition;
	public float force = 15f;

	bool hasArrow = true;

	public GameObject[] wayPoints;

	public int currentWaypoint = 0;

	NavMeshAgent agent;

	public GameObject[] Enemies;
	public int hitArrow = 0;

    private void Start()
    {
		agent = GetComponent<NavMeshAgent>();

		agent.SetDestination(wayPoints[0].transform.position);
		//agent.updateRotation = true;
	}

    void Update()
	{
		if (Input.GetMouseButtonDown(0) )
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit) && hasArrow)
            {
				GameObject tempArrow = (GameObject)GameObject.Instantiate(arrowPrefab, arrowPosition.position, Quaternion.LookRotation(ray.direction));
				tempArrow.GetComponent<Rigidbody>().AddForce((hit.point - tempArrow.transform.position).normalized * force, ForceMode.VelocityChange);

				arrowPosition.rotation = Quaternion.LookRotation(ray.direction);

				hasArrow = false;
				HideArrow();
			}

			

			
			

		}

		if(hitArrow >= 3)
        {
			if(currentWaypoint == 0)
            {
				Enemies[0].GetComponent<Enemy>().Dead();
				hitArrow = 0;
				currentWaypoint += 1;
				agent.SetDestination(wayPoints[currentWaypoint].transform.position);
            }
			else  if (currentWaypoint == 1)
			{
				Enemies[1].GetComponent<Enemy>().Dead();
				hitArrow = 0;
				
			}
		}
	}

	void HideArrow()
    {
		arrowPosition.gameObject.SetActive(false);
		StartCoroutine(AutoRefill());
    }

	IEnumerator AutoRefill()
    {
		yield return new WaitForSeconds(0.5f);
		arrowPosition.gameObject.SetActive(true);
		hasArrow = true;
    }

	public void WaypointReached()
    {
		if(currentWaypoint == 0)
        {
			Enemies[0].GetComponent<Enemy>().ActivateEnemy();
        }

		if(currentWaypoint == 1)
        {
			Enemies[1].GetComponent<Enemy>().ActivateEnemy();
		}
    }
}
