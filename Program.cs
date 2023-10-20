using System.Net.WebSockets;

namespace HW3b_368
{
    internal class Program
    {

        static void Main(string[] args)
        {
            BinaryMaxHeap<int> heap = new BinaryMaxHeap<int>();

        
            heap.Insert(4);
            heap.Insert(19);
            heap.Insert(56);
            heap.Insert(6);
            heap.Insert(8);
            heap.Insert(22);
            heap.Insert(12); 

            int[] arrayRepresentation = heap.ToArray();

            Console.WriteLine("Heap:");
            foreach (var value in arrayRepresentation)
            {
                Console.Write(value + " ");
            }

           
            arrayRepresentation = heap.HeapSort();
            Console.WriteLine("Sorted Heap:");
            foreach (var value in arrayRepresentation)
            {
                Console.Write(value + " ");
            }
        }


        public class Node<T> where T : IComparable<T>
        {
            public T Value { get; set; }
            public Node<T> Parent { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }

            public Node(T value)
            {
                Value = value;
                Parent = null;
                Left = null;
                Right = null;
            }
        }

        public class BinaryMaxHeap<T> where T : IComparable<T>
        {
            public Node<T> Root { get; private set; }
            private Node<T> lastInsertedNode;

            public BinaryMaxHeap()
            {
                Root = null;
            }



            public void Insert(T value)
            {
                Node<T> newNode = new Node<T>(value);


                if (Root == null)
                {
                    Root = newNode;
                    lastInsertedNode = Root;
                    return;
                }

                Queue<Node<T>> queue = new Queue<Node<T>>();
                queue.Enqueue(Root);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    if (current.Left == null)
                    {
                        current.Left = newNode;
                        newNode.Parent = current;
                        lastInsertedNode = newNode;
                        break;
                    }
                    else if (current.Right == null)
                    {
                        current.Right = newNode;
                        newNode.Parent = current;
                        lastInsertedNode = newNode;
                        break;
                    }
                    else
                    {
                        queue.Enqueue(current.Left);
                        queue.Enqueue(current.Right);
                    }
                }

                TrickleUp(lastInsertedNode);
            }

            private void TrickleUp(Node<T> node)
            {
                var parent = node.Parent;

                while (parent != null && node.Value.CompareTo(parent.Value) > 0)
                {

                    var temp = node.Value;
                    node.Value = parent.Value;
                    parent.Value = temp;


                    node = parent;
                    parent = node.Parent;
                }
            }

            public T[] ToArray()
            {
                if (Root == null)
                {
                    return new T[0]; 
                }

                List<T> result = new List<T>();

                var queue = new Queue<Node<T>>();
                queue.Enqueue(Root);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    result.Add(current.Value);

                    if (current.Left != null)
                    {
                        queue.Enqueue(current.Left);
                    }

                    if (current.Right != null)
                    {
                        queue.Enqueue(current.Right);
                    }
                }

                return result.ToArray();
            }


            public T[] HeapSort()
            {
                List<T> sortedList = new List<T>();

                while (Root != null)
                {
                    sortedList.Insert(0, Root.Value);  // this is the pulling off of first node. 

                    if (lastInsertedNode == Root)
                    {
                        Root = null;
                    }
                    else
                    {
                        T tempValue = Root.Value;  // the swap of the first node with the very last node - using a pointer 
                        Root.Value = lastInsertedNode.Value;
                        lastInsertedNode.Value = tempValue;

                       
                        if (lastInsertedNode == lastInsertedNode.Parent.Left) // eliminate where the old node was 
                        {
                            lastInsertedNode.Parent.Left = null; 
                        }
                        else
                        {
                            lastInsertedNode.Parent.Right = null;
                        }

                        // now need to loop thru the whole tree to locate the new lastInsertedNode 
                        var nodes = new Queue<Node<T>>();
                        nodes.Enqueue(Root);
                        Node<T> current = null;
                        while (nodes.Count > 0)
                        {
                            current = nodes.Dequeue();

                            if (current.Left != null)
                            {
                                nodes.Enqueue(current.Left);
                            }
                            if (current.Right != null)
                            {
                                nodes.Enqueue(current.Right);
                            }
                        }

                    
                        lastInsertedNode = current;

                        
                        TrickleDown(Root);
                    }
                }

                return sortedList.ToArray();
            }

            private void TrickleDown(Node<T> node)
            {
                while (node != null)
                {
                    Node<T> largest = node;
                    if (node.Left != null && node.Left.Value.CompareTo(node.Value) > 0)
                    {
                        largest = node.Left;
                    }
                    if (node.Right != null && node.Right.Value.CompareTo(largest.Value) > 0)
                    {
                        largest = node.Right;
                    }

                    if (largest == node) break;

                  
                    T tempValue = node.Value;
                    node.Value = largest.Value;
                    largest.Value = tempValue;

                    node = largest;
                }
            }



        }

    }
}


