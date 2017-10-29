using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
	[RequireComponent (typeof(ThirdPersonCharacter))]
	public class AICharacterControl : MonoBehaviour
	{
		public UnityEngine.AI.NavMeshAgent agent { get; private set; }
		// the navmesh agent required for the path finding
		public ThirdPersonCharacter character { get; private set; }
		// the character we are controlling

		float x, z;

		private void Start ()
		{
			// get the components on the object we need ( should not be null due to require component so no need to check )
			agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent> ();
			character = GetComponent<ThirdPersonCharacter> ();

			agent.updateRotation = false;
			agent.updatePosition = true;

			StartCoroutine (RepostionWithDelay ());	
		}


		private void Update ()
		{
			
			agent.SetDestination (new Vector3 (x, 0.0f, z));
			

			if (agent.remainingDistance > agent.stoppingDistance)
				character.Move (agent.desiredVelocity, false, false);
			else
				character.Move (Vector3.zero, false, false);
		}

		IEnumerator RepostionWithDelay ()
		{
			while (true) {
				SetRandomPosition ();
				yield return new WaitForSeconds (5);
			}
		}

		void SetRandomPosition ()
		{
			x = Random.Range (10.0f, -10.0f);
			z = Random.Range (10.0f, -10.0f);
			//transform.position = new Vector3 (x, 0.0f, z);
		}

	}
}