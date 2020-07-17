using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{
    static class TreeUtil
    {
        /// <summary>
        /// Get how many levels are from the node to root
        /// </summary>
        /// <param name="node"></param>
        /// <returns>1 if on root, 2 the next, 3,...etc.</returns>
        static public int GetLevelFromRoot(TreeNode node)
        {
            if (node.Parent == null)
            {
                return 1;
            }
            return 1 + GetLevelFromRoot(node.Parent);
        }

        static public void MarkRegion(TreeNode node, bool isChecked)
        {
            MarkRegion(node, isChecked, true);
        }

        static public void MarkRegion(TreeNode node, bool isChecked, bool childnodes)
        {
            node.Checked = isChecked;
            if (childnodes)
                foreach (TreeNode childNode in node.Nodes)
                {
                    MarkRegion(childNode, isChecked, childnodes);
                }
        }

        static public void MarkRegion(TreeNode node, bool isChecked, int level, int currentLevel)
        {
            if (level == currentLevel)
            {
                MarkRegion(node, isChecked, false);
            }
            else if (level > currentLevel)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    MarkRegion(childNode, isChecked, level, currentLevel + 1);
                }
            }
        }

    }
}
