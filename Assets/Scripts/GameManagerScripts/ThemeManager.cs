using UnityEngine;

public class ThemeManager : MonoBehaviour
{

    private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicList;

    // Start is called before the first frame update

    //FI
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        DetermineAudioClip();
    }

    private void DetermineAudioClip()
    {
        musicSource.clip = musicList[Random.Range(0, musicList.Length)];
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSource.isPlaying == false)
        {
            DetermineAudioClip();
        }
    }

}
