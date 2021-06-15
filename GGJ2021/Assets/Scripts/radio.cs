using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// manager for the music system and sound effects.
/// </summary>
public class radio : MonoBehaviour {

    // /imagine if I made all of the variables have alliteration too. yikes.

    public AudioClip[] music;

    AudioSource radioDevice;

    [SerializeField] GameObject RadioPanel;

    [SerializeField] int currSong;
    [SerializeField] TextMeshProUGUI currentSongNameText;

    [Header("Audio Mixers")]
    public AudioMixer mixer;
    [SerializeField] AudioMixer beepMixer;
    [SerializeField] AudioMixer chatterMixer;
    [SerializeField] AudioMixer footstepMixer;

    [Header("Sound Sliders")]
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider detectorBeepSlider;
    [SerializeField] Slider peopleChatterSlider;
    [SerializeField] Slider footstepSoundSlider;

    // Start is called before the first frame update
    void Start() {

        radioDevice = GetComponent<AudioSource>();
        currSong = 0;

        radioDevice.clip = music[currSong];
        radioDevice.Play();

        currentSongNameText.text = music[currSong].name;


        musicVolumeSlider.value = .4f;
        detectorBeepSlider.value = .5f;
        peopleChatterSlider.value = .4f;

    }

    // Update is called once per frame
    void Update() {

        if (radioDevice.clip.name != music[currSong].name) {

            radioDevice.clip = music[currSong];
            radioDevice.Play();

            currentSongNameText.text = music[currSong].name;
        }

        if (!radioDevice.isPlaying) {
            RadioIncrease();
        }



    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            RadioPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            RadioPanel.SetActive(false);
        }

    }

    public void SetMusicVolume() {

        float sliderValue = musicVolumeSlider.value;

        mixer.SetFloat("VolumeMusic", Mathf.Log10(sliderValue) * 20);
    }

    public void SetDetectorBeepsVolume() {

        float sliderValue = detectorBeepSlider.value;

        beepMixer.SetFloat("BeepVolume", Mathf.Log10(sliderValue) * 20);

    }

    public void SetPeopleChatterVolume() {

        float sliderValue = peopleChatterSlider.value;

        chatterMixer.SetFloat("ChatterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void RadioIncrease() {
        currSong++;

        if (currSong >= music.Length)
            currSong = 0;

    }

    public void RadioDecrease() {
        currSong--;

        if (currSong == 0)
            currSong = music.Length - 1;
    }

}
