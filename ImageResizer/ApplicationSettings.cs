using MatthiWare.CommandLine.Core.Attributes;

namespace ImageResizer.Options
{
    public enum Behaviour
    {
        Copy,
        Replace
    }

    public class ApplicationSettings
    {
        [Name("b", "behaviour"), DefaultValue(Behaviour.Copy), Description("shall the new images override the original? it defaults to copy.")]
        public Behaviour Behaviour { get; set; }
        [Name("s", "source-path"), DefaultValue(""), Description("source path that contains the images. it defaults to the path where the program is found.")]
        public string SourcePath { get; set; }
        [Name("t", "target-path"), DefaultValue(""), Description("target path the resized images will be saved to. it defaults to the path where the program is found.")]
        public string TargetPath { get; set; }
        [Required, Name("f", "scale-factor"), Description("a value bigger than 1 (percent value) that defines the scale percent. Example: -f 25 => scale factor will be 25% of the original image")]
        public int ScaleFactor { get; set; }
        public override string ToString()
        {
            return $@"Behaviour: {Behaviour}
Source path: {SourcePath}
Target path: {TargetPath}
Scale Factor: {ScaleFactor}";
        }
    }
}
