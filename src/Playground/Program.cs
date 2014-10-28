using System.Windows.Forms;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new View(new ViewModel());
            Application.Run(ui);
        }
    }
}
