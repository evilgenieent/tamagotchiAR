/*==============================================================================
Copyright (c) 2010-2013 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class Hidder : MonoBehaviour,
ITrackableEventHandler
{

	public GameObject hidder;

	#region PRIVATE_MEMBER_VARIABLES
	
	private TrackableBehaviour mTrackableBehaviour;
	
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	
	
	#region UNTIY_MONOBEHAVIOUR_METHODS
	
	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		hidder.renderer.enabled = false;
		Debug.Log("Hidding at startup");
	}
	
	#endregion // UNTIY_MONOBEHAVIOUR_METHODS
	
	
	
	#region PUBLIC_METHODS
	
	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	
	#endregion // PUBLIC_METHODS
	
	
	
	#region PRIVATE_METHODS
	
	
	private void OnTrackingFound()
	{

		Renderer[] rendererComponents = hidder.GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = hidder.GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }
		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
	}
	
	
	private void OnTrackingLost()
	{
		hidder.transform.position = new Vector3(100.0f, 100.0f, 100.0f);
		Renderer[] rendererComponents = hidder.GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = hidder.GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }
		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
	}
	
	#endregion // PRIVATE_METHODS
}
