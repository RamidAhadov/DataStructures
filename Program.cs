using System.Diagnostics;
using ListDemo;
using ListDemo.Exceptions;
using ListDemo.List;
using ListDemo.Queue;
using ListDemo.Stack;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
ListDemo<int> list = new ListDemo<int>(4);

list.Add(1);
list.Add(2);
list.Add(3); //3
list.Add(5);
list.Add(4); //3
list.Add(1000); // 1 2 3 5

list.Remove(1000);
list.Remove(4);
list.Remove(5);

list.AddRange(list);

//list.Clear();

foreach (var item in list)
{
    Console.WriteLine("Item: "+item);
}

StackDemo<int> stackDemo = new StackDemo<int>();

stackDemo.Push(1);
stackDemo.Push(2);
stackDemo.Push(3);
stackDemo.Push(4);
stackDemo.Push(5);
stackDemo.Push(6);

stackDemo.Pop();
stackDemo.Pop();
stackDemo.Pop();


Queue<int> queue = new Queue<int>();

QueueDemo<int> queueDemo = new QueueDemo<int>();

queueDemo.Enqueue(1);
queueDemo.Enqueue(2);
queueDemo.Enqueue(3);
queueDemo.Enqueue(4);
queueDemo.Enqueue(5);

foreach (var item in queueDemo)
{
    Console.WriteLine("Queue item: " + item);
}
Console.WriteLine(queueDemo.Dequeue());
bool result = list.Remove(45);

// for (int i = 0; i < 10000000; i++)
// {
//     list.Add(i);
// }
//list.Remove(3);
stopwatch.Stop();

Console.WriteLine("Elapsed time for Demo: "+ stopwatch.ElapsedMilliseconds);


Console.WriteLine("Count of elements: "+list.Count);

foreach (var item in list)
{
    Console.WriteLine(item);
}

stopwatch.Reset();
stopwatch.Start();
List<int> list1 = new List<int>();
// list1.Add(11);
// list1.Add(22);
// list1.Add(33);
// list1.Add(44);
// list1.Add(55);;
// list1.Add(66);


for (int i = 0; i < 10000000; i++)
{
    list1.Add(i);
}

stopwatch.Stop();
Console.WriteLine("Elapsed time for Original: "+ stopwatch.ElapsedMilliseconds);
int a = list1.IndexOf(7);

list.AddRange(list1);

Console.WriteLine("----------------------------");
foreach (var item in list)
{
    //Console.WriteLine(item);
}

Console.WriteLine("Before deletion:"+list1.IndexOf(4));
list1.Remove(3);
Console.WriteLine("After deletion:"+list1.IndexOf(4));

//Console.ReadLine();
