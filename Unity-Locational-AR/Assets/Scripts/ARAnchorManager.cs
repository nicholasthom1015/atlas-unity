using UnityEngine;
using System.Collections.Generic;

public class GPSAnchorManager : MonoBehaviour
{
    private List<Vector3> gpsAnchors = new List<Vector3>();
    private const float checkDistance = 50.0f; // Distance to check for nearby anchors

    private void Start()
    {
        LoadGPSAnchors();
    }

    public void SaveAnchor(Vector3 gpsPosition)
    {
        // Convert GPS position to Unity position
        Vector3 unityPosition = ConvertGPSToUnity(gpsPosition);
        
        // Save the position
        gpsAnchors.Add(gpsPosition);
        PlayerPrefs.SetString("GPSAnchor_" + (gpsAnchors.Count - 1), gpsPosition.x + "," + gpsPosition.y + "," + gpsPosition.z);
        //PlayerPrefs.SetString("GPSAnchor_" + (gpsAnchors.Count - 1), gpsPosition.ToString());
        PlayerPrefs.SetInt("GPSAnchorCount", gpsAnchors.Count);
        PlayerPrefs.Save();
        Debug.Log("Saving anchor: " + gpsPosition);
        
        // Optionally, instantiate the object at the converted position
        // Instantiate(yourPrefab, unityPosition, Quaternion.identity);
    }

    private void LoadGPSAnchors()
    {
        int count = PlayerPrefs.GetInt("GPSAnchorCount", 0);
        for (int i = 0; i < count; i++)
        {
            string positionString = PlayerPrefs.GetString("GPSAnchor_" + i);
            if (!string.IsNullOrEmpty(positionString))
            {
                Vector3 position = StringToVector3(positionString);
                // Instantiate your anchored object here
                Vector3 unityPosition = ConvertGPSToUnity(position);
                // Instantiate(yourPrefab, unityPosition, Quaternion.identity);
                gpsAnchors.Add(position);
            }
        }
    }

    private Vector3 StringToVector3(string sVector)
    {
        try
        {
            string[] sArray = sVector.Split(',');
            return new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));
        }
            catch (System.Exception e)
        {
            Debug.LogError("Error parsing Vector3 from string: " + e.Message);
            return Vector3.zero; // or handle it in another way
        }
    }

    // private Vector3 StringToVector3(string sVector)
    // {
    //     string[] sArray = sVector.Replace("(", "").Replace(")", "").Split(',');
    //     return new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));
    // }

    private Vector3 ConvertGPSToUnity(Vector3 gpsPosition)
    {
        // Implement GPS to Unity conversion logic here
        // Use Niantic Lightship SDK for actual conversion
        // For now, just return a placeholder position
        return new Vector3(gpsPosition.x, 0, gpsPosition.y); // Placeholder conversion
    }

    public List<Vector3> GetNearbyAnchors(Vector3 currentGPSPosition)
    {
        List<Vector3> nearbyAnchors = new List<Vector3>();
        
        foreach (var anchor in gpsAnchors)
        {
            if (Vector3.Distance(anchor, currentGPSPosition) <= checkDistance)
            {
                nearbyAnchors.Add(anchor);
            }
        }

        return nearbyAnchors;
    }
}
