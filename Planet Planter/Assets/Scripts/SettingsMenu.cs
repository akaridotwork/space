using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public Toggle fsTog, vsyncTog;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    private int selectedResolution;

    public TMP_Text resolutionText;

    // Start is called before the first frame update
    void Start()
    {
        fsTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        } else
        {
            vsyncTog.isOn = true;
        }

        bool currentRes = false;
        for(int i = 0;  i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                currentRes = true;
                selectedResolution = i;

                UpdateResolutionLabel();
            }
        }

        if(!currentRes)
        {
            ResolutionItem newRes = new ResolutionItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;

            UpdateResolutionLabel();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolutionLeft()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResolutionLabel();

    }

    public void ResolutionRight()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
        resolutionText.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();

    }

    public void ApplyGraphics()
    {
        // Screen.fullScreen = fsTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        } else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fsTog.isOn);
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int horizontal, vertical;
}
