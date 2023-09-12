using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public enum View
{
    Video,
    Text
}
public class Interactable : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string AnimationName;
    [Range(0f, 2f)] [SerializeField] private float videoStartDelay = 1f;

    [Tooltip("Only play the video and automatically rotate the screen once the video finished playing.")]
    [SerializeField]
    private bool StartWithVideo = false;

    private VideoPlayer videoPlayer;

    private bool isPlaying = false;
    private bool firstPlay = true;
    private View view;
    
    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        view = StartWithVideo ? View.Video : View.Text;
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

        if (!isPlaying && view == View.Video)
        {
            StartCoroutine(StartDelayedVideo(0));
            return;
        }


        if (!StartWithVideo)
        {
            animator.StopPlayback();
            animator.Play(AnimationName);
            StartCoroutine(StartDelayedVideo(videoStartDelay));
        }
        else
        {
            if (firstPlay)
            {
                StartCoroutine(StartDelayedVideo(0));
                firstPlay = false;
            }
            else
            {
                animator.StopPlayback();
                animator.Play(AnimationName);
                StartCoroutine(StartDelayedVideo(videoStartDelay));
            }
        }
    }

    private IEnumerator StartDelayedVideo(float delay)
    {
        yield return new WaitForSeconds(delay);
        view = View.Video;
        videoPlayer.Play();
        isPlaying = true;
    }

    private void OnLoopPointReched(VideoPlayer videoPlayer)
    {
        print("LOOP POINT REACHED");
        animator.Play(AnimationName + " reverse");
        isPlaying = false;
        firstPlay = false;
        view = View.Text;
    }
}