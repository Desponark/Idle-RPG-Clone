using Godot;
using System;

public partial class Logger : RichTextLabel {
	public static event Action<string> AddLog;

	public static void Log(string text) {
		AddLog.Invoke(text + "\n");
	}

	public override void _Ready() {
		AddLog += (string text) => { AppendText(text); };
	}
}