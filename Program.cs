using System;
using System.Threading.Tasks;
using Python.Included;
using Python.Runtime;
namespace PythonClusterTest {
    class Program {
        static async Task Main(string[] args) {
            await Installer.SetupPython();
            Console.WriteLine("Installing pip...");
            Installer.TryInstallPip();
            Console.WriteLine("Installing pulp...");
            Installer.PipInstallModule("pulp");
            PythonEngine.Initialize();
            dynamic kmeans = Py.Import("weighted_mm_kmeans");
            Console.WriteLine("Kmeans: " + kmeans.__name__);
            var weights = kmeans.read_weights("testweights.txt");
            var data = kmeans.read_data("data.txt");
            try {
                var result = kmeans.minsize_kmeans_weighted(data, 3, weights, 0, 4000, 999);
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine("RESULT");
                }
                Console.WriteLine(result);
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine("END RESULT");
                }
            }
            catch (Exception e) {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
//def minsize_kmeans_weighted(dataset, k, weights=None, min_weight=0, max_weight=None,max_iters=999,uiter=None):
//@dataset - numpy matrix(or list of lists) - of point coordinates
//@k - number of clusters
//@weights - list of point weights, length equal to len(@dataset)
//@min_weight - minimum total weight per cluster
//@max_weight - maximum total weight per cluster
//@max_iters - if no convergence after this number of iterations, stop anyway
//@uiter - iterator like tqdm to print a progress bar.