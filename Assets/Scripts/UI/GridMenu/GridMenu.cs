using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMenu : MonoBehaviour
{
    [UnityEngine.Range(0f, 1f)]
    [SerializeField] private float posittionXViewport = 0.25f;
    [SerializeField] private bool opened = false;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float offSetMaxX = 100;

    [Header("UI Element")]
    [SerializeField] private RectTransform GridMenuPanel;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuContent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private List<GameObject> catalog;

    public void OpenCloseGridMenu()
    {
        opened = !opened;
        menu.SetActive(opened);
        StartCoroutine(OpenCloseAnimation());
    }

    public IEnumerator OpenCloseAnimation()
    {
        float xResolutionScreen = Screen.width;
        float xTarget = 0f;
        if (opened)
        {
            xTarget = xResolutionScreen * (1 - posittionXViewport);
        }
        else
        {
            xTarget = xResolutionScreen - offSetMaxX;
        }

        float timer = 0f;
        Vector2 offsetMax = GridMenuPanel.offsetMax;
        float valueBase = - offsetMax.x;
        while (timer <= animationDuration)
        {
            float progress = Mathf.Lerp(valueBase, xTarget, timer / animationDuration);
            offsetMax.x = -progress;
            GridMenuPanel.offsetMax = offsetMax;
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void MenuContent()
    {
        foreach (GameObject item in catalog)
        {
            //Display here
        }
    }
}
