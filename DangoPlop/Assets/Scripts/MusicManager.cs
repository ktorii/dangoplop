using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MusicType {
	Upbeat,
	Piano,
	None
}

public class MusicManager : MonoBehaviour {
	private static MusicManager instance = null;
	private MusicType currentMusicType = MusicType.Upbeat;
	private Text musicTextDescription;
	private Text musicTextType;
	private string currentScene = "";

	// music
	private List<AudioSource> songs;
	private List<AudioSource> songsUpbeat = new List<AudioSource>();
	private List<AudioSource> songsPiano = new List<AudioSource>();
	private int currentSongIndexPlaying = 0;

	public static MusicManager Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
		currentScene = SceneManager.GetActiveScene().name;
		musicTextDescription = GameObject.FindGameObjectWithTag ("MusicTextDescription").GetComponent<Text>();
		musicTextType = GameObject.FindGameObjectWithTag ("MusicTextType").GetComponent<Text>();

		songs = new List<AudioSource>(GetComponents<AudioSource> ());

		// manually add all songs into playlists. Is there a better way?
		songsUpbeat.Add (songs [0]);
		songsUpbeat.Add (songs [1]);
		songsUpbeat.Add (songs [2]);

		songsPiano.Add (songs [3]);
		songsPiano.Add (songs [4]);
		songsPiano.Add (songs [5]);
		songsPiano.Add (songs [6]);

		// start music
		if (currentScene == "Gameplay") {
			restartPlaylistSongForGameplay ();
		} else { 
			restartPlaylistSongForMenu ();
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.M)) {
			changeMusicType ();
		}

		// print (currentSongIndexPlaying);

		string prevScene = currentScene;
		currentScene = SceneManager.GetActiveScene ().name;
		musicTextDescription = GameObject.FindGameObjectWithTag ("MusicTextDescription").GetComponent<Text>();
		musicTextType = GameObject.FindGameObjectWithTag ("MusicTextType").GetComponent<Text>();

		if (isDonePlaying (currentMusicType)) {
			playNextSong ();
		}

		if (prevScene == "Gameplay" && currentScene != "Gameplay") {
			restartPlaylistSongForMenu ();
		} else if (prevScene != "Gameplay" && currentScene == "Gameplay") {
			restartPlaylistSongForGameplay ();
		}

		if (currentScene == "Gameplay") {
			musicTextDescription.enabled = false;
		} else {
			musicTextDescription.enabled = true;
		}

	}

	void FixedUpdate() {
		switch (currentMusicType) {
		case MusicType.Upbeat:
			musicTextType.text = "Upbeat";
			break;
		case MusicType.Piano:
			musicTextType.text = "Piano";
			break;
		case MusicType.None:
			musicTextType.text = "No Music";
			break;
		default:
			break;
		}
	}

	private void changeMusicType() {
		stopPreviousSong ();
		switch (currentMusicType) {
		case MusicType.Upbeat:
			currentMusicType = MusicType.Piano;
			if (currentScene == "Gameplay") {
				playSongIndex (songsUpbeat.Count+1);
			} else {
				playSongIndex (songsUpbeat.Count);
			}
			break;
		case MusicType.Piano:
			currentMusicType = MusicType.None;
			break;
		case MusicType.None:
			currentMusicType = MusicType.Upbeat;
			if (currentScene == "Gameplay") {
				playSongIndex (1);
			} else {
				playSongIndex (0);
			}
			break;
		default:
			break;
		}
	}

	private void playSongIndex(int index) {
		songs [index].Play ();
		currentSongIndexPlaying = index;
	}

	public void stopPreviousSong() {
		songs[currentSongIndexPlaying].Stop();
	}

	private bool isDonePlaying(MusicType playlistType) {
		List<AudioSource> playlist = new List<AudioSource> ();
		switch (currentMusicType) {
		case MusicType.Upbeat:
			playlist = songsUpbeat;
			break;
		case MusicType.Piano:
			playlist = songsPiano;
			break;
		case MusicType.None:
			playlist = new List<AudioSource> ();
			break;
		default:
			break;
		}
		for (int i = 0; i < playlist.Count; i++) {
			if (playlist [i].isPlaying) {
				return false;
			}
		}
		return true;
	}

	private void playNextSong() {
		int nextIndexSong = currentSongIndexPlaying + 1;
		switch (currentMusicType) {
		case MusicType.Upbeat:
			if (nextIndexSong >= songsUpbeat.Count) {
				nextIndexSong = 0;
			}
			break;
		case MusicType.Piano:
			if (nextIndexSong >= songsUpbeat.Count + songsPiano.Count) {
				nextIndexSong = songsUpbeat.Count;
			}
			break;
		case MusicType.None:
			return;
		default:
			break;
		}

		playSongIndex (nextIndexSong);
	}

	public void restartPlaylistSongForGameplay() {
		stopPreviousSong ();
		int nextIndexSong = 0;
		switch (currentMusicType) {
		case MusicType.Upbeat:
			nextIndexSong = 1;
			break;
		case MusicType.Piano:
			nextIndexSong = songsUpbeat.Count + 1;
			break;
		case MusicType.None:
			return;
			break;
		default:
			break;
		}
		playSongIndex (nextIndexSong);
	}

	private void restartPlaylistSongForMenu() {
		stopPreviousSong ();
		int nextIndexSong = 0;
		switch (currentMusicType) {
		case MusicType.Upbeat:
			nextIndexSong = 0;
			break;
		case MusicType.Piano:
			nextIndexSong = songsUpbeat.Count;
			break;
		case MusicType.None:
			return;
			break;
		default:
			break;
		}
		playSongIndex (nextIndexSong);
	}

	public void resumeMusic() {
		songs [currentSongIndexPlaying].Play ();
	}

	public void pauseMusic() {
		songs [currentSongIndexPlaying].Pause ();
	}
}
