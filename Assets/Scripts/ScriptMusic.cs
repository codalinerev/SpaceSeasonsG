using UnityEngine;

public class ScriptMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] songs;
    //int i = 0;
    float time = 0;
    public AudioSource sourceAudio;// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        sourceAudio = GetComponent<AudioSource>();
        if (sourceAudio != null)
        {
            sourceAudio.volume = 0.05f;
            sourceAudio.clip = songs[0];
            sourceAudio.Play();
            sourceAudio.loop = false;
        }
    }
    public void SwitchMusicOnOff()
    {
        if (sourceAudio.isPlaying) sourceAudio.Stop();
        else sourceAudio.Play();     
    }

    // Update is called once per frame
    /* void Update()
     {
         time += Time.deltaTime;
         if (time > 5) sourceAudio.Stop();

     }*/
}
