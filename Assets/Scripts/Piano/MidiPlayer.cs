using System;
using UnityEngine;
using UnityEngine.Events;

public class MidiPlayer : MonoBehaviour
{
	[Header("References")]
	public PianoKeyController PianoKeyDetector;

	[Header("Properties")]
	public float GlobalSpeed = 1;
	public RepeatType RepeatType;

	public KeyMode KeyMode;
	public bool ShowMIDIChannelColours;
	public Color[] MIDIChannelColours;

	[Header("Ensure Song Name is filled for builds")]
	public MidiSong[] MIDISongs;

	[HideInInspector]
	public MidiNote[] MidiNotes;
	public UnityEvent OnPlayTrack { get; set; }

	// public GameObject parent;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			PianoKeyDetector.PianoNotes["C5"].Play();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			PianoKeyDetector.PianoNotes["D5"].Play();
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			PianoKeyDetector.PianoNotes["E5"].Play();
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			PianoKeyDetector.PianoNotes["F5"].Play();
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			PianoKeyDetector.PianoNotes["G5"].Play();
		}
	
	}

}

public enum RepeatType { NoRepeat, RepeatLoop, RepeatOne }
public enum KeyMode { Physical, ForShow }

[Serializable]
public class MidiSong
{
#if UNITY_EDITOR
	public UnityEngine.Object MIDIFile;
#endif
	public string SongFileName;
	public float Speed = 1;
	[TextArea]
	public string Details;
}