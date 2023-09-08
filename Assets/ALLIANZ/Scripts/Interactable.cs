using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;


public class Interactable : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string AnimationName;
    [Range(0f, 2f)] [SerializeField] private float videoStartDelay = 1f;

    [Tooltip("Only play the video and automatically rotate the screen once the video finished playing.")]
    [SerializeField]
    private bool AutoRotate = false;

    private VideoPlayer videoPlayer;

    private bool isPlaying = false;

    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnLoopPointReched;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnLoopPointReched;
    }

    public void Select()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();
            isPlaying = false;
            return;
        }


        if (!AutoRotate)
        {
            animator.Play(AnimationName);
            StartCoroutine(StartDelayedVideo(videoStartDelay));
        }
        else
        {
            StartCoroutine(StartDelayedVideo(0));
        }
    }

    private IEnumerator StartDelayedVideo(float delay)
    {
        yield return new WaitForSeconds(delay);
        print("PLAYING");
        videoPlayer.Play();
        isPlaying = true;
    }

    private void OnLoopPointReched(VideoPlayer videoPlayer)
    {
        animator.Play(AnimationName + " reverse");
        isPlaying = false;
    }
}