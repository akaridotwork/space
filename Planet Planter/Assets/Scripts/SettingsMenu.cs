using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public Toggle fsTog, vsyncTog;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    private int selectedResolution;

    public TMP_Text resolutionText;

    public AudioMixer Mixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;

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

        VolumeHolder();
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume();
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

    public void SetVolume()
    {
        masterLabel.text = (masterSlider.value + 80).ToString() + "%";
        musicLabel.text = (musicSlider.value + 80).ToString() + "%";
        sfxLabel.text = (sfxSlider.value + 80).ToString() + "%";

        Mixer.SetFloat("MasterVol", masterSlider.value);
        Mixer.SetFloat("MusicVol", musicSlider.value);
        Mixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);

    }

    public void VolumeHolder()
    {
        float volume = 0f;

        Mixer.GetFloat("MasterVol", out volume);
        masterSlider.value = volume;
        masterLabel.text = (masterSlider.value + 80).ToString() + "%";


        Mixer.GetFloat("MusicVol", out volume);
        musicSlider.value = volume;
        musicLabel.text = (musicSlider.value + 80).ToString() + "%";

        Mixer.GetFloat("SFXVol", out volume);
        sfxSlider.value = volume;
        sfxLabel.text = (sfxSlider.value + 80).ToString() + "%";
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int horizontal, vertical;
}
