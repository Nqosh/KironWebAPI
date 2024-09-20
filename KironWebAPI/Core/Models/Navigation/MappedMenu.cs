namespace KironWebAPI.Core.Models.Navigation
{
    public class MappedMenu
    {
        public string NavText { get; set; }
        public List<ChildMenu> Children { get; set; }    
    }
}
