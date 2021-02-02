using System.Text;

namespace Lesson_05_Tree_BFS
{
    public class Tree
    {
        /// <summary>
        /// Корень бинарного дерева
        /// </summary>
        public Node RootNode { get; set; }

        private Node Add(Node node, Node currentNode = null)
        {
            if (RootNode == null)
            {
                node.ParentNode = null;
                return RootNode = node;
            }

            currentNode = currentNode ?? RootNode;
            node.ParentNode = currentNode;
            int result;
            return (result = node.Data.CompareTo(currentNode.Data)) == 0
                ? currentNode
                : result < 0
                    ? currentNode.LeftNode == null
                        ? (currentNode.LeftNode = node)
                        : Add(node, currentNode.LeftNode)
                    : currentNode.RightNode == null
                        ? (currentNode.RightNode = node)
                        : Add(node, currentNode.RightNode);
        }
        /// <summary>
        /// Добавление данных в бинарное дерево
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Узел</returns>
        public Node Add(int data)
        {
            return Add(new Node(data));
        }

        /// <summary>
        /// Поиск узла по значению
        /// </summary>
        /// <param name="data">Искомое значение</param>
        /// <param name="startWithNode">Узел начала поиска</param>
        /// <returns>Найденный узел</returns>
        public Node FindNode(int data, Node startWithNode = null)
        {
            startWithNode = startWithNode ?? RootNode;
            int result;
            return (result = data.CompareTo(startWithNode.Data)) == 0
                ? startWithNode
                : result < 0
                    ? startWithNode.LeftNode == null
                        ? null
                        : FindNode(data, startWithNode.LeftNode)
                    : startWithNode.RightNode == null
                        ? null
                        : FindNode(data, startWithNode.RightNode);
        }
        /// <summary>
        /// Удаление узла бинарного дерева
        /// </summary>
        /// <param name="node">Узел для удаления</param>
        private void Remove(Node node)
        {
            if (node == null)
            {
                return;
            }

            var currentNodeSide = node.NodeSide;
            //если у узла нет подузлов, можно его удалить
            if (node.LeftNode == null && node.RightNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = null;
                }
                else
                {
                    node.ParentNode.RightNode = null;
                }
            }
            //если нет левого, то правый ставим на место удаляемого 
            else if (node.LeftNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.RightNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.RightNode;
                }

                node.RightNode.ParentNode = node.ParentNode;
            }
            //если нет правого, то левый ставим на место удаляемого 
            else if (node.RightNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.LeftNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.LeftNode;
                }

                node.LeftNode.ParentNode = node.ParentNode;
            }
            //если оба дочерних присутствуют, 
            //то правый становится на место удаляемого,
            //а левый вставляется в правый
            else
            {
                switch (currentNodeSide)
                {
                    case Side.Left:
                        node.ParentNode.LeftNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    case Side.Right:
                        node.ParentNode.RightNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    default:
                        var bufLeft = node.LeftNode;
                        var bufRightLeft = node.RightNode.LeftNode;
                        var bufRightRight = node.RightNode.RightNode;
                        node.Data = node.RightNode.Data;
                        node.RightNode = bufRightRight;
                        node.LeftNode = bufRightLeft;
                        Add(bufLeft, node);
                        break;
                }
            }
        }
        /// <summary>
        /// Удаление узла дерева
        /// </summary>
        /// <param name="data">Данные для удаления</param>
        public void Remove(int data)
        {
            var foundNode = FindNode(data);
            Remove(foundNode);
        }

        /// <summary>
        /// Вывод узла дерева
        /// </summary>
        /// <param name="output">Строка для вывода</param>
        /// <param name="depth">Уровень ветки</param>
        /// <param name="parent">Ссылка на родителя</param>
        private void displayNode(StringBuilder output, int depth, Node parent = null)
        {
            if (parent == null)
                parent = RootNode;

            if (parent.RightNode != null)
                displayNode(output, depth + 1, parent.RightNode);

            output.Append('\t', depth);
            output.AppendLine(parent.Data.ToString());


            if (parent.LeftNode != null)
                displayNode(output, depth + 1, parent.LeftNode);

        }
        /// <summary>
        /// Вывод дерева
        /// </summary>
        public string PrintTree()
        {
            StringBuilder output = new StringBuilder();
            displayNode(output, 0);
            return output.ToString();
        }

    }
}
