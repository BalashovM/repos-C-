namespace Lesson_04_Binary_Tree

{
    /// <summary>
    /// Расположения узла относительно родителя
    /// </summary>
    public enum Side
    {
        Left,
        Right
    }

    public class Node
    {
        // <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="data">Данные</param>
        public Node(int data)
        {
            Data = data;
        }
        public int Data { get; set; }

        /// <summary>
        /// Левая ветка
        /// </summary>
        public Node LeftNode { get; set; }

        /// <summary>
        /// Правая ветка
        /// </summary>
        public Node RightNode { get; set; }

        /// <summary>
        /// Родитель
        /// </summary>
        public Node ParentNode { get; set; }

        /// <summary>
        /// Расположение узла относительно его родителя
        /// </summary>
        public Side? NodeSide =>
            ParentNode == null
            ? (Side?)null
            : ParentNode.LeftNode == this
                ? Side.Left
                : Side.Right;

    }
}
