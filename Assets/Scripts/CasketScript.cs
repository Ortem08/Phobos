using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class CasketScript : MonoBehaviour
{
    public Sprite CasketImage;
    public GameObject CasketScriptHolder;

    [SerializeField] public static float DamageDistance { get; private set; }

    private GameObject currentChest;
    private Vector3 startPosition;
    private Vector3 direction;
    private Vector3 spawnPoint;
    private GameObject[] npcs;
    private bool prepareSoundIsDone;
    private AudioSource audioSource;
    private GameObject player;
    private bool flag = true;

    private AudioMixer Mixer { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prepareSoundIsDone = false;

        startPosition = player.transform.position;
        direction = player.GetComponent<NavMeshAgent>().destination - startPosition;
        spawnPoint = startPosition + direction.normalized * 3;

        currentChest = new GameObject();
        var chectSprite = currentChest.AddComponent<SpriteRenderer>();
        chectSprite.sprite = CasketImage;
        chectSprite.sortingOrder = -1;
        currentChest.transform.position = spawnPoint;

        npcs = GameObject.FindGameObjectsWithTag("NPC");

        DamageDistance = 10f;
        Mixer = AssetDatabase.LoadAssetAtPath<AudioMixer>("Assets/AudioMixer.mixer");
        MakePrepareSound();
    }

    private void Update()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            prepareSoundIsDone = true;
            player.GetComponent<PlayerControl>().BlockedByAnotherScript = false;
        }

        if (prepareSoundIsDone && flag)
        {
            currentChest.GetComponent<SpriteRenderer>().sortingOrder = 10;
            flag = false;

            var casketSong = Resources.Load<AudioClip>("CascetSong" + Random.Range(1, 8));
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
            //audioSource.clip = casketSong;
            //audioSource.volume = 0.02f;
            //audioSource.loop = false;
            audioSource.PlayOneShot(casketSong);
            currentChest.AddComponent<AudioVolumeDistance>().Source = audioSource;
        }

        if (!audioSource.isPlaying && prepareSoundIsDone)
        {
            KillCasketScript();
        }

        else if (audioSource.isPlaying && prepareSoundIsDone)
        {
            foreach (var bro in npcs)
            {
                var distance = Mathf.Abs((bro.transform.position - spawnPoint).magnitude);
                if (distance < DamageDistance)
                {
                    MakeDamage(bro, distance);
                    //Destroy(gameObject);
                    //Destroy(currentChest);
                }
            }
        }
    }

    private void MakePrepareSound()
    {
        var casketPrepareSound = Resources.Load<AudioClip>("CascetPrepare" + Random.Range(1, 4));
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
        //audioSource.clip = casketPrepareSound;
        //audioSource.volume = 0.02f;
        //audioSource.loop = false;
        audioSource.PlayOneShot(casketPrepareSound);
        player.GetComponent<PlayerControl>().BlockedByAnotherScript = true;
    }

    private void KillCasketScript()
    {
        Destroy(currentChest);
        Destroy(CasketScriptHolder);
    }

    private void MakeDamage(GameObject npc, float distance)
    {
        var abc = npc.GetComponentInChildren<HpBar>();
        abc.ChangeHealth(-(1 / (distance / 100)));
        StartCoroutine(WaitFor(5));
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
