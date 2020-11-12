using System;

namespace Priority_Queue
{
    enum Priority { High, Low };
    class QueueCircular
    {
        private int _max;
        private char[] _qArray;
        private int _front;
        private int _rear;
        private int _size;

        //Size of queue passed as argument
        //Init queue to empty
        public QueueCircular(int qSize)
        {
            _max = qSize;
            _qArray = new char[_max];
            _front = 0;
            _rear = -1;
            _size = 0;
        }

        // Function to add an item to the queue. 
        // Changes _rear and _size
        // Returns: 
        //   true: successful
        //   false: unsuccessful
        public bool enqueue(char item)
        {
            bool rv;

            //Are we at the end of the Array?
            if (_rear == _max - 1)
            {
                _rear = -1;  //Move to beginning
            }

            //Are we full?
            if (_size == _max)
            {
                rv = false;  //Yes
            }
            else
            {
                _rear++;
                _qArray[_rear] = item;
                _size++;
                rv = true;
            }

            return rv;
        }

        // Function to remove an item from queue. 
        // Changes _front and _size 
        // Returns: 
        //   true: successful   
        //         p holds value
        //   false: unsuccessful
        //         p holds -1
        public bool dequeue(ref char p)
        {
            bool rv = true;

            //Are we empty?
            if (_size == 0)
            {
                p = '*';
                rv = false;
            }
            else
            {
                p = _qArray[_front];
                _front++;
                if (_front == _max)
                    _front = 0;
                _size--;
            }
            return rv;

        }

        // Function to print queue. 
        public void printQueue()
        {
            //Are we empty?
            if (_size == 0)
            {
                Console.WriteLine("Queue is Empty");
                return;
            }
            else
            {
                int i;
                i = _front;
                do
                {
                    Console.WriteLine(_qArray[i]);
                    if (i == _rear) break;    //Done
                    i++;
                    if (i == _max)  //We at end of array?
                        i = 0;
                } while (true);
            }

        }
    }

    class PriorityQueue
    {
        private QueueCircular _qHigh;
        private QueueCircular _qLow;

        public PriorityQueue(int size)
        {
            _qHigh = new QueueCircular(size);
            _qLow = new QueueCircular(size);
        }

        public bool enqueue(Priority priority, char item)
        {
            bool rv = true;

            if (priority == Priority.High)
            {
                rv = _qHigh.enqueue(item);
            }
            else if (priority == Priority.Low)
            {
                rv = _qLow.enqueue(item);
            }
            else
            {
                rv = false;
            }
            return rv;
        }

        public bool dequeue(ref char p)
        {
            bool rv = false;
            if ((rv = _qHigh.dequeue(ref p)) == false)
            {
                rv = _qLow.dequeue(ref p);
            }
            return rv;
        }

        public void printQueue()
        {
            Console.WriteLine("High");
            _qHigh.printQueue();
            Console.WriteLine("Low");
            _qLow.printQueue();
        }

    }

    // Test code 
    class Program
    {
        static void Main()
        {
            PriorityQueue Q = new PriorityQueue(5);
            char item = '#';

            Console.WriteLine("\nPrintout of queue\n--------------------");
            Q.printQueue();
            Console.WriteLine();

            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enqueue of {0} ", (char)(i + 97));
                if (Q.enqueue(Priority.Low, (char)(i + 97)))
                {
                    Console.WriteLine("Successful");
                }
                else
                {
                    Console.WriteLine("Unsuccessful");
                }
            }

            Console.WriteLine("\nPrintout of queue\n--------------------");
            Q.printQueue();
            Console.WriteLine();

            for (int i = 0; i < 2; i++)
            {
                if (Q.dequeue(ref item))
                {
                    Console.WriteLine("{0} removed from queue", item);
                }
                else
                {
                    Console.WriteLine("Dequeue Error");
                }
            }
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enqueue of {0} ", (char)(i + 65));
                if (Q.enqueue(Priority.High, (char)(i + 65)))
                {
                    Console.WriteLine("Successful");
                }
                else
                {
                    Console.WriteLine("Unsuccessful");
                }
            }

            Console.WriteLine("\nPrintout of queue\n--------------------");
            Q.printQueue();
            Console.WriteLine();

            for (int i = 0; i < 7; i++)
            {
                if (Q.dequeue(ref item))
                {
                    Console.WriteLine("{0} removed from queue", item);
                }
                else
                {
                    Console.WriteLine("Dequeue Error");
                }
            }

            Console.WriteLine("\nPrintout of queue\n--------------------");
            Q.printQueue();
            Console.ReadKey();
        }
    }
}
