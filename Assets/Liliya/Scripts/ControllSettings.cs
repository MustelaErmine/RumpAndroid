using UnityEngine;
using UnityEngine.UI;

public class ControllSettings : MonoBehaviour
{
    [SerializeField] Button audio_btn;
    public Sprite audio_on;
    public Sprite audio_off;
    bool _audio;
    bool Audio
    {
        set
        {
            _audio = value;
            if (_audio)
                audio_btn.GetComponent<Image>().sprite = audio_on;
            else
                audio_btn.GetComponent<Image>().sprite = audio_off;
            AudioListener.volume = _audio ? 1 : 0;
            oldSave.Audio = _audio;
            SaveLevel.SaveGameLevel(oldSave);
        }
        get => _audio;
    }
    DataSaveLevel oldSave;
    //Назад в меню
    private void Awake()
    {
        oldSave = SaveLevel.Load();
        Audio = oldSave.Audio;
    }
    public void AudioBtn()
    {
        Audio = !Audio;
    }
}
