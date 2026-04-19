// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var newObj = new StackClass();
newObj.Push("first element");
newObj.Push("second element");
newObj.Push("third element");

var topElement = newObj.Peek();
var afterPop = newObj.Pop();
int size = afterPop.Count();

//create a class implemeting stack with Push, Pop and peek
public class StackClass
{
    private List<string> _container = new List<string>();
    public StackClass(List<string> items) {

        _container = items;
    }

    public static void Push(string element)
    {
        //adding to the collection
        _container.Add(element);
    }

    public static List<string> Pop()
    {
        //removing top from collection FIFO
        //find index of the last element and remove from stack
        var size = _container.Count();
        var elementToRemove = _container.IndexOf[size - 1];
        return _container.Remove(elementToRemove);
    }

    public static string Peek() {
        //finding top one
        var firstElement = _container.IndexOf[0];
        return firstElement;
    }
}