using System.Diagnostics;
using DataStructuresDemo.LinkedList;
using DataStructuresDemo.List;
using DataStructuresDemo.Queue;
using DataStructuresDemo.Stack;

LinkedListDemo<int> linkedListDemo = new LinkedListDemo<int>();
linkedListDemo.AddLast(4); //2
linkedListDemo.AddLast(5); //3
linkedListDemo.AddFirst(3); //1
linkedListDemo.AddLast(9); //5
linkedListDemo.AddAfter(5,8); // 3 4 5 8 9 // 4

linkedListDemo.AddBefore(4,12);

var arr = new int[6];

linkedListDemo.CopyTo(arr,0);

foreach (var i in arr)
{
    Console.WriteLine("New array: " + i);
}

Stopwatch stopwatch = new Stopwatch();

Console.WriteLine("-------------------------------------------------------");
LinkedListDemo<int> myLinkedList = new LinkedListDemo<int>();
myLinkedList.AddLast(-1);

stopwatch.Start();
for (int i = 0; i < 10000; i++)
{
    //myLinkedList.AddBefore(-1,i);
}

// foreach (var i in myLinkedList)
// {
//     Console.WriteLine(i);
// }
//bool myResult = myLinkedList.Contains(9999);
stopwatch.Stop();
Console.WriteLine("My List (Add before): "+stopwatch.ElapsedMilliseconds);
stopwatch.Reset();




LinkedList<int> dotNetLinkedList = new LinkedList<int>();

//dotNetLinkedList.AddLast(5);
//LinkedListNode<int> node = new LinkedListNode<int>(5);

for (int i = 0; i < 10000; i++)
{
    dotNetLinkedList.AddLast(i);
}
stopwatch.Start();
bool resultList = dotNetLinkedList.Contains(6000);
stopwatch.Stop();
Console.WriteLine("Dot Net List: "+stopwatch.ElapsedMilliseconds);


Console.WriteLine("-------------------------------------------------------");

foreach (var item in linkedListDemo)
{
    Console.WriteLine("My linked list: "+item);
}
stopwatch.Reset();
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

LinkedList<int> linkedList = new LinkedList<int>();

foreach (var i in linkedList)
{
    
}


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
