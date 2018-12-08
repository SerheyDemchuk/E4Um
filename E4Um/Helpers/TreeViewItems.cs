using System.Collections.Generic;


namespace E4Um.Helpers
{
    public class TreeViewItems
    {
        public string Name { get; set; }
    }

    public class FileItem: TreeViewItems {}

    public class DirectoryItem: TreeViewItems
    {
        public List<TreeViewItems> Items { get; set; }

        public DirectoryItem()
        {
            Items = new List<TreeViewItems>();
        }
    }
}
