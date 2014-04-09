﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Gherkin.Specs
{
	[TestFixture]
	public class TokenizationTests
	{
		[Test, TestCaseSource(typeof(TestFileProvider), "GetValidTestFiles")]
		public void TestSuccessfulTokenMatching(string testFeatureFile)
		{
			Console.WriteLine(testFeatureFile);

			var featureFileFolder = Path.GetDirectoryName(testFeatureFile);
			Debug.Assert(featureFileFolder != null);
			var expectedTokensFile = testFeatureFile + ".tokens";

			var parser = new Parser();
			var tokenFormatterBuilder = new TokenFormatterBuilder();
			using (var reader = new StreamReader(testFeatureFile))
				parser.Parse(new TokenScanner(reader), new TokenMatcher(), tokenFormatterBuilder);

			var tokensText = tokenFormatterBuilder.GetTokensText();
			Console.WriteLine(tokensText);

			var expectedTokensText = LineEndingHelper.NormalizeLineEndings(File.ReadAllText(expectedTokensFile));

			Assert.AreEqual(expectedTokensText, tokensText);
		}
	}
}