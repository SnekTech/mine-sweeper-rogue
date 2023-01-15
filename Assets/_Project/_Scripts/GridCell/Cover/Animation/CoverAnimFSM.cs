using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public struct Triggers
    {
        public bool ShouldReveal;
        public bool ShouldPutCover;
    }
    
    public class CoverAnimFSM : SpriteAnimFSM
    {
        public Triggers Triggers = new Triggers
        {
            ShouldReveal = false,
            ShouldPutCover = false,
        };
    }
}