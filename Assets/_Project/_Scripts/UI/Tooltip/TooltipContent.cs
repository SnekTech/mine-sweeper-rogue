namespace SnekTech.UI.Tooltip
{
    public struct TooltipContent
    {
        public readonly string Header;
        public readonly string Body;

        public TooltipContent(string header = "", string body = "")
        {
            Header = header;
            Body = body;
        }
    }
}
