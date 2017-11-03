using gilded_rose;
using Xunit;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace gilded_rose_test
{

	public class ApplicationTest
  {
		private string expectedOutput = File.ReadAllText("ExpectedOutput.txt");

		[Fact]
		public void RunsWithSpecifiedInputFile()
		{
			string output = string.Empty;
			Application.SendOutput(x => output = x);
			Application.Main(new[] { "" });

			output = new Regex(@"(?:\r\n|[\r\n])+").Replace(output, Environment.NewLine);
			expectedOutput = new Regex(@"(?:\r\n|[\r\n])+").Replace(expectedOutput, Environment.NewLine);

			Assert.Equal(expectedOutput, output);
		}
	}
}
