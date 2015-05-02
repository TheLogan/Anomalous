using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
	//Load profile images from resources
	//Load message file from resources
	//combine it all with a gui prefab

	[SerializeField]
	private RunFactor _runWhen;
	[SerializeField]
	private string _messageName;

	enum RunFactor { Start, Function }
	enum States { Ready, Running, HasRun }
	States _currentState = States.Ready;

	private GameObject _messageInstance;
	private Image _imageField;
	private Text _textField;
	private List<string> _messages;
	private Dictionary<string, Sprite> _profileImages = new Dictionary<string, Sprite>();
	private bool _doContinue;

	private AudioSource _mySource;
	private AudioClip _menuStartClip;

	void Start()
	{
		_mySource = GetComponent<AudioSource>();
		if(_mySource == null)
			_mySource = gameObject.AddComponent<AudioSource>();

		_menuStartClip = Resources.Load<AudioClip>("SFX/Purchase");

		var go = (GameObject)Resources.Load("MessageCanvas");
		_messageInstance = (GameObject)Instantiate(go);
		_messageInstance.SetActive(false);

		_imageField = _messageInstance.GetComponentsInChildren<Image>(true)[0];
		_textField = _messageInstance.GetComponentsInChildren<Text>(true)[0];

		var v = (TextAsset)Resources.Load("messages/" + _messageName);
		_messages = v.text.Split(';').Select(p => p.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();

		var profileResources = Resources.LoadAll<Sprite>("ProfileImages");
		foreach (var profileResource in profileResources)
		{
			_profileImages.Add(profileResource.name, profileResource);
		}

		if (_runWhen == RunFactor.Start)
		{
			RunMessage();
		}
	}


	/// <summary>
	/// Starts the messages
	/// </summary>
	public void RunMessage()
	{
		if (_currentState != States.Ready)
		{
			Debug.LogError("Trying to play a sequence that is currently or has already run");
			return;
		}
		Time.timeScale = 0;
		_currentState = States.Running;
		_mySource.clip = _menuStartClip;
		_mySource.Play();
		StartCoroutine(ActualRunning());
	}

	IEnumerator ActualRunning()
	{
		_messageInstance.SetActive(true);
		int index = 0;
		while (true) //While running
		{
			SetMessage(index);
			while (true) //wait for next input
			{
				if (_doContinue)
				{
					_doContinue = false;
					index++;
					if (index >= _messages.Count)
						_currentState = States.HasRun;
					break;
				}
				yield return new WaitForEndOfFrame();
			}
			if (_currentState == States.HasRun)
			{
				CloseMessageWindow();
				break;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			_doContinue = true;
		else
			_doContinue = false;
	}

	void CloseMessageWindow()
	{
		Time.timeScale = 1f;
		_messageInstance.SetActive(false);
	}

	void SetMessage(int index)
	{
		string profileName = _messages[index].Split(':')[0];
		_imageField.sprite = _profileImages[profileName];
		_textField.text = _messages[index];
	}
}
