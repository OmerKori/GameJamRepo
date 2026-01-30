using UnityEngine;

public class MasksManager : MonoBehaviour
{
    [SerializeField] GameObject masksParent;
    [SerializeField] GameObject masksUIParent;
    [SerializeField] RectTransform maskUIHighlight;
    int currentMaskIndex = 0;
    int totalMasks = 0;
    private void Start()
    {
        totalMasks = masksParent.transform.childCount;
    }
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll >= 0.1f)
            ChangeMask(1);
        else if (scroll <= -0.1f)
            ChangeMask(-1);
    }

    void ChangeMask(int i)
    {
        currentMaskIndex += i;
        if(currentMaskIndex > totalMasks)
            currentMaskIndex = 0;
        else if (currentMaskIndex < 0)
            currentMaskIndex = totalMasks;
        maskUIHighlight.SetParent(masksUIParent.transform.GetChild(currentMaskIndex), false);
        maskUIHighlight.anchoredPosition = Vector3.zero;
    }
}
