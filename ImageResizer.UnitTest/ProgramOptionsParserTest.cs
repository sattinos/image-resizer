using System.Collections.Generic;
using Xunit;
using ImageResizer.Options;
using System;

namespace ImageResizer.UnitTest
{
    public class ProgramOptionsParserTest
    {
        public static IEnumerable<object[]> InvalidArgumentsSet
           => new object[][] {
                new object[] { new string[] { "some wrong arguments" } },
                new object[] { new string[] { "" } },
                new object[] { Array.Empty<string>() }
           };

        [Theory(DisplayName = "wrong arguments test")]
        [MemberData(nameof(InvalidArgumentsSet))]
        public void WrongArgumentsPassed(string[] args)
        {
            var appInstance = new Application(args);
            Assert.Null(appInstance.Settings);
            Assert.NotNull(appInstance.Error);
        }

        public static IEnumerable<object[]> ValidArgumentsSet
           => new object[][] {
                new object[] { new string[] { "-f", "25" }, 25, Behaviour.Copy, Environment.CurrentDirectory, Environment.CurrentDirectory  },
                new object[] { new string[] { "-f", "200", "-s", "c:/images", "-b", "replace" }, 200, Behaviour.Replace, "c:/images", "c:/images" }
           };

        [Theory(DisplayName = "correct arguments test")]
        [MemberData(nameof(ValidArgumentsSet))]
        public void CorrectArgumentsPassed(string[] args, int factor, Behaviour behaviour, string sourcePath, string targetPath)
        {
            var appInstance = new Application(args);
            Assert.Null(appInstance.Error);
            Assert.NotNull(appInstance.Settings);
            Assert.Equal(appInstance.Settings.ScaleFactor, factor);
            Assert.Equal(appInstance.Settings.Behaviour, behaviour);
            Assert.Equal(appInstance.Settings.SourcePath, sourcePath);
            Assert.Equal(appInstance.Settings.TargetPath, targetPath);
        }
    }
}
