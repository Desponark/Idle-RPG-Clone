using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Logger : RichTextLabel {
	public static event Action<string> AddLog;

	// Apparently when using AppendText() there is no way to get the text that was added to the RichTextLavel.
	// GetParsedText() only returns the parsed text without any bbcode which is NOT what I want...
	// https://github.com/godotengine/godot-proposals/issues/8739
	// Therefore here is a nice duplicate string field just for getting the raw text...
	private string rawText = "";
	public string RawText { get => rawText; }

	public static void Log(string text) {
		AddLog.Invoke(text + "\n");
	}

	public override void _Ready() {
		AddLog += EnterText;
	}

	// it is important to unregister the event in case the scene gets reloaded
	public override void _ExitTree() {
		AddLog -= EnterText;
	}

	private void EnterText(string text) {
		AppendText(text);
		rawText += text;
	}

	// Takes the last lines of a string in a more efficient manner
	// https://stackoverflow.com/questions/11942885/take-the-last-n-lines-of-a-string-c-sharp

	public List<string> TakeLastRawLines(int count) {
		List<string> lines = new();
		Match match = Regex.Match(rawText, "^.*$", RegexOptions.Multiline | RegexOptions.RightToLeft);

		while (match.Success && lines.Count < count) {
			lines.Insert(0, match.Value);
			match = match.NextMatch();
		}

		return lines;
	}
}