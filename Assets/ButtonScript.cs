using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.IO;
using System.Text.RegularExpressions; // for Regex

/*
 * v0.2 2015/09/27
 *   - can handle multiline text
 * v0.1 2015/09/27
 *   - can replace nth tab to newline
 */

public class ButtonScript : MonoBehaviour {

	public InputField IF_nth;
	public Text T_status;

	void ReplaceTabToNewLine(int nth) {
		string infile = "indata.txt";
		string outfile = "outdata.txt";

		if (File.Exists (infile) == false) {
			T_status.text = infile + ": not found";
			return;
		}

		string intext = System.IO.File.ReadAllText ("indata.txt");

		string outtext = "";
		int count;
		bool newline = false;
		foreach (var linetext in intext.Split('\n')) {
			count = 0;
			if (outtext.Length > 0) {
				outtext = outtext + '\n';
				newline = true;
			}
			foreach (var element in linetext.Split('\t')) {
				if (newline) {
					newline = false;
				} else {
					if (count >= nth) {
						count = 0;
						if (outtext.Length > 0) {
							outtext = outtext + System.Environment.NewLine;
						}
					} else {
						if (outtext.Length > 0) {
							outtext = outtext + '\t';
						}
					}
				}
				outtext = outtext + element;
				count++;
			}
		}

		T_status.text = "output to " + outfile;
		System.IO.File.WriteAllText (outfile, outtext);
	}

	public void OnClick() {
		string valtxt = IF_nth.text;
		int nth = int.Parse(new Regex("[0-9]+").Match(valtxt).Value);
		ReplaceTabToNewLine (nth);
	}	
}
