using gilded_rose;
using Xunit;
using System.IO;

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

			Assert.Equal(expectedOutput, output);
		}
	}
}
