//-----------------------------------------------------------------------
// <copyright file="TangoFloorFindingUIController.cs" company="Google">
//
// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tango;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tango floor finding user interface controller. 
/// 
/// Place a marker at the y position of the found floor and allow user to recalculate.
/// </summary>
public class TangoFloorFindingUIController_L : MonoBehaviour 
{
    /// <summary>
    /// The marker for the found floor.
    /// </summary>
    public GameObject[] m_marker;
	int whichMarker = 0;

    /// <summary>
    /// The scene's Tango application.
    /// </summary>
    private TangoApplication m_tangoApplication;

    /// <summary>
    /// Reference to the TangoPointCloud in the scene. 
    /// 
    /// FindFloor is called in TangoPointCloud, and the TangoPointCloudFloor automatically reflects 
    /// changes in the found floor.
    /// </summary>
    private TangoPointCloud m_pointCloud;

    /// <summary>
    /// Reference to the TangoPointCloudFloor in the scene.
    /// </summary>
    private TangoPointCloudFloor m_pointCloudFloor;

    /// <summary>
    /// If <c>true</c>, floor finding is in progress.
    /// </summary>
    private bool m_findingFloor = false;

	//
	List<GameObject> markerGroup = new List<GameObject>();
	public Dictionary<int, GameObject> markerDictionary = new Dictionary<int, GameObject>();
	bool[] markerDrop;
	bool showMarkerMenu = false;
	bool showConfirm = false;
	int markerCount = 0;

	public GameObject[] showOnFinishPlacing;

	float buttonWidth = 200f;
	float hSliderValue = 0.0f;
	int theLatestButton = 0;

	public CopyPosition copyPosition; 

	public GUISkin mySkin;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
//        m_marker.SetActive(false);
        m_pointCloud = FindObjectOfType<TangoPointCloud>();
        m_pointCloudFloor = FindObjectOfType<TangoPointCloudFloor>();
        m_tangoApplication = FindObjectOfType<TangoApplication>();

		markerDrop = new bool[m_marker.Length];
		for (int i = 0; i < m_marker.Length; i++)
		{
			markerDrop [i] = false;
		}
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!m_findingFloor)
        {
            return;
        }

        // If the point cloud floor has found a new floor, place the marker at the found y position.
        if (m_pointCloudFloor.m_floorFound && m_pointCloud.m_floorFound)
        {
            m_findingFloor = false;

			//DropMarker();
			showMarkerMenu = true;

			AndroidHelper.ShowAndroidToastMessage(string.Format("Floor found. Unity world height = {0}", m_pointCloudFloor.transform.position.y.ToString()));
        }
    }

	void PlaceMarker(int whichMarker)
	{
		GameObject markerrr = m_marker [whichMarker];

		Vector3 target;
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f)), out hitInfo))
		{
			// Limit distance of the marker position from the camera to the camera's far clip plane. This makes sure that the marker
			// is visible on screen when the floor is found.
			Vector3 cameraBase = new Vector3(Camera.main.transform.position.x, hitInfo.point.y, Camera.main.transform.position.z);
			target = cameraBase + Vector3.ClampMagnitude(hitInfo.point - cameraBase, Camera.main.farClipPlane * 0.9f);
		}
		else
		{
			// If no raycast hit, place marker in the camera's forward direction.
			Vector3 dir = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);
			target = dir.normalized * (Camera.main.farClipPlane * 0.9f);
			target.y = m_pointCloudFloor.transform.position.y;
		}
		markerrr.transform.position = target;

		if (!markerDrop [whichMarker]) {
			markerDictionary.Add (whichMarker, markerrr);
			markerCount++;
			markerDrop [whichMarker] = true;
			Debug.Log ("Place Marker " + whichMarker);
		} else {
			Debug.Log ("Replace Marker " + whichMarker);
		}

		theLatestButton = whichMarker;

		if (markerCount == m_marker.Length) {
//			this.gameObject.SetActive (false);
//			ShowOnFinish ();
			// Show confirm option
			showConfirm = true;
		}
	}

	void DropMarker()
	{
		GameObject markerrr = m_marker [whichMarker];// Instantiate(m_marker[whichMarker], new Vector3(), Quaternion.identity) as GameObject;

		Vector3 target;
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f)), out hitInfo))
		{
			// Limit distance of the marker position from the camera to the camera's far clip plane. This makes sure that the marker
			// is visible on screen when the floor is found.
			Vector3 cameraBase = new Vector3(Camera.main.transform.position.x, hitInfo.point.y, Camera.main.transform.position.z);
			target = cameraBase + Vector3.ClampMagnitude(hitInfo.point - cameraBase, Camera.main.farClipPlane * 0.9f);
		}
		else
		{
			// If no raycast hit, place marker in the camera's forward direction.
			Vector3 dir = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);
			target = dir.normalized * (Camera.main.farClipPlane * 0.9f);
			target.y = m_pointCloudFloor.transform.position.y;
		}
		markerrr.transform.position = target;
		markerGroup.Add (markerrr);
//		markerDrop = true;

		Debug.Log (whichMarker);
		whichMarker++;
		if (whichMarker == m_marker.Length) {
			this.gameObject.SetActive (false);
			ShowOnFinish ();
		}
	}

	void ShowOnFinish(){
		for (int i = 0; i < showOnFinishPlacing.Length; i++) {
			showOnFinishPlacing [i].SetActive (true);
		}
	}
    
    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// </summary>
    public void OnGUI()
    {
		GUI.skin = mySkin;
//        GUI.color = Color.white;

		if (!m_findingFloor && !showMarkerMenu)
        {
			if (GUI.Button(new Rect(Screen.width - 220, 20, 200, 80), "<size=30>Find Floor</size>"))
            {
                if (m_pointCloud == null)
                {
                    Debug.LogError("TangoPointCloud required to find floor.");
                    return;
                }

                m_findingFloor = true;
                m_tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.MAXIMUM);
                m_pointCloud.FindFloor();
            }
		}
		else if (showMarkerMenu)
		{
			if (GUI.Button(new Rect(Screen.width - buttonWidth*5, 20, buttonWidth, 80), "<size=30>Marker 1</size>"))
			{
				PlaceMarker(0);
			}
			if (GUI.Button(new Rect(Screen.width - buttonWidth*4, 20, buttonWidth, 80), "<size=30>Marker 2</size>"))
			{
				PlaceMarker(1);
			}
			if (GUI.Button(new Rect(Screen.width - buttonWidth*3, 20, buttonWidth, 80), "<size=30>Marker 3</size>"))
			{
				PlaceMarker(2);
			}
			if (GUI.Button(new Rect(Screen.width - buttonWidth*2, 20, buttonWidth, 80), "<size=30>Marker 4</size>"))
			{
				PlaceMarker(3);
			}
			if (GUI.Button(new Rect(Screen.width - buttonWidth, 20, buttonWidth, 80), "<size=30>Marker 5</size>"))
			{
				PlaceMarker(4);
			}

			if (markerCount > 0) {
				hSliderValue = GUI.HorizontalSlider(new Rect(25, 150, Screen.width/2, Screen.height/6), hSliderValue, 0.0F, 360.0F);
				Vector3 newAngle = new Vector3 (0, hSliderValue, 0);

				markerDictionary[theLatestButton].transform.eulerAngles = newAngle;

				copyPosition.UpdateTargetTransform (theLatestButton, 2);
			}

			// Confirm Button
			if (showConfirm)
			{
				if (GUI.Button(new Rect(Screen.width - buttonWidth, 100, buttonWidth, 80), "<size=30>OK!</size>"))
				{
					this.gameObject.SetActive (false);
					showMarkerMenu = false;
					ShowOnFinish ();
				}
			}
		}
        else
        {
            GUI.Label(new Rect(0, Screen.height - 50, Screen.width, 50), "<size=30>Searching for floor position. Make sure the floor is visible.</size>");
        }
    }

    /// <summary>
    /// Application onPause / onResume callback.
    /// </summary>
    /// <param name="pauseStatus"><c>true</c> if the application about to pause, otherwise <c>false</c>.</param>
    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // When application is backgrounded, we reload the level because the Tango Service is disconected. All
            // learned area and placed marker should be discarded as they are not saved.
            #pragma warning disable 618
            Application.LoadLevel(Application.loadedLevel);
            #pragma warning restore 618
        }
    }
}