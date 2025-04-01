using System.Collections;
using UnityEngine;

public class SmoothCameraTransition : MonoBehaviour
{
    public Camera cam1; 
    public Camera cam2; 
    private bool isSwitching = false;
    private float transitionTime = 1.5f; 

    public void SwitchCamera()
    {
        StartCoroutine(SwitchCamera(cam1, cam2, transitionTime));
    }

    IEnumerator SwitchCamera(Camera fromCam, Camera toCam, float duration)
    {
        isSwitching = true;
        float elapsed = 0;
        Vector3 startPos = fromCam.transform.position;
        Quaternion startRot = fromCam.transform.rotation;
        Vector3 endPos = toCam.transform.position;
        Quaternion endRot = toCam.transform.rotation;

        toCam.gameObject.SetActive(true);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            fromCam.transform.position = Vector3.Lerp(startPos, endPos, t);
            fromCam.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fromCam.gameObject.SetActive(false);
        isSwitching = false;
    }
}
